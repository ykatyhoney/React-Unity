#if !(ENABLE_IL2CPP || REACT_DISABLE_CLEARSCRIPT || (UNITY_ANDROID && !UNITY_EDITOR)) && REACT_CLEARSCRIPT_AVAILABLE
#define REACT_CLEARSCRIPT
#endif

#if !REACT_DISABLE_JINT && REACT_JINT_AVAILABLE
#define REACT_JINT
#endif

#if !REACT_DISABLE_QUICKJS && REACT_QUICKJS_AVAILABLE
#define REACT_QUICKJS
#endif

namespace ReactUnity.Scripting
{
    internal class JavascriptEngineHelpers
    {
        public static IJavaScriptEngineFactory GetEngineFactory(JavascriptEngineType type)
        {
            switch (type)
            {
#if REACT_JINT
                case JavascriptEngineType.Jint:
                    return new JintEngineFactory();
#endif
#if REACT_QUICKJS
                case JavascriptEngineType.QuickJS:
                    return new QuickJSEngineFactory();
#endif
#if REACT_CLEARSCRIPT
                case JavascriptEngineType.ClearScript:
                    return new ClearScriptEngineFactory();
#endif
                default:

#if REACT_QUICKJS
                    return new QuickJSEngineFactory();
#elif REACT_CLEARSCRIPT
                    return new ClearScriptEngineFactory();
#elif REACT_JINT
                    return new JintEngineFactory();
#else
                    throw new System.Exception("Could not find a valid scripting engine.");
#endif
            }
        }
    }
}
