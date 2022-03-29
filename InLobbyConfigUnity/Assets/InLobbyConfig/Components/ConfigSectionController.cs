using InLobbyConfig.FieldControllers;
using InLobbyConfig.Fields;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InLobbyConfig.Components
{
    public class ConfigSectionController : MonoBehaviour
    {
        public BooleanFieldController enableButtonController;
        public Button contentToggleButton;
        public GameObject contentContainer;
        public string sectionName;
        public IEnumerable<IConfigField> fields;
        public BooleanConfigField enableField;
        public TextMeshProUGUI modNameText;

        private bool initialized = false;

        public void Start()
        {
            modNameText.text = $"[{sectionName}]";

            if (enableField == null)
            {
                enableButtonController.gameObject.SetActive(false);
            }
            else
            {
                enableButtonController.configField = enableField;
            }

            if (contentContainer.activeSelf)
            {
                InitContent();
            }
        }

        public void ToggleContent()
        {
            if (!contentContainer)
            {
                return;
            }

            contentContainer.SetActive(!contentContainer.activeSelf);

            if (contentContainer.activeSelf)
            {
                InitContent();
            }
        }

        private void InitContent()
        {
            if (initialized)
            {
                return;
            }

            foreach (var configField in fields)
            {
                if (!configField.FieldPrefab)
                {
                    InLobbyConfigPlugin.InstanceLogger.LogWarning($"FieldPrefab for `{configField.DisplayName}` of type `{configField.GetType()}` doesn't exists");
                    continue;
                }
                var fieldPrefab = Instantiate(configField.FieldPrefab, contentContainer.transform);
                if (!fieldPrefab)
                {
                    InLobbyConfigPlugin.InstanceLogger.LogWarning($"Failed to instantiate FieldPrefab for `{configField.DisplayName}`");
                    continue;
                }
                var fieldController = fieldPrefab.GetComponent<ConfigFieldController>();
                if (!fieldController)
                {
                    InLobbyConfigPlugin.InstanceLogger.LogWarning($"FieldPrefab for `{configField.DisplayName}` doesn't contain `ConfigFieldController`");
                    continue;
                }
                fieldController.configField = configField;
            }

            initialized = true;
        }
    }
}
