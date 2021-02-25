using InLobbyConfig.Components;
using InLobbyConfig.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InLobbyConfig.FieldControllers
{
    public class SelectListFieldController : ConfigFieldController
    {
        public TextMeshProUGUI fieldText;
        public SearchableDropdown dropdown;
        public GameObject itemPrefab;
        public Button contentToggleButton;
        public Transform contentContainer;
        private SelectListField ConfigField { get; set; }
        private readonly List<SelectListItemController> items = new List<SelectListItemController>();
        private Dictionary<object, string> options;

        public void Start()
        {
            ConfigField = configField as SelectListField;
            if (ConfigField == null)
            {
                return;
            }

            options = ConfigField.GetOptions().ToDictionary(el => el.Key, el => el.Value);
            dropdown.Options = options.Select(el => new SearchableDropdown.OptionData(el.Key, el.Value)).ToList();
            (dropdown.OnItemSelected ?? (dropdown.OnItemSelected = new SearchableDropdown.DropdownEvent())).AddListener(AddNewItem);

            if (tooltipProvider)
            {
                tooltipProvider.SetContent(ConfigField.Tooltip);
            }

            if (fieldText)
            {
                fieldText.text = ConfigField.DisplayName;
            }

            var value = ConfigField.GetValue();
            foreach (var item in value)
            {
                AddNewItem(item, true);
            }
        }

        public void AddNewItem(object value)
        {
            AddNewItem(value, false);
            if (!contentContainer.gameObject.activeSelf)
            {
                contentToggleButton.onClick?.Invoke();
            }
        }

        public void AddNewItem(object value, bool skipNotification = false)
        {
            if (ConfigField == null)
            {
                return;
            }
            if (items.Any(item => item.value.Equals(value)))
            {
                return;
            }
            var itemGameObject = Instantiate(itemPrefab, contentContainer);

            var itemController = itemGameObject.GetComponent<SelectListItemController>();
            options.TryGetValue(value, out var text);
            itemController.textComponent.text = text ?? "{Missing option}";
            itemController.value = value;

            itemGameObject.SetActive(true);

            items.Add(itemController);
            if (skipNotification)
            {
                return;
            }
            ConfigField.OnItemAdded(value, itemGameObject.transform.GetSiblingIndex());
        }

        public void DeleteItem(int index)
        {
            if (ConfigField == null)
            {
                return;
            }
            items.RemoveAt(index);
            ConfigField.OnItemRemoved(index);
        }

        public void ToggleContent()
        {
            contentContainer.gameObject.SetActive(!contentContainer.gameObject.activeSelf);
        }
    }
}
