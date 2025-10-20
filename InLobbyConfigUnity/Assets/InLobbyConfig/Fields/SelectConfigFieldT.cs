using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RoR2.UI;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class SelectConfigField<T> : SelectConfigField
    {
        protected new Func<IDictionary<T, string>> OptionsAccessor { get; }  
        protected new Func<T> ValueAccessor { get; }  

        public SelectConfigField(string displayName, Func<T> valueAccessor, Func<IDictionary<T, string>> optionsAccessor, Action<T> onValueChanged = null) : base(displayName, () => valueAccessor(), () => optionsAccessor?.Invoke().ToDictionary(row => (object)row.Key, row => row.Value), (v) => onValueChanged?.Invoke((T)v))
        {
            OptionsAccessor = optionsAccessor;
            ValueAccessor = valueAccessor;
        }

        public SelectConfigField(string displayName, string tooltip, Func<T> valueAccessor, Func<IDictionary<T, string>> optionsAccessor, Action<T> onValueChanged = null) : base(displayName, tooltip, () => valueAccessor(), () => optionsAccessor?.Invoke().ToDictionary(row => (object)row.Key, row => row.Value), (v) => onValueChanged?.Invoke((T)v))
        {
            OptionsAccessor = optionsAccessor;
            ValueAccessor = valueAccessor;
        }

        public SelectConfigField(string displayName, TooltipContent tooltip, Func<T> valueAccessor, Func<IDictionary<T, string>> optionsAccessor, Action<T> onValueChanged = null) : base(displayName, tooltip, () => valueAccessor(), () => optionsAccessor?.Invoke().ToDictionary(row => (object)row.Key, row => row.Value), (v) => onValueChanged?.Invoke((T)v))
        {
            OptionsAccessor = optionsAccessor;
            ValueAccessor = valueAccessor;
        }

        public new IDictionary<T, string> GetOptions()
        {
            return OptionsAccessor.Invoke();
        }

        public new T GetValue()
        {
            return ValueAccessor.Invoke();
        }
    }
}
