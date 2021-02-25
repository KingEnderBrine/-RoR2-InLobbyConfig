using RoR2.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class EnumConfigField : BaseConfigField<Enum>
    {
        private static GameObject fieldPrefab;
        public override GameObject FieldPrefab
        {
            get
            {
                return fieldPrefab ? fieldPrefab : (fieldPrefab = AssetBundleHelper.LoadPrefab("EnumFieldPrefab"));
            }
        }
        public ReadOnlyCollection<string> Options { get; }
        public ReadOnlyCollection<Enum> Values { get; }
        public Type EnumType { get; }

        public EnumConfigField(Type enumType, string displayName, Func<Enum> valueAccessor, Action<Enum> onValueChanged = null) : base(displayName, valueAccessor, onValueChanged)
        {
            EnumType = enumType;
            Options = new ReadOnlyCollection<string>(Enum.GetNames(enumType));
            Values = new ReadOnlyCollection<Enum>(Enum.GetValues(enumType).Cast<Enum>().ToList());
        }

        public EnumConfigField(Type enumType, string displayName, string tooltip, Func<Enum> valueAccessor, Action<Enum> onValueChanged = null) : base(displayName, tooltip, valueAccessor, onValueChanged)
        {
            EnumType = enumType;
            Options = new ReadOnlyCollection<string>(Enum.GetNames(enumType));
            Values = new ReadOnlyCollection<Enum>(Enum.GetValues(enumType).Cast<Enum>().ToList());
        }

        public EnumConfigField(Type enumType, string displayName, TooltipContent tooltip, Func<Enum> valueAccessor, Action<Enum> onValueChanged = null) : base(displayName, tooltip, valueAccessor, onValueChanged)
        {
            EnumType = enumType;
            Options = new ReadOnlyCollection<string>(Enum.GetNames(enumType));
            Values = new ReadOnlyCollection<Enum>(Enum.GetValues(enumType).Cast<Enum>().ToList());
        }
    }
}
