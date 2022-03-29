using RoR2.UI;
using System;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class IntConfigField : BaseNumberInputConfigField<int>
    {
        private static GameObject fieldPrefab;
        public override GameObject FieldPrefab
        {
            get
            {
                return fieldPrefab ? fieldPrefab : (fieldPrefab = AssetBundleHelper.LoadPrefab("IntFieldPrefab"));
            }
        }

        public IntConfigField(string displayName, Func<int> valueAccessor, Action<int> onValueChanged = null, Action<int> onEndEdit = null, int? minimum = null, int? maximum = null) : base(displayName, valueAccessor, onValueChanged, onEndEdit, minimum, maximum)
        {
        }

        public IntConfigField(string displayName, string tooltip, Func<int> valueAccessor, Action<int> onValueChanged = null, Action<int> onEndEdit = null, int? minimum = null, int? maximum = null) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit, minimum, maximum)
        {
        }

        public IntConfigField(string displayName, TooltipContent tooltip, Func<int> valueAccessor, Action<int> onValueChanged = null, Action<int> onEndEdit = null, int? minimum = null, int? maximum = null) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit, minimum, maximum)
        {
        }
    }
}
