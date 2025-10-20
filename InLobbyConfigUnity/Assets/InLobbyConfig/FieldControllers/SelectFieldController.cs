using InLobbyConfig.Components;
using InLobbyConfig.Fields;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace InLobbyConfig.FieldControllers
{
    public class SelectFieldController : ConfigFieldController
    {
        public TextMeshProUGUI fieldText;
        public SearchableDropdown dropdown;
        private SelectConfigField ConfigField { get; set; }
        private Dictionary<object, string> options;

        public void Start()
        {
            ConfigField = configField as SelectConfigField;
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

            options = ConfigField.GetOptions().ToDictionary(el => el.Key, el => el.Value);
            dropdown.Options = options.Select(el => new SearchableDropdown.OptionData(el.Key, el.Value)).ToList();
            (dropdown.OnItemSelected ?? (dropdown.OnItemSelected = new SearchableDropdown.DropdownEvent())).AddListener(AddNewItem);

            var value = ConfigField.GetValue();
            options.TryGetValue(value, out var text);
            dropdown.selectText.text = text ?? "{Missing option}";
        }

        public void AddNewItem(object value)
        {
            if (ConfigField == null)
            {
                return;
            }
            ConfigField.OnValueChanged(value);
            options.TryGetValue(value, out var text);
            dropdown.selectText.text = text ?? "{Missing option}";
        }
    }
}
