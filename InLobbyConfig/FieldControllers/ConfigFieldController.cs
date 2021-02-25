using InLobbyConfig.Fields;
using RoR2.UI;
using UnityEngine;

namespace InLobbyConfig.FieldControllers
{
    public abstract class ConfigFieldController : MonoBehaviour
    {
        public TooltipProvider tooltipProvider;
        [HideInInspector]
        public IConfigField configField;
    }
}
