using InLobbyConfig.FieldControllers;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InLobbyConfig.Components
{
    public class ModConfigController : MonoBehaviour
    {
        public BooleanFieldController enableButtonController;
        public Button contentToggleButton;
        public GameObject contentContainer;
        public ModConfigEntry configEntry;
        public TextMeshProUGUI modNameText;

        private bool initialized = false;

        public void Start()
        {
            if (configEntry == null)
            {
                return;
            }

            modNameText.text = configEntry.DisplayName;

            if (configEntry.EnableField == null)
            {
                enableButtonController.gameObject.SetActive(false);
            }
            else
            {
                enableButtonController.configField = configEntry.EnableField;
            }

            if (configEntry.SectionFields.Count == 0 && configEntry.SectionFields.All(row => row.Value.Count() == 0))
            {
                contentToggleButton.gameObject.SetActive(false);
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

            foreach (var row in configEntry.SectionFields)
            {
                var sectionPrefab = Instantiate(AssetBundleHelper.LoadPrefab("SectionPrefab"), contentContainer.transform);
                var sectionController = sectionPrefab.GetComponent<ConfigSectionController>();
                sectionController.sectionName = row.Key;
                sectionController.fields = row.Value;
                configEntry.SectionEnableFields.TryGetValue(row.Key, out sectionController.enableField);
            }

            initialized = true;
        }
    }
}
