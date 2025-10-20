using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class EnumConfigField : SelectConfigField
    {
        public Type EnumType { get; }

        public EnumConfigField(Type enumType, string displayName, Func<Enum> valueAccessor, Action<Enum> onValueChanged = null) : base(displayName, valueAccessor, () => GetEnumOptions(enumType), (v) => onValueChanged?.Invoke((Enum)v))
        {
            EnumType = enumType;
        }

        public EnumConfigField(Type enumType, string displayName, string tooltip, Func<Enum> valueAccessor, Action<Enum> onValueChanged = null) : base(displayName, tooltip, valueAccessor, () => GetEnumOptions(enumType), (v) => onValueChanged?.Invoke((Enum)v))
        {
            EnumType = enumType;
        }

        public EnumConfigField(Type enumType, string displayName, TooltipContent tooltip, Func<Enum> valueAccessor, Action<Enum> onValueChanged = null) : base(displayName, tooltip, valueAccessor, () => GetEnumOptions(enumType), (v) => onValueChanged?.Invoke((Enum)v))
        {
            EnumType = enumType;
        }

        private static IDictionary<object, string> GetEnumOptions(Type enumType)
        {
            return Enum.GetValues(enumType).Cast<Enum>().ToDictionary(v => (object)v, v => v.ToString());
        }
    }
}
