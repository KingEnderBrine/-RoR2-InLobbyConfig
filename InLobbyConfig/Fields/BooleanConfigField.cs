using RoR2.UI;
using System;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class BooleanConfigField : BaseConfigField<bool>
    {
        private static GameObject fieldPrefab;
        public override GameObject FieldPrefab
        {
            get
            {
                return fieldPrefab ? fieldPrefab : (fieldPrefab = AssetBundleHelper.LoadPrefab("BooleanFieldPrefab"));
            }
        }

        public BooleanConfigField(string displayName, Func<bool> valueAccessor, Action<bool> onValueChanged = null) : base(displayName, valueAccessor, onValueChanged)
        {
        }

        public BooleanConfigField(string displayName, string tooltip, Func<bool> valueAccessor, Action<bool> onValueChanged = null) : base(displayName, tooltip, valueAccessor, onValueChanged)
        {
        }

        public BooleanConfigField(string displayName, TooltipContent tooltip, Func<bool> valueAccessor, Action<bool> onValueChanged) : base(displayName, tooltip, valueAccessor, onValueChanged)
        {
        }
    }
}
