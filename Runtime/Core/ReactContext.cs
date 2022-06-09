using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ExCSS;
using ReactUnity.Helpers;
using ReactUnity.Helpers.Visitors;
using ReactUnity.Html;
using ReactUnity.Scheduling;
using ReactUnity.Scripting;
using ReactUnity.Scripting.DomProxies;
using ReactUnity.Styling;
using ReactUnity.Styling.Rules;
using UnityEngine;

namespace ReactUnity
{
    public abstract partial class ReactContext : IDisposable
    {
        public class Options
        {
            public SerializableDictionary Globals;
            public ScriptSource Source;
            public ITimer Timer;
            public IMediaProvider MediaProvider;
            public Action OnRestart;
            public JavascriptEngineType EngineType;
            public bool Debug;
            public bool AwaitDebugger;
            public Action BeforeStart;
            public Action AfterStart;

            public virtual bool CalculatesLayout { get; }
        }

        protected static Regex ExtensionRegex = new Regex(@"\.\w+$");
        protected static Regex ResourcesRegex = new Regex(@"resources(/|\\)", RegexOptions.IgnoreCase);

        public bool CalculatesLayout { get; }
        public IHostComponent Host { get; protected set; }
        public HashSet<IReactComponent> DetachedRoots { get; protected set; } = new HashSet<IReactComponent>();
        public GlobalRecord Globals { get; private set; }
        public bool IsDisposed { get; private set; }
        public virtual bool IsEditorContext => false;

        public Options options { get; }
        public ScriptSource Source { get; }
        public ITimer Timer { get; }
        public IDispatcher Dispatcher { get; }
        public virtual Dictionary<string, Type> StateHandlers { get; }
        public Location Location { get; }
        public LocalStorage LocalStorage { get; }
        public IMediaProvider MediaProvider { get; }
        public Action OnRestart { get; }
        public StylesheetParser StyleParser { get; }
        public StyleContext Style { get; private set; }
        public ScriptContext Script { get; }
        public HtmlContext Html { get; }
        public virtual CursorSet CursorSet { get; }
        public CursorAPI CursorAPI { get; }
        public List<Action> Disposables { get; } = new List<Action>();

        public ReactContext(Options options)
        {
            this.options = options;
            Source = options.Source;
            Timer = options.Timer;
            Dispatcher = CreateDispatcher();
            Globals = GlobalRecord.BindSerializableDictionary(options.Globals, Dispatcher, false);
            OnRestart = options.OnRestart ?? (() => { });
            CalculatesLayout = options.CalculatesLayout;
            Location = new Location(this);
            MediaProvider = options.MediaProvider;
            CursorAPI = new CursorAPI(this);
            LocalStorage = new LocalStorage();

            StyleParser = new StylesheetParser(true, true, true, true, true, false, true);
            Style = CreateStyleContext();
            Script = new ScriptContext(this, options.EngineType, options.Debug, options.AwaitDebugger);

            Html = new HtmlContext(this);

            Dispatcher.OnEveryUpdate(UpdateElementsRecursively);

            if (CalculatesLayout) Dispatcher.OnEveryLateUpdate(() => {
                Host?.Layout.CalculateLayout();
                foreach (var dr in DetachedRoots) dr.Layout.CalculateLayout();
            });

#if UNITY_EDITOR
            // Runtime contexts are disposed on reload (by OnDisable), but this is required for editor contexts 
            UnityEditor.AssemblyReloadEvents.beforeAssemblyReload += Dispose;
#endif
        }

        public void UpdateElementsRecursively()
        {
            Host?.Accept(UpdateVisitor.Instance);
        }

        protected virtual StyleContext CreateStyleContext() => new StyleContext(this);

        public virtual StyleSheet InsertStyle(string style) => InsertStyle(style, 0);

        public virtual StyleSheet InsertStyle(string style, int importanceOffset)
        {
            var sheet = new StyleSheet(Style, style, importanceOffset);
            return InsertStyle(sheet);
        }

        public virtual StyleSheet InsertStyle(StyleSheet sheet)
        {
            Style.Insert(sheet);
            return sheet;
        }

        public virtual void RemoveStyle(StyleSheet sheet)
        {
            Style.Remove(sheet);
        }

        public virtual string ResolvePath(string path)
        {
            var source = Source.GetResolvedSourceUrl();
            var type = Source.EffectiveScriptSource;

            if (type == ScriptSourceType.Url)
            {
                var baseUrl = new Uri(source);
                if (Uri.TryCreate(baseUrl, path, out var res)) return res.AbsoluteUri;
            }
            else if (type == ScriptSourceType.File || type == ScriptSourceType.Resource)
            {
                var lastSlash = source.LastIndexOfAny(new[] { '/', '\\' });
                var parent = source.Substring(0, lastSlash);

                var res = parent + (path.StartsWith("/") ? path : "/" + path);
                if (type == ScriptSourceType.Resource) return GetResourceUrl(res);
                return res;
            }
            else
            {
                // TODO: write path rewriting logic
            }

            return null;
        }

        public virtual ScriptSource CreateStaticScript(string path)
        {
            var src = new ScriptSource(Source);
            src.SourcePath = ResolvePath(path);
            src.Type = Source.EffectiveScriptSource;
            src.UseDevServer = Source.IsDevServer;
            return src;
        }

        private string GetResourceUrl(string fullUrl)
        {
            var splits = ResourcesRegex.Split(fullUrl);
            var url = splits[splits.Length - 1];

            return ExtensionRegex.Replace(url, "");
        }

        public abstract ITextComponent CreateText(string text);
        public abstract IReactComponent CreateDefaultComponent(string tag, string text);
        public abstract IReactComponent CreateComponent(string tag, string text);
        public abstract IReactComponent CreatePseudoComponent(string tag);
        public abstract void PlayAudio(AudioClip clip);

        public void Start(Action afterStart = null)
        {
            SetRef(0, Host);
            var renderCount = 0;

            var scriptJob = Source.GetScript((code) => {
                if (renderCount > 0)
                {
                    Style = CreateStyleContext();
                }

                renderCount++;

                if (Source.Language == ScriptSourceLanguage.Html)
                {
                    options.BeforeStart?.Invoke();
                    Html.InsertHtml(code, Host, true);
                    afterStart?.Invoke();
                    options.AfterStart?.Invoke();
                }
                else
                {
                    Script.RunMainScript(code, options.BeforeStart, () => {
                        afterStart?.Invoke();
                        options.AfterStart?.Invoke();
                    });
                }

                Style.ResolveStyle();
                if (CalculatesLayout) Host.Layout?.CalculateLayout();
            }, Dispatcher, true);

            if (scriptJob != null) Disposables.Add(scriptJob.Dispose);
        }

        public void Dispose()
        {
            CommandsCallback = null;
            FireEventByRefCallback = null;
            GetObjectCallback = null;
            GetEventAsObjectCallback = null;

            IsDisposed = true;
            Host?.Destroy(false);
            Host = null;
            Refs.Clear();
            foreach (var dr in DetachedRoots) dr.Destroy(false);
            DetachedRoots.Clear();
            Dispatcher?.Dispose();
            Globals?.Dispose();
            foreach (var item in Disposables) item?.Invoke();
            Script?.Dispose();
        }

        protected virtual IDispatcher CreateDispatcher() => Application.isPlaying && !IsEditorContext ?
            RuntimeDispatcherBehavior.Create(this) as IDispatcher :
            new EditorDispatcher(this);
    }
}
