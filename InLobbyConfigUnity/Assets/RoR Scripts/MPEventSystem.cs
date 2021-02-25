using UnityEngine.EventSystems;

namespace RoR2.UI
{
	public class MPEventSystem : EventSystem
	{
		public enum InputSource
		{
			MouseAndKeyboard = 0,
			Gamepad = 1,
		}

		public int cursorOpenerCount;
		public int playerSlot;
		public TooltipProvider currentTooltipProvider;
		public TooltipController currentTooltip;
	}
}
