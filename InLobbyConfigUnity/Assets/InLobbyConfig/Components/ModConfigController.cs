using InLobbyConfig.FieldControllers;
using System.Collections.Generic;
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

        public Button closeSearchButton;
        public Button searchButton;
        public TMP_InputField searchField;

        private readonly List<ConfigSectionController> sectionControllers = new List<ConfigSectionController>();
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

            if (configEntry.SectionFields.Count < 2)
            {
                searchButton.gameObject.SetActive(false);
                modNameText.rectTransform.anchoredPosition = new Vector2(-46, 0);
                modNameText.rectTransform.sizeDelta = new Vector2(-102, 0);
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
                sectionControllers.Add(sectionController);
            }

            initialized = true;
        }

        public void UpdateFilter(string value)
        {
            if (contentContainer && !contentContainer.activeSelf)
            {
                contentToggleButton.transform.GetComponentInChildren<FlipVertical>()?.Flip();
                contentContainer.SetActive(true);
            }

            foreach (var controller in sectionControllers)
            {
                var enabled = controller.sectionName.Contains(value, System.StringComparison.OrdinalIgnoreCase);
                controller.gameObject.SetActive(enabled);
            }
        }

        public void OpenSearch()
        {
            InitContent();

            if (contentContainer && !contentContainer.activeSelf)
            {
                contentToggleButton.transform.GetComponentInChildren<FlipVertical>()?.Flip();
                contentContainer.SetActive(true);
            }

            searchField.gameObject.SetActive(true);
            searchButton.gameObject.SetActive(false);
            closeSearchButton.gameObject.SetActive(true);

            searchField.Select();
        }

        public void CloseSearch()
        {
            searchField.text = null;
            searchField.gameObject.SetActive(false);

            closeSearchButton.gameObject.SetActive(false);
            searchButton.gameObject.SetActive(true);

            foreach (var controller in sectionControllers)
            {
                controller.gameObject.SetActive(true);
            }
        }
    }
}
