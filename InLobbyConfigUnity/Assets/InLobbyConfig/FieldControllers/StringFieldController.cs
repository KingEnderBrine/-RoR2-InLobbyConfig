using InLobbyConfig.Fields;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace InLobbyConfig.FieldControllers
{
    public class StringFieldController : ConfigFieldController
    {
        public TextMeshProUGUI fieldText;
        public TMP_InputField inputField;
        private StringConfigField ConfigField { get; set; }

        public void Start()
        {
            ConfigField = configField as StringConfigField;
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
            var value = ConfigField.GetValue();
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
            ConfigField.OnValueChanged(newValue);
        }

        public void OnEndEdit(string newValue)
        {
            if (ConfigField == null)
            {
                return;
            }
            if (ConfigField.GetValue() == newValue)
            {
                return;
            }
            ConfigField.OnEndEdit(newValue);
        }
    }
}
