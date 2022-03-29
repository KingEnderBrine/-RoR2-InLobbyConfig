using InLobbyConfig.Fields;
using TMPro;
using UnityEngine.UI;

namespace InLobbyConfig.FieldControllers
{
    public class BooleanFieldController : ConfigFieldController
    {
        public TextMeshProUGUI fieldText;
        public Toggle toggle;
        private BooleanConfigField ConfigField { get; set; }
        private bool skipValueChange;

        public void Start()
        {
            ConfigField = configField as BooleanConfigField;
            if (ConfigField == null)
            {
                return;
            }

            if (tooltipProvider)
            {
                tooltipProvider.SetContent(ConfigField.Tooltip);
            }

            if (fieldText)
            {
                fieldText.text = ConfigField.DisplayName;
            }

            var value = ConfigField.GetValue();
            if (value == toggle.isOn)
            {
                return;
            }
            skipValueChange = true;
            toggle.isOn = value;
        }

        public void OnValueChanged(bool newValue)
        {
            if (ConfigField == null)
            {
                return;
            }
            if (skipValueChange)
            {
                skipValueChange = false;
                return;
            }
            ConfigField.OnValueChanged(newValue);
        }
    }
}
