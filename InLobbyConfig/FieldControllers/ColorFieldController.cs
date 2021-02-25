using InLobbyConfig.Fields;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InLobbyConfig.FieldControllers
{
    public class ColorFieldController : ConfigFieldController
    {
        public enum ColorPart { R, G, B, A }
        
        public TextMeshProUGUI fieldText;
        public Image preview;
        public TMP_InputField colorR;
        public TMP_InputField colorG;
        public TMP_InputField colorB;
        public TMP_InputField colorA;

        private ColorConfigField ConfigField { get; set; }
        private Color32 currentColor;

        public void Start()
        {
            ConfigField = configField as ColorConfigField;
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

            if (!ConfigField.ShowAlpha)
            {
                colorA.transform.parent.gameObject.SetActive(false);
            }

            //Starting coroutine to avoid an annoying bug with text allignment
            StartCoroutine(DelayedStartCoroutine());
        }

        private IEnumerator DelayedStartCoroutine()
        {
            yield return new WaitForSeconds(0.01f);

            currentColor = ConfigField.GetValue();

            ProcessColor(currentColor.r, colorR);
            ProcessColor(currentColor.g, colorG);
            ProcessColor(currentColor.b, colorB);
            ProcessColor(currentColor.a, colorA);
            preview.color = currentColor;

            void ProcessColor(byte color, TMP_InputField inputField)
            {
                var textColor = color.ToString();
                if (textColor != inputField.text)
                {
                    inputField.SetTextWithoutNotify(textColor);
                }
            }
        }

        public void OnValueChangedR(string newValue) => OnValueChanged(newValue, ColorPart.R); 
        public void OnValueChangedG(string newValue) => OnValueChanged(newValue, ColorPart.G); 
        public void OnValueChangedB(string newValue) => OnValueChanged(newValue, ColorPart.B); 
        public void OnValueChangedA(string newValue) => OnValueChanged(newValue, ColorPart.A);

        private void OnValueChanged(string newValue, ColorPart colorPart)
        {
            if (ConfigField == null)
            {
                return;
            }
            if (!int.TryParse(newValue, out var number))
            {
                return;
            }

            switch (colorPart)
            {
                case ColorPart.R:
                    if (ProcessColorPart(number, colorR))
                    {
                        return;
                    }
                    currentColor.r = (byte)number;
                    break;
                case ColorPart.G:
                    if (ProcessColorPart(number, colorG))
                    {
                        return;
                    }
                    currentColor.g = (byte)number;
                    break;
                case ColorPart.B:
                    if (ProcessColorPart(number, colorB))
                    {
                        return;
                    }
                    currentColor.b = (byte)number;
                    break;
                case ColorPart.A:
                    if (ProcessColorPart(number, colorA))
                    {
                        return;
                    }
                    currentColor.a = (byte)number;
                    break;
            }

            preview.color = currentColor;

            ConfigField.OnValueChanged(new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a));
        }

        public void OnEndEditR(string newValue) => OnEndEdit(newValue, ColorPart.R);
        public void OnEndEditG(string newValue) => OnEndEdit(newValue, ColorPart.G);
        public void OnEndEditB(string newValue) => OnEndEdit(newValue, ColorPart.B);
        public void OnEndEditA(string newValue) => OnEndEdit(newValue, ColorPart.A);

        private void OnEndEdit(string newValue, ColorPart colorPart)
        {
            if (ConfigField == null)
            {
                return;
            }
            if (!int.TryParse(newValue, out var number))
            {
                var isEmpty = string.IsNullOrEmpty(newValue);
                switch (colorPart)
                {
                    case ColorPart.R:
                        colorR.text = isEmpty ? "0" : currentColor.r.ToString();
                        break;
                    case ColorPart.G:
                        colorG.text = isEmpty ? "0" : currentColor.g.ToString();
                        break;
                    case ColorPart.B:
                        colorB.text = isEmpty ? "0" : currentColor.b.ToString();
                        break;
                    case ColorPart.A:
                        colorA.text = isEmpty ? "0" : currentColor.a.ToString();
                        break;
                }
            }
            var currentValue = ConfigField.GetValue();
            if (currentValue == currentColor)
            {
                return;
            }
            ConfigField.OnEndEdit(new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a));
        }

        private bool ProcessColorPart(int newValue, TMP_InputField inputField)
        {
            if (newValue > 255)
            {
                inputField.text = "255";
                return true;
            }
            if (newValue < 0)
            {
                inputField.text = "0";
                return true;
            }

            return false;
        }
    }
}
