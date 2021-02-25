using InLobbyConfig.Fields;
using System.Linq;
using TMPro;

namespace InLobbyConfig.FieldControllers
{
    public class EnumFieldController : ConfigFieldController
    {
        public TextMeshProUGUI fieldText;
        public TMP_Dropdown dropdown;
        private EnumConfigField ConfigField { get; set; }

        public void Start()
        {
            ConfigField = configField as EnumConfigField;
            if (ConfigField == null)
            {
                return;
            }

            if (fieldText)
            {
                fieldText.text = ConfigField.DisplayName;
            }

            if (tooltipProvider)
            {
                tooltipProvider.SetContent(ConfigField.Tooltip);
            }

            foreach (var option in ConfigField.Options)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData(option));
            }

            var value = ConfigField.Values.IndexOf(ConfigField.GetValue());
            if (value == dropdown.value)
            {
                return;
            }
            dropdown.SetValueWithoutNotify(value);
        }

        public void OnValueChanged(int index)
        {
            if (ConfigField == null)
            {
                return;
            }
            ConfigField.OnValueChanged(ConfigField.Values.ElementAt(index));
        }
    }
}
