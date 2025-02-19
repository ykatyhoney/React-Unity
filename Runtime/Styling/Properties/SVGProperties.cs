using System;
using System.Collections.Generic;
using Facebook.Yoga;

using ReactUnity.Styling.Animations;
using ReactUnity.Styling.Computed;
using ReactUnity.Styling.Converters;
using ReactUnity.Types;
using TMPro;
using UnityEngine;

namespace ReactUnity.Styling
{
    public static class SVGProperties
    {
        public static readonly StyleProperty<Color> fill = new StyleProperty<Color>("fill", ComputedCurrentColor.Instance, true, false);
        public static readonly StyleProperty<Color> stroke = new StyleProperty<Color>("stroke", ComputedCurrentColor.Instance, true, false);

        public static readonly Dictionary<string, IStyleProperty> PropertyMap = new Dictionary<string, IStyleProperty>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "fill", fill },
            { "stroke", stroke },
        };
    }
}
