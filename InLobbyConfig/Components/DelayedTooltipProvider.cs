using RoR2.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InLobbyConfig.Components
{
    public class DelayedTooltipProvider : TooltipProvider, IPointerEnterHandler, IPointerExitHandler
    {
        public float delayInSeconds = 0.5f;
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            var current = EventSystem.current as MPEventSystem;
            if (!current || !tooltipIsAvailable)
            {
                return;
            }
            StartCoroutine(DelayTooltipOpen(current, eventData));
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            var current = EventSystem.current as MPEventSystem;
            if (!current || !tooltipIsAvailable)
            {
                return;
            }
            TooltipController.RemoveTooltip(current, this);
        }

        private IEnumerator DelayTooltipOpen(MPEventSystem eventSystem, PointerEventData eventData)
        {
            yield return new WaitForSeconds(delayInSeconds);
            TooltipController.SetTooltip(eventSystem, this, eventData.position);
        }
    }
}
