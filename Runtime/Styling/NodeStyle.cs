using System.Collections.Generic;
using Facebook.Yoga;
using ReactUnity.Animations;
using ReactUnity.Styling.Computed;
using ReactUnity.Types;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReactUnity.Styling
{
    public class NodeStyle
    {
        Dictionary<string, object> StyleMap;
        public List<IDictionary<IStyleProperty, object>> CssStyles;
        NodeStyle Fallback;
        public bool HasInheritedChanges { get; private set; } = false;

        public ReactContext Context;
        public NodeStyle Parent;

        #region Set/Get

        public float opacity
        {
            set => SetStyleValue(StyleProperties.opacity, value);
            get => GetStyleValue<float>(StyleProperties.opacity);
        }
        public int zIndex
        {
            set => SetStyleValue(StyleProperties.zIndex, value);
            get => GetStyleValue<int>(StyleProperties.zIndex);
        }
        public bool visibility
        {
            set => SetStyleValue(StyleProperties.visibility, value);
            get => GetStyleValue<bool>(StyleProperties.visibility);
        }
        public CursorList cursor
        {
            set => SetStyleValue(StyleProperties.cursor, value);
            get => GetStyleValue<CursorList>(StyleProperties.cursor);
        }
        public PointerEvents pointerEvents
        {
            set => SetStyleValue(StyleProperties.pointerEvents, value);
            get => GetStyleValue<PointerEvents>(StyleProperties.pointerEvents);
        }
        public Color backgroundColor
        {
            set => SetStyleValue(StyleProperties.backgroundColor, value);
            get => GetStyleValue<Color>(StyleProperties.backgroundColor);
        }
        public ImageReference backgroundImage
        {
            set => SetStyleValue(StyleProperties.backgroundImage, value);
            get => GetStyleValue<ImageReference>(StyleProperties.backgroundImage);
        }
        public float borderRadius
        {
            set => SetStyleValue(StyleProperties.borderRadius, value);
            get => GetStyleValue<float>(StyleProperties.borderRadius);
        }
        public float borderTopLeftRadius
        {
            set => SetStyleValue(StyleProperties.borderTopLeftRadius, value);
            get => HasValue(StyleProperties.borderTopLeftRadius) ? GetStyleValue<float>(StyleProperties.borderTopLeftRadius) : GetStyleValue<float>(StyleProperties.borderRadius);
        }
        public float borderTopRightRadius
        {
            set => SetStyleValue(StyleProperties.borderTopRightRadius, value);
            get => HasValue(StyleProperties.borderTopRightRadius) ? GetStyleValue<float>(StyleProperties.borderTopRightRadius) : GetStyleValue<float>(StyleProperties.borderRadius);
        }
        public float borderBottomLeftRadius
        {
            set => SetStyleValue(StyleProperties.borderBottomLeftRadius, value);
            get => HasValue(StyleProperties.borderBottomLeftRadius) ? GetStyleValue<float>(StyleProperties.borderBottomLeftRadius) : GetStyleValue<float>(StyleProperties.borderRadius);
        }
        public float borderBottomRightRadius
        {
            set => SetStyleValue(StyleProperties.borderBottomRightRadius, value);
            get => HasValue(StyleProperties.borderBottomRightRadius) ? GetStyleValue<float>(StyleProperties.borderBottomRightRadius) : GetStyleValue<float>(StyleProperties.borderRadius);
        }
        public Color borderColor
        {
            set => SetStyleValue(StyleProperties.borderColor, value);
            get => GetStyleValue<Color>(StyleProperties.borderColor);
        }
        public Color borderLeftColor
        {
            set => SetStyleValue(StyleProperties.borderLeftColor, value);
            get => HasValue(StyleProperties.borderLeftColor) ? GetStyleValue<Color>(StyleProperties.borderLeftColor) : GetStyleValue<Color>(StyleProperties.borderColor);
        }
        public Color borderRightColor
        {
            set => SetStyleValue(StyleProperties.borderRightColor, value);
            get => HasValue(StyleProperties.borderRightColor) ? GetStyleValue<Color>(StyleProperties.borderRightColor) : GetStyleValue<Color>(StyleProperties.borderColor);
        }
        public Color borderTopColor
        {
            set => SetStyleValue(StyleProperties.borderTopColor, value);
            get => HasValue(StyleProperties.borderTopColor) ? GetStyleValue<Color>(StyleProperties.borderTopColor) : GetStyleValue<Color>(StyleProperties.borderColor);
        }
        public Color borderBottomColor
        {
            set => SetStyleValue(StyleProperties.borderBottomColor, value);
            get => HasValue(StyleProperties.borderBottomColor) ? GetStyleValue<Color>(StyleProperties.borderBottomColor) : GetStyleValue<Color>(StyleProperties.borderColor);
        }
        public BoxShadowList boxShadow
        {
            set => SetStyleValue(StyleProperties.boxShadow, value);
            get => GetStyleValue<BoxShadowList>(StyleProperties.boxShadow);
        }
        public YogaValue2 translate
        {
            set => SetStyleValue(StyleProperties.translate, value);
            get => GetStyleValue<YogaValue2>(StyleProperties.translate);
        }
        public Vector2 scale
        {
            set => SetStyleValue(StyleProperties.scale, value);
            get => GetStyleValue<Vector2>(StyleProperties.scale);
        }
        public YogaValue2 transformOrigin
        {
            set => SetStyleValue(StyleProperties.transformOrigin, value);
            get => GetStyleValue<YogaValue2>(StyleProperties.transformOrigin);
        }
        public Vector3 rotate
        {
            set => SetStyleValue(StyleProperties.rotate, value);
            get => GetStyleValue<Vector3>(StyleProperties.rotate);
        }
        public FontReference fontFamily
        {
            set => SetStyleValue(StyleProperties.fontFamily, value);
            get => GetStyleValue<FontReference>(StyleProperties.fontFamily);
        }
        public Color color
        {
            set => SetStyleValue(StyleProperties.color, value);
            get => GetStyleValue<Color>(StyleProperties.color);
        }
        public FontWeight fontWeight
        {
            set => SetStyleValue(StyleProperties.fontWeight, value);
            get => GetStyleValue<FontWeight>(StyleProperties.fontWeight);
        }
        public FontStyles fontStyle
        {
            set => SetStyleValue(StyleProperties.fontStyle, value);
            get => GetStyleValue<FontStyles>(StyleProperties.fontStyle);
        }
        public float fontSize
        {
            set => SetStyleValue(StyleProperties.fontSize, value);
            get => GetStyleValue<float>(StyleProperties.fontSize);
        }
        public float lineHeight
        {
            set => SetStyleValue(StyleProperties.lineHeight, value);
            get => GetStyleValue<float>(StyleProperties.lineHeight);
        }
        public float letterSpacing
        {
            set => SetStyleValue(StyleProperties.letterSpacing, value);
            get => GetStyleValue<float>(StyleProperties.letterSpacing);
        }
        public float wordSpacing
        {
            set => SetStyleValue(StyleProperties.wordSpacing, value);
            get => GetStyleValue<float>(StyleProperties.wordSpacing);
        }
        public TextAlignmentOptions textAlign
        {
            set => SetStyleValue(StyleProperties.textAlign, value);
            get => GetStyleValue<TextAlignmentOptions>(StyleProperties.textAlign);
        }
        public TextOverflowModes textOverflow
        {
            set => SetStyleValue(StyleProperties.textOverflow, value);
            get => GetStyleValue<TextOverflowModes>(StyleProperties.textOverflow);
        }
        public bool textWrap
        {
            set => SetStyleValue(StyleProperties.textWrap, value);
            get => GetStyleValue<bool>(StyleProperties.textWrap);
        }
        public string content
        {
            set => SetStyleValue(StyleProperties.content, value);
            get => GetStyleValue<string>(StyleProperties.content);
        }
        public Appearance appearance
        {
            set => SetStyleValue(StyleProperties.appearance, value);
            get => GetStyleValue<Appearance>(StyleProperties.appearance);
        }
        public Navigation.Mode navigation
        {
            set => SetStyleValue(StyleProperties.navigation, value);
            get => GetStyleValue<Navigation.Mode>(StyleProperties.navigation);
        }
        public TransitionList transition
        {
            set => SetStyleValue(StyleProperties.transition, value);
            get => GetStyleValue<TransitionList>(StyleProperties.transition);
        }
        public AnimationList animation
        {
            set => SetStyleValue(StyleProperties.animation, value);
            get => GetStyleValue<AnimationList>(StyleProperties.animation);
        }
        public AudioList audio
        {
            set => SetStyleValue(StyleProperties.audio, value);
            get => GetStyleValue<AudioList>(StyleProperties.audio);
        }
        public ObjectFit objectFit
        {
            set => SetStyleValue(StyleProperties.objectFit, value);
            get => GetStyleValue<ObjectFit>(StyleProperties.objectFit);
        }
        public YogaValue2 objectPosition
        {
            set => SetStyleValue(StyleProperties.objectPosition, value);
            get => GetStyleValue<YogaValue2>(StyleProperties.objectPosition);
        }
        #endregion

        public NodeStyle(ReactContext context, NodeStyle fallback = null)
        {
            Context = context;
            StyleMap = new Dictionary<string, object>();
            Fallback = fallback;
        }

        public void UpdateParent(NodeStyle parent)
        {
            Parent = parent;
            Fallback?.UpdateParent(parent);
        }

        public void CopyStyle(NodeStyle copyFrom)
        {
            StyleMap = new Dictionary<string, object>(copyFrom.StyleMap);
        }

        public object GetRawStyleValue(IStyleProperty prop, bool fromChild = false, NodeStyle activeStyle = null)
        {
            if (fromChild) HasInheritedChanges = true;

            object value;
            var name = prop.name;

            if (
                !StyleMap.TryGetValue(name, out value) &&
                !OwnTryGetValue(prop, out value))
            {
                if (Fallback != null)
                {
                    value = Fallback.GetRawStyleValue(prop, fromChild, this);
                }
                else if (prop.inherited)
                {
                    value = Parent?.GetRawStyleValue(prop, true) ?? prop?.defaultValue;
                }
                else value = prop?.defaultValue;
            }

            return GetStyleValueSpecial(value, prop, activeStyle ?? this);
        }

        private object GetStyleValueSpecial(object value, IStyleProperty prop, NodeStyle activeStyle)
        {
            if (value == null) return null;
            if (value is CssKeyword ck)
            {
                if (ck == CssKeyword.Invalid) return null;
                else if (ck == CssKeyword.Auto) return prop?.defaultValue;
                else if (ck == CssKeyword.None) return prop?.noneValue;
                else if (ck == CssKeyword.Initial || ck == CssKeyword.Unset) return prop?.defaultValue;
                else if (ck == CssKeyword.Inherit) return Parent?.GetRawStyleValue(prop) ?? prop?.defaultValue;
            }
            return value;
        }

        public T GetStyleValue<T>(IStyleProperty prop, bool convert = false)
        {
            var value = GetRawStyleValue(prop);
            if (value is IComputedValue d) value = d.Convert(prop, this);
            if (value == null) return default;
            if (convert && value.GetType() != typeof(T)) value = prop.Convert(value);

#if UNITY_EDITOR
            if (value != null && value.GetType() != typeof(T) && !typeof(T).IsEnum)
            {
                Debug.LogError($"Error while converting {value} from type {value.GetType()} to {typeof(T)}");
            }
#endif

            return (T) value;
        }

        public void SetStyleValue(IStyleProperty prop, object value)
        {
            var name = prop.name;
            object currentValue;

            if (!StyleMap.TryGetValue(name, out currentValue))
            {
                if (value == null) return;
            }

            var changed = currentValue != value;
            if (value == null)
            {
                StyleMap.Remove(name);
            }
            else
            {
                StyleMap[name] = value;
            }

            if (changed)
            {
                if (prop.inherited) HasInheritedChanges = true;
            }
        }

        public void MarkChangesSeen()
        {
            HasInheritedChanges = false;
        }

        public bool HasValue(IStyleProperty prop)
        {
            return StyleMap.ContainsKey(prop.name) ||
                OwnHasValue(prop) ||
                (Fallback != null && Fallback.HasValue(prop));
        }

        private bool OwnTryGetValue(IStyleProperty prop, out object res)
        {
            if (CssStyles == null)
            {
                res = null;
                return false;
            }
            for (int i = 0; i < CssStyles.Count; i++)
            {
                var dic = CssStyles[i];
                if (dic.TryGetValue(prop, out res)) return true;
            }
            res = null;
            return false;
        }

        private bool OwnHasValue(IStyleProperty prop)
        {
            if (CssStyles == null) return false;
            for (int i = 0; i < CssStyles.Count; i++)
            {
                var dic = CssStyles[i];
                if (dic.ContainsKey(prop)) return true;
            }
            return false;
        }
    }
}
