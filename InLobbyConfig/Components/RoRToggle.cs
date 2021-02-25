using RoR2;
using UnityEngine;
using UnityEngine.UI;

namespace InLobbyConfig.Components
{
    public class RoRToggle : Toggle
    {
        private SelectionState previousState = SelectionState.Disabled;

        public Sprite onSprite;
        public Sprite offSprite;

        protected override void Start()
        {
            base.Start();

            onValueChanged.AddListener(ToggleImage);
            ToggleImage(isOn);
        }

        private void ToggleImage(bool newValue)
        {
            Util.PlaySound("Play_UI_menuClick", RoR2Application.instance.gameObject);
            image.sprite = newValue ? onSprite : offSprite;
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            if (previousState != state)
            {
                switch (state)
                {
                    case SelectionState.Highlighted:
                        Util.PlaySound("Play_UI_menuHover", RoR2Application.instance.gameObject);
                        break;
                }
                previousState = state;
            }
        }
    }
}
