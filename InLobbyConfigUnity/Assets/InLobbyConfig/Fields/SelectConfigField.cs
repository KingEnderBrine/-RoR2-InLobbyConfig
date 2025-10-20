using System;
using System.Collections.Generic;
using RoR2.UI;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class SelectConfigField : BaseConfigField<object>
    {
        private static GameObject fieldPrefab;
        public override GameObject FieldPrefab
        {
            get
            {
                return fieldPrefab ? fieldPrefab : (fieldPrefab = AssetBundleHelper.LoadPrefab("SelectFieldPrefab"));
            }
        }
        protected Func<IDictionary<object, string>> OptionsAccessor { get; }  

        public SelectConfigField(string displayName, Func<object> valueAccessor, Func<IDictionary<object, string>> optionsAccessor, Action<object> onValueChanged = null) : base(displayName, valueAccessor, onValueChanged)
        {
            OptionsAccessor = optionsAccessor;
        }

        public SelectConfigField(string displayName, string tooltip, Func<object> valueAccessor, Func<IDictionary<object, string>> optionsAccessor, Action<object> onValueChanged = null) : base(displayName, tooltip, valueAccessor, onValueChanged)
        {
            OptionsAccessor = optionsAccessor;
        }

        public SelectConfigField(string displayName, TooltipContent tooltip, Func<object> valueAccessor, Func<IDictionary<object, string>> optionsAccessor, Action<object> onValueChanged = null) : base(displayName, tooltip, valueAccessor, onValueChanged)
        {
            OptionsAccessor = optionsAccessor;
        }

        public IDictionary<object, string> GetOptions()
        {
            return OptionsAccessor?.Invoke();
        }
    }
}
