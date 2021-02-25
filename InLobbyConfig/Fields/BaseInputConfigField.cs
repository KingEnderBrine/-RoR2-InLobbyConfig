using RoR2.UI;
using System;

namespace InLobbyConfig.Fields
{
    public abstract class BaseInputConfigField<T> : BaseConfigField<T>
    {
        protected Action<T> OnEndEditCallback { get; }

        public BaseInputConfigField(string displayName, Func<T> valueAccessor, Action<T> onValueChanged = null, Action<T> onEndEdit = null) : base(displayName, valueAccessor, onValueChanged)
        {
            OnEndEditCallback = onEndEdit;
        }

        public BaseInputConfigField(string displayName, string tooltip, Func<T> valueAccessor, Action<T> onValueChanged = null, Action<T> onEndEdit = null) : base(displayName, tooltip, valueAccessor, onValueChanged)
        {
            OnEndEditCallback = onEndEdit;
        }

        public BaseInputConfigField(string displayName, TooltipContent tooltip, Func<T> valueAccessor, Action<T> onValueChanged, Action<T> onEndEdit = null) : base(displayName, tooltip, valueAccessor, onValueChanged)
        {
            OnEndEditCallback = onEndEdit;
        }

        public virtual void OnEndEdit(T newValue)
        {
            OnEndEditCallback?.Invoke(newValue);
        }
    }
}
