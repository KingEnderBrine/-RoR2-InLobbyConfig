using RoR2.UI;
using System;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class FloatConfigField : BaseNumberInputConfigField<float>
    {
        private static GameObject fieldPrefab;
        public override GameObject FieldPrefab
        {
            get
            {
                return fieldPrefab ? fieldPrefab : (fieldPrefab = AssetBundleHelper.LoadPrefab("FloatFieldPrefab"));
            }
        }

        public FloatConfigField(string displayName, Func<float> valueAccessor, Action<float> onValueChanged = null, Action<float> onEndEdit = null, float? minimum = null, float? maximum = null) : base(displayName, valueAccessor, onValueChanged, onEndEdit, minimum, maximum)
        {
        }

        public FloatConfigField(string displayName, string tooltip, Func<float> valueAccessor, Action<float> onValueChanged = null, Action<float> onEndEdit = null, float? minimum = null, float? maximum = null) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit, minimum, maximum)
        {
        }

        public FloatConfigField(string displayName, TooltipContent tooltip, Func<float> valueAccessor, Action<float> onValueChanged = null, Action<float> onEndEdit = null, float? minimum = null, float? maximum = null) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit, minimum, maximum)
        {
        }
    }
}
