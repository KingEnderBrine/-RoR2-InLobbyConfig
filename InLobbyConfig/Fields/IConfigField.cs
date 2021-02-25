using RoR2.UI;
using UnityEngine;

namespace InLobbyConfig.Fields
{
    public interface IConfigField
    {
        GameObject FieldPrefab { get; }
        string DisplayName { get; }
        TooltipContent Tooltip { get; }

        object GetValue();
    }
}
