using RoR2.UI;
using System;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class ColorConfigField : BaseInputConfigField<Color>
    {
        private static GameObject fieldPrefab;
        public override GameObject FieldPrefab
        {
            get
            {
                return fieldPrefab ? fieldPrefab : (fieldPrefab = AssetBundleHelper.LoadPrefab("ColorFieldPrefab"));
            }
        }
        public bool ShowAlpha { get; set; }

        public ColorConfigField(string displayName, Func<Color> valueAccessor, Action<Color> onValueChanged = null, Action<Color> onEndEdit = null, bool showAlpha = true) : base(displayName, valueAccessor, onValueChanged, onEndEdit)
        {
            ShowAlpha = showAlpha;
        }

        public ColorConfigField(string displayName, string tooltip, Func<Color> valueAccessor, Action<Color> onValueChanged = null, Action<Color> onEndEdit = null, bool showAlpha = true) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit)
        {
            ShowAlpha = showAlpha;
        }

        public ColorConfigField(string displayName, TooltipContent tooltip, Func<Color> valueAccessor, Action<Color> onValueChanged = null, Action<Color> onEndEdit = null, bool showAlpha = true) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit)
        {
            ShowAlpha = showAlpha;
        }
    }
}
