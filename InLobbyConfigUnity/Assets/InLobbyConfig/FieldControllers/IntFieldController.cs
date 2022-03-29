using InLobbyConfig.Fields;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace InLobbyConfig.FieldControllers
{
    public class IntFieldController : ConfigFieldController
    {
        public TextMeshProUGUI fieldText;
        public TMP_InputField inputField;
        private IntConfigField ConfigField { get; set; }

        public void Start()
        {
            ConfigField = configField as IntConfigField;
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

            //Starting coroutine to avoid an annoying bug with text allignment
            StartCoroutine(DelayedStartCoroutine());
        }

        private IEnumerator DelayedStartCoroutine()
        {
            yield return new WaitForSeconds(0.01f);
            var value = ConfigField.GetValue().ToString(NumberFormatInfo.InvariantInfo);
            if (value == inputField.text)
            {
                yield break;
            }
            inputField.SetTextWithoutNotify(value);
        }

        public void OnValueChanged(string newValue)
        {
            if (ConfigField == null)
            {
                return;
            }
            if (!int.TryParse(newValue, out var number))
            {
                return;
            }
            if (ConfigField.Minimum.HasValue && ConfigField.Minimum > number)
            {
                inputField.text = ConfigField.Minimum.Value.ToString();
                return;
            }
            if (ConfigField.Maximum.HasValue && ConfigField.Maximum < number)
            {
                inputField.text = ConfigField.Maximum.Value.ToString();
                return;
            }
            ConfigField.OnValueChanged(number);
        }

        public void OnEndEdit(string newValue)
        {
            if (ConfigField == null)
            {
                return;
            }
            var currentValue = ConfigField.GetValue();
            if (!int.TryParse(newValue, out var number))
            {
                inputField.text = currentValue.ToString();
            }
            if (currentValue == number)
            {
                return;
            }
            ConfigField.OnEndEdit(number);
        }
    }
}
