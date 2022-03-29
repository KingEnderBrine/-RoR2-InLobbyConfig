using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class SelectListField : BaseConfigField<IEnumerable<object>>
    {
        private static GameObject fieldPrefab;
        public override GameObject FieldPrefab => fieldPrefab ? fieldPrefab : (fieldPrefab = AssetBundleHelper.LoadPrefab("SelectListFieldPrefab"));

        protected Func<IDictionary<object, string>> OptionsAccessor { get; }  
        protected Action<object, int> OnItemAddedCallback { get; }
        protected Action<int> OnItemRemovedCallback { get; }

        public SelectListField(string displayName, Func<IEnumerable<object>> valueAccessor, Action<object, int> onItemAdded, Action<int> onItemRemoved, Func<IDictionary<object, string>> optionsAccessor) : base(displayName, valueAccessor, null)
        {
            OnItemAddedCallback = onItemAdded;
            OnItemRemovedCallback = onItemRemoved;
            OptionsAccessor = optionsAccessor;
        }

        public SelectListField(string displayName, string tooltip, Func<IEnumerable<object>> valueAccessor, Action<object, int> onItemAdded, Action<int> onItemRemoved, Func<IDictionary<object, string>> optionsAccessor) : base(displayName, tooltip, valueAccessor, null)
        {
            OnItemAddedCallback = onItemAdded;
            OnItemRemovedCallback = onItemRemoved;
            OptionsAccessor = optionsAccessor;
        }

        public void OnItemAdded(object value, int index)
        {
            OnItemAddedCallback?.Invoke(value, index);
        }

        public void OnItemRemoved(int index)
        {
            OnItemRemovedCallback?.Invoke(index);
        }

        public IDictionary<object, string> GetOptions()
        {
            return OptionsAccessor?.Invoke();
        }
    }
}
