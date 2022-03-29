using RoR2.UI;
using System;

namespace InLobbyConfig.Fields
{
    public abstract class BaseNumberInputConfigField<T> : BaseInputConfigField<T> where T : struct
    {
        public T? Minimum { get; }
        public T? Maximum { get; }

        public BaseNumberInputConfigField(string displayName, Func<T> valueAccessor, Action<T> onValueChanged = null, Action<T> onEndEdit = null, T? minimum = null, T? maximum = null) : base(displayName, valueAccessor, onValueChanged, onEndEdit)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public BaseNumberInputConfigField(string displayName, string tooltip, Func<T> valueAccessor, Action<T> onValueChanged = null, Action<T> onEndEdit = null, T? minimum = null, T? maximum = null) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public BaseNumberInputConfigField(string displayName, TooltipContent tooltip, Func<T> valueAccessor, Action<T> onValueChanged = null, Action<T> onEndEdit = null, T? minimum = null, T? maximum = null) : base(displayName, tooltip, valueAccessor, onValueChanged, onEndEdit)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
    }
}
