using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace InLobbyConfig.FieldControllers
{
    public class SelectListItemController : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;
        [HideInInspector]
        public object value;

        public SelectListFieldController fieldController;

        public void DeleteButtonClick()
        {
            fieldController?.DeleteItem(transform.GetSiblingIndex());
            Destroy(gameObject);
        }
    }
}
