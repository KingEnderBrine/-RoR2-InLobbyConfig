using RoR2.UI;
using System;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class StringConfigField : BaseInputConfigField<string>
    {
        private static GameObject fieldPrefab;
        public override GameObject FieldPrefab
        {
            get
            {
                return fieldPrefab ? fieldPrefab : (fieldPrefab = AssetBundleHelper.LoadPrefab("StringFieldPrefab"));
            }
        }

        public StringConfigField(string displayName, Func<string> valueAccessor, Action<string> onValueChanged = null, Action<string> onEndEdit = null) : base(displayName, valueAccessor, onValueChanged, onEndEdit)
        {
        }

        public StringConfigField(string displayName, string tooltip, Func<string> valueAccessor, Action<string> onValueChanged = null, Action<string> onEndEdit = null) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit)
        {
        }

        public StringConfigField(string displayName, TooltipContent tooltip, Func<string> valueAccessor, Action<string> onValueChanged, Action<string> onEndEdit = null) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit)
        {
        }
    }
}
