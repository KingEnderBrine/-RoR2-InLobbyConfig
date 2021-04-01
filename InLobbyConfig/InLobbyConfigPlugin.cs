using BepInEx;
using BepInEx.Logging;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: R2API.Utils.ManualNetworkRegistration]
[assembly: EnigmaticThunder.Util.ManualNetworkRegistration]
namespace InLobbyConfig
{
    [BepInPlugin("com.KingEnderBrine.InLobbyConfig", "In Lobby Config", "1.3.0")]
    public class InLobbyConfigPlugin : BaseUnityPlugin
    {
        internal static InLobbyConfigPlugin Instance { get; private set; }
        internal static ManualLogSource InstanceLogger { get => Instance?.Logger; }

        private void Awake()
        {
            Instance = this;

            AssetBundleHelper.LoadAssetBundle();

            On.RoR2.UI.CharacterSelectController.Awake += Components.ConfigPanelController.CharacterSelectControllerAwake;
            On.RoR2.Language.LoadStrings += LanguageTokens.LoadStrings;
        }

        private void Destroy()
        {
            Instance = null;

            AssetBundleHelper.UnloadAssetBundle();

            On.RoR2.UI.CharacterSelectController.Awake -= Components.ConfigPanelController.CharacterSelectControllerAwake;
            On.RoR2.Language.LoadStrings -= LanguageTokens.LoadStrings;
        }
    }
}