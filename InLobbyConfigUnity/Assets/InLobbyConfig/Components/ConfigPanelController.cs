using RoR2.UI;
using RoR2.UI.SkinControllers;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace InLobbyConfig.Components
{
    public class ConfigPanelController : MonoBehaviour
    {
        public static GameObject CachedPrefab { get; private set; }

        public GameObject configButton;
        public GameObject popoutPanel;
        private GameObject scrollContent;

        private bool initialized;

        private void Awake()
        {
            var buttonComponent = configButton.GetComponent<HGButton>();
            buttonComponent.onClick.AddListener(TogglePopoutPanel);
        }

        private void Start()
        {
            if (!popoutPanel)
            {
                return;
            }

            var popoutController = popoutPanel.GetComponent<HGPopoutPanel>();

            popoutController.popoutPanelTitleText.token = LanguageTokens.IN_LOBBY_CONFIG_POPOUT_PANEL_NAME;
            popoutController.popoutPanelDescriptionText.transform.parent.gameObject.SetActive(false);

            var popoutChildLocator = popoutPanel.GetComponent<ChildLocator>();
            popoutChildLocator.FindChild("RandomButtonContainer")?.gameObject?.SetActive(false);

            var contentContainer = popoutController.popoutPanelContentContainer;
            DestroyImmediate(contentContainer.GetComponent<GridLayoutGroup>());

            var rectTransform = contentContainer.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(0, 700);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5F, 1);
            rectTransform.offsetMin = new Vector2();
            rectTransform.offsetMax = new Vector2();

            var layoutElement = contentContainer.gameObject.AddComponent<LayoutElement>();
            layoutElement.minHeight = 700;

            //If SLUI is loaded content container's height is limited to 425px, changing it to 700
            if (InLobbyConfigPlugin.IsScrollableLobbyUILoaded)
            {
                ModifyIfSLUILoaded(contentContainer);
            }

            contentContainer.gameObject.AddComponent<RectMask2D>();

            scrollContent = Instantiate(AssetBundleHelper.LoadPrefab("ScrollContent"), contentContainer.transform);
            var scrollbar = Instantiate(AssetBundleHelper.LoadPrefab("ScrollBar"), contentContainer.transform);

            var scrollRect = contentContainer.gameObject.AddComponent<ScrollRect>();
            scrollRect.content = scrollContent.GetComponent<RectTransform>();
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.viewport = contentContainer;
            scrollRect.scrollSensitivity = 30;
            scrollRect.verticalScrollbar = scrollbar.GetComponent<Scrollbar>();
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;

            if (popoutPanel.activeSelf)
            {
                InitContent();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ModifyIfSLUILoaded(Transform contentTransform)
        {
            var dynamicFitter = contentTransform.parent.parent.GetComponent<ScrollableLobbyUI.DynamicContentSizeFitter>();
            dynamicFitter.maxHeight = 700;
        }

        private void TogglePopoutPanel()
        {
            if (!popoutPanel)
            {
                return;
            }

            popoutPanel.SetActive(!popoutPanel.activeSelf);

            if (popoutPanel.activeSelf)
            {
                InitContent();
            }
        }

        private void InitContent()
        {
            if (initialized)
            {
                return;
            }

            foreach (var configEntry in ModConfigCatalog.ReadonlyList)
            {
                if (configEntry == null)
                {
                    InLobbyConfigPlugin.InstanceLogger.LogWarning($"Skipping over null ModConfigEntry");
                    continue;
                }
                if (configEntry.EnableField == null && (configEntry.SectionFields.Count == 0 || configEntry.SectionFields.All(row => row.Value.Count() == 0)))
                {
                    InLobbyConfigPlugin.InstanceLogger.LogWarning($"Skipping over `{configEntry.DisplayName}` because it has no fields assigned");
                    continue;
                }
                var modConfigInstance = Instantiate(AssetBundleHelper.LoadPrefab("ModConfigPrefab"), scrollContent.transform);
                if (!modConfigInstance)
                {
                    InLobbyConfigPlugin.InstanceLogger.LogWarning("Failed to instantiate `ModConfigPrefab`");
                    continue;
                }
                var configController = modConfigInstance.GetComponent<ModConfigController>();
                configController.configEntry = configEntry;
            }

            initialized = true;
        }

        internal static void CharacterSelectControllerAwake(Action<CharacterSelectController> orig, CharacterSelectController self)
        {
            orig(self);
            self.StartCoroutine(InitConfigPanel(self));
        }

        private static IEnumerator InitConfigPanel(CharacterSelectController self)
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            
            var leftHandPanel = self.transform.Find("SafeArea/LeftHandPanel (Layer: Main)");
            var rightHandPanel = self.transform.Find("SafeArea/RightHandPanel");

            if (!CachedPrefab)
            {
                CachePrefabFromSurvivorGrid(leftHandPanel, "SurvivorChoiceGrid, Panel");
            }

            var configPanel = GameObject.Instantiate(CachedPrefab, self.transform.Find("SafeArea"), false);
            var popoutPanelContainer = rightHandPanel.Find("PopoutPanelContainer");
            
            var popoutPanel  = GameObject.Instantiate(popoutPanelContainer.Find("PopoutPanelPrefab").gameObject, popoutPanelContainer);
            configPanel.GetComponent<ConfigPanelController>().popoutPanel = popoutPanel;

            var cscInputEvents = self.GetComponents<HGGamepadInputEvent>();

            var cscRightInputEventOne = cscInputEvents.First(el => el.actionName == "UIPageRight");
            cscRightInputEventOne.requiredTopLayer = leftHandPanel.GetComponent<UILayerKey>();

            var cscRightInputEventTwo = self.gameObject.AddComponent<HGGamepadInputEvent>();
            cscRightInputEventTwo.actionName = cscRightInputEventOne.actionName;
            cscRightInputEventTwo.actionEvent = cscRightInputEventOne.actionEvent;
            cscRightInputEventTwo.requiredTopLayer = leftHandPanel.Find("SurvivorInfoPanel, Active (Layer: Secondary)").GetComponent<UILayerKey>();
            cscRightInputEventTwo.enabledObjectsIfActive = Array.Empty<GameObject>();

            var ruleLayout = rightHandPanel.Find("RuleLayoutActive (Layer: Tertiary)");
            var ruleLayoutLayer = ruleLayout.GetComponent<UILayerKey>();

            var rlLeftInputEvent = self.GetComponents<HGGamepadInputEvent>().First(input => input.actionName == "UIPageLeft" && input.requiredTopLayer == ruleLayoutLayer);

            var rlRightInputEventOne = ruleLayout.gameObject.AddComponent<HGGamepadInputEvent>();
            rlRightInputEventOne.actionName = "UIPageRight";
            rlRightInputEventOne.actionEvent = configPanel.GetComponent<EventHolder>().unityEvent;
            rlRightInputEventOne.requiredTopLayer = ruleLayoutLayer;
            rlRightInputEventOne.enabledObjectsIfActive = Array.Empty<GameObject>();

            var rlRightInputEventTwo = ruleLayout.gameObject.AddComponent<HGGamepadInputEvent>();
            rlRightInputEventTwo.actionName = rlRightInputEventOne.actionName;
            rlRightInputEventTwo.actionEvent = rlLeftInputEvent.actionEvent;
            rlRightInputEventTwo.requiredTopLayer = rlRightInputEventOne.requiredTopLayer;
            rlRightInputEventTwo.enabledObjectsIfActive = Array.Empty<GameObject>();

            var configLeftInputEvent = configPanel.GetComponent<HGGamepadInputEvent>();
            configLeftInputEvent.actionEvent.AddListener(() => ruleLayout.gameObject.SetActive(true));

            var configCancelInputEvent = configPanel.AddComponent<HGGamepadInputEvent>();
            configCancelInputEvent.actionName = "UICancel";
            configCancelInputEvent.actionEvent = configLeftInputEvent.actionEvent;
            configCancelInputEvent.requiredTopLayer = configLeftInputEvent.requiredTopLayer;
            configCancelInputEvent.enabledObjectsIfActive = configLeftInputEvent.enabledObjectsIfActive;
        }

        private static void CachePrefabFromSurvivorGrid(Transform panel, string survivorGridName)
        {
            if (CachedPrefab)
            {
                return;
            }
            var survivorGrid = panel.Find(survivorGridName);

            CachedPrefab = AssetBundleHelper.LoadPrefab("ConfigPanel");

            CachedPrefab.GetComponent<Image>().sprite = panel.Find("BorderImage").GetComponent<Image>().sprite;
            var baseOutlineSprite = survivorGrid.Find("SurvivorIconPrefab/BaseOutline").GetComponent<Image>().sprite;
            var hoverOutlineSprite = survivorGrid.Find("SurvivorIconPrefab/HoverOutline").GetComponent<Image>().sprite;
            var buttonSkin = GameObject.Instantiate(survivorGrid.Find("WIPClassButtonPrefab").GetComponent<ButtonSkinController>().skinData);
            buttonSkin.buttonStyle.colors.normalColor = Color.white;
            foreach (var button in CachedPrefab.GetComponentsInChildren<HGButton>())
            {
                button.GetComponent<ButtonSkinController>().skinData = buttonSkin;
                button.transform.Find("BaseOutline").GetComponent<Image>().sprite = baseOutlineSprite;
                button.transform.Find("HoverOutline").GetComponent<Image>().sprite = hoverOutlineSprite;
            }
        }
    }
}
