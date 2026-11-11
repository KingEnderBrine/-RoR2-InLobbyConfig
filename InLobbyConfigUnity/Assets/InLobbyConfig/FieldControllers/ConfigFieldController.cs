using System.Collections.Generic;
using InLobbyConfig.Fields;
using RoR2;
using RoR2.UI;
using UnityEngine;

namespace InLobbyConfig.FieldControllers
{
    public abstract class ConfigFieldController : MonoBehaviour
    {
        private static Dictionary<IConfigField, List<ConfigFieldController>> configInstances = new Dictionary<IConfigField, List<ConfigFieldController>>();
        public TooltipProvider tooltipProvider;
        [HideInInspector]
        public IConfigField configField;

        public virtual void Start()
        {
            if (configField is not null)
            {
                if (!configInstances.TryGetValue(configField, out var instances))
                {
                    configInstances[configField] = instances = new List<ConfigFieldController>();
                }
                instances.Add(this);
            }
        }

        public virtual void OnDestroy()
        {
            if (configField is not null && configInstances.TryGetValue(configField, out var instances))
            {
                instances.Remove(this);
                if (instances.Count == 0)
                {
                    configInstances.Remove(configField);
                }
            }
        }

        protected abstract void RefreshValue();

        internal static void RefreshValues(IConfigField config)
        {
            if (configInstances.TryGetValue(config, out var instances))
            {
                foreach (var instance in instances)
                {
                    instance.RefreshValue();
                }
            }
        }
    }
}
