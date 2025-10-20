using BepInEx.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public static class ConfigFieldUtilities
    {
        public static ModConfigEntry CreateFromBepInExConfigFile(ConfigFile file, string displayName) => CreateFromBepInExConfigFile(file, displayName, true, true);
        public static ModConfigEntry CreateFromBepInExConfigFile(ConfigFile file, string displayName, bool tryToFindEnabledField) => CreateFromBepInExConfigFile(file, displayName, tryToFindEnabledField, true);

        public static ModConfigEntry CreateFromBepInExConfigFile(ConfigFile file, string displayName, bool tryToFindEnabledField, bool tryToFindSectionEnabledField)
        {
            var entry = new ModConfigEntry
            {
                DisplayName = displayName
            };
            foreach (var row in file)
            {
                if (tryToFindEnabledField && entry.EnableField == null && row.Key.Key.Equals("enabled", StringComparison.InvariantCultureIgnoreCase) && row.Value.BoxedValue is bool)
                {
                    entry.EnableField = CreateBooleanConfigField(row.Value as ConfigEntry<bool>);
                    continue;
                }
                if (tryToFindSectionEnabledField && !entry.SectionEnableFields.ContainsKey(row.Key.Section) && row.Key.Key.Equals("sectionEnabled", StringComparison.InvariantCultureIgnoreCase) && row.Value.BoxedValue is bool)
                {
                    entry.SectionEnableFields[row.Key.Section] = CreateBooleanConfigField(row.Value as ConfigEntry<bool>);
                }
                var field = ProcessConfigRow(row.Value);
                if (field == null)
                {
                    InLobbyConfigPlugin.InstanceLogger.LogWarning($"Config [{displayName}]. Not found prefab that can be associated with `{row.Key.Key}` field.");
                    continue;
                }
                if (!entry.SectionFields.TryGetValue(row.Key.Section, out var fields))
                {
                    fields = entry.SectionFields[row.Key.Section] = new List<IConfigField>();
                }
                (fields as List<IConfigField>).Add(field);
            }

            return entry;
        }

        public static IConfigField CreateFromBepInExConfigEntry<T>(ConfigEntry<T> configEntry)
        {
            return ProcessConfigRow(configEntry);
        }

        private static IConfigField ProcessConfigRow(ConfigEntryBase configEntry)
        {
            try
            {
                if (configEntry.Description.AcceptableValues is not null)
                {
                    var field = CreateSelectConfigField(configEntry);
                    if (field != null)
                    {
                        return field;
                    }
                }
                if (configEntry.BoxedValue is bool)
                {
                    return CreateBooleanConfigField(configEntry as ConfigEntry<bool>);
                }
                if (configEntry.BoxedValue is Enum)
                {
                    return CreateEnumConfigField(configEntry);
                }
                if (configEntry.BoxedValue is int)
                {
                    return CreateIntConfigField(configEntry as ConfigEntry<int>);
                }
                if (configEntry.BoxedValue is float)
                {
                    return CreateFloatConfigField(configEntry as ConfigEntry<float>);
                }
                if (configEntry.BoxedValue is string)
                {
                    return CreateStringConfigField(configEntry as ConfigEntry<string>);
                }
                if (configEntry.BoxedValue is Color)
                {
                    return CreateColorConfigField(configEntry as ConfigEntry<Color>);
                }
            }
            catch (Exception ex)
            {
                InLobbyConfigPlugin.InstanceLogger.LogError(ex);
            }

            return null;
        }

        private static IConfigField CreateSelectConfigField(ConfigEntryBase configEntry)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;
            if (acceptableValues.GetType().GetGenericTypeDefinition() != typeof(AcceptableValueList<string>).GetGenericTypeDefinition())
            {
                return null;
            }

            var options = new Dictionary<object, string>();
            var valuesField = acceptableValues.GetType().GetProperty(nameof(AcceptableValueList<string>.AcceptableValues));
            var values = valuesField.GetValue(acceptableValues) as IList;
            foreach (var value in values)
            {
                options[value] = value?.ToString();
            }

            return new SelectConfigField(configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, OptionsAccessor, OnEndEdit);

            IDictionary<object, string> OptionsAccessor()
            {
                return options;
            }
            object ValueAccessor()
            {
                return configEntry.BoxedValue;
            }
            void OnEndEdit(object newValue)
            {
                configEntry.BoxedValue = newValue;
            }
        }

        private static BooleanConfigField CreateBooleanConfigField(ConfigEntry<bool> configEntry)
        {
            return new BooleanConfigField(configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, OnValueChanged);
            
            bool ValueAccessor()
            {
                return configEntry.Value;
            }
            void OnValueChanged(bool newValue)
            {
                configEntry.Value = newValue;
            }
        }

        private static StringConfigField CreateStringConfigField(ConfigEntry<string> configEntry)
        {
            return new StringConfigField(configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, null, OnEndEdit);
            
            string ValueAccessor()
            {
                return configEntry.Value;
            }
            void OnEndEdit(string newValue)
            {
                configEntry.Value = newValue;
            }
        }

        private static ColorConfigField CreateColorConfigField(ConfigEntry<Color> configEntry)
        {
            return new ColorConfigField(configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, null, OnEndEdit);

            Color ValueAccessor()
            {
                return configEntry.Value;
            }
            void OnEndEdit(Color newValue)
            {
                configEntry.Value = newValue;
            }
        }

        private static IntConfigField CreateIntConfigField(ConfigEntry<int> configEntry)
        {
            if (configEntry.Description.AcceptableValues is AcceptableValueRange<int> acceptableValues)
            {
                return new IntConfigField(configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, null, OnEndEdit, acceptableValues.MinValue, acceptableValues.MaxValue);
            }

            return new IntConfigField(configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, null, OnEndEdit);

            int ValueAccessor()
            {
                return configEntry.Value;
            }
            void OnEndEdit(int newValue)
            {
                configEntry.Value = newValue;
            }
        }

        private static FloatConfigField CreateFloatConfigField(ConfigEntry<float> configEntry)
        {
            if (configEntry.Description.AcceptableValues is AcceptableValueRange<float> acceptableValues)
            {
                return new FloatConfigField(configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, null, OnEndEdit, acceptableValues.MinValue, acceptableValues.MaxValue);
            }

            return new FloatConfigField(configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, null, OnEndEdit);

            float ValueAccessor()
            {
                return configEntry.Value;
            }
            void OnEndEdit(float newValue)
            {
                configEntry.Value = newValue;
            }
        }

        private static EnumConfigField CreateEnumConfigField(ConfigEntryBase configEntry)
        {
            return new EnumConfigField(configEntry.SettingType, configEntry.Definition.Key, configEntry.Description.Description, ValueAccessor, OnEndEdit);

            Enum ValueAccessor()
            {
                return (Enum)configEntry.BoxedValue;
            }
            void OnEndEdit(Enum newValue)
            {
                configEntry.BoxedValue = newValue;
            }
        }
    }
}
