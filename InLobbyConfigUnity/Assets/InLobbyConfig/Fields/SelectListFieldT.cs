using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public class SelectListField<T> : SelectListField
    {
        protected new Func<IDictionary<T, string>> OptionsAccessor { get; }  
        protected new Action<T, int> OnItemAddedCallback { get; }
        protected new Action<int> OnItemRemovedCallback { get; }

        public SelectListField(string displayName, Func<ICollection<T>> valueAccessor, Action<T, int> onItemAdded, Action<int> onItemRemoved, Func<IDictionary<T, string>> optionsAccessor) : base(displayName, () => valueAccessor.Invoke().Cast<object>(), (value, index) => onItemAdded?.Invoke((T)value, index), onItemRemoved, () => optionsAccessor?.Invoke().ToDictionary(row => (object)row.Key, row => row.Value))
        {
            OnItemAddedCallback = onItemAdded;
            OnItemRemovedCallback = onItemRemoved;
            OptionsAccessor = optionsAccessor;
        }

        public SelectListField(string displayName, string tooltip, Func<ICollection<T>> valueAccessor, Action<T, int> onItemAdded, Action<int> onItemRemoved, Func<IDictionary<T, string>> optionsAccessor) : base(displayName, tooltip, () => valueAccessor.Invoke().Cast<object>(), (value, index) => onItemAdded?.Invoke((T)value, index), onItemRemoved, () => optionsAccessor?.Invoke().ToDictionary(row => (object)row.Key, row => row.Value))
        {
            OnItemAddedCallback = onItemAdded;
            OnItemRemovedCallback = onItemRemoved;
            OptionsAccessor = optionsAccessor;
        }

        public void OnItemAdded(T value, int index)
        {
            OnItemAddedCallback?.Invoke(value, index);
        }

        public new IDictionary<T, string> GetOptions()
        {
            return OptionsAccessor?.Invoke();
        }
    }
}
