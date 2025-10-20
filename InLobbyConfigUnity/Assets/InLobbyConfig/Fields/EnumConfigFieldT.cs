using RoR2.UI;
using System;
using System.Collections.Generic;

namespace InLobbyConfig.Fields
{
    public class EnumConfigField<T> : EnumConfigField where T : Enum
    {
        protected new Action<T> OnValueChangedCallback { get; } 
        protected new Func<T> ValueAccessor { get; }
        public EnumConfigField(string displayName, Func<T> valueAccessor, Action<T> onValueChanged = null) : base(typeof(T), displayName, () => valueAccessor.Invoke())
        {
            ValueAccessor = valueAccessor;
            OnValueChangedCallback = onValueChanged;
        }

        public EnumConfigField(string displayName, string tooltip, Func<T> valueAccessor, Action<T> onValueChanged = null) : base(typeof(T), displayName, tooltip, () => valueAccessor.Invoke())
        {
            ValueAccessor = valueAccessor;
            OnValueChangedCallback = onValueChanged;
        }

        public EnumConfigField(string displayName, TooltipContent tooltip, Func<T> valueAccessor, Action<T> onValueChanged = null) : base(typeof(T), displayName, tooltip, () => valueAccessor.Invoke())
        {
            ValueAccessor = valueAccessor;
            OnValueChangedCallback = onValueChanged;
        }

        public void OnValueChanged(T newValue)
        {
            OnValueChangedCallback?.Invoke(newValue);
        }

        public override void OnValueChanged(object newValue)
        {
            OnValueChangedCallback?.Invoke((T)newValue);
        }

        public new T GetValue()
        {
            return ValueAccessor.Invoke();
        }
    }
}
