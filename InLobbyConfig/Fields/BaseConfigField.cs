using RoR2.UI;
using System;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public abstract class BaseConfigField<T> : IConfigField
    {        
        public abstract GameObject FieldPrefab { get; }
        
        public string DisplayName { get; }
        public TooltipContent Tooltip { get; }

        protected Func<T> ValueAccessor { get; }
        protected Action<T> OnValueChangedCallback { get; }


        public BaseConfigField(string displayName, Func<T> valueAccessor, Action<T> onValueChanged)
        {
            DisplayName = displayName;
            ValueAccessor = valueAccessor;
            OnValueChangedCallback = onValueChanged;
        }

        public BaseConfigField(string displayName, string tooltip, Func<T> valueAccessor, Action<T> onValueChanged) : this(displayName, valueAccessor, onValueChanged)
        {
            if (string.IsNullOrEmpty(tooltip))
            {
                return;
            }
            Tooltip = new TooltipContent
            {
                titleColor = Color.black,
                overrideTitleText = displayName,
                bodyColor = Color.black,
                overrideBodyText = tooltip
            };
        }

        public BaseConfigField(string displayName, TooltipContent tooltip, Func<T> valueAccessor, Action<T> onValueChanged) : this(displayName, valueAccessor, onValueChanged)
        {
            Tooltip = tooltip;
        }

        object IConfigField.GetValue()
        {
            return GetValue();
        }

        public virtual T GetValue()
        {
            return ValueAccessor.Invoke();
        }

        public virtual void OnValueChanged(T newValue)
        {
            OnValueChangedCallback?.Invoke(newValue);
        }
    }
}
