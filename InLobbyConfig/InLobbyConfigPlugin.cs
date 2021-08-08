using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace InLobbyConfig
{
    [BepInDependency("com.KingEnderBrine.ScrollableLobbyUI", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.KingEnderBrine.InLobbyConfig", "In Lobby Config", "1.3.2")]
    public class InLobbyConfigPlugin : BaseUnityPlugin
    {
        internal static InLobbyConfigPlugin Instance { get; private set; }
        internal static ManualLogSource InstanceLogger { get => Instance?.Logger; }
        internal static bool IsScrollableLobbyUILoaded { get; private set; }

        private void Awake()
        {
            Instance = this;

            IsScrollableLobbyUILoaded = Chainloader.PluginInfos.ContainsKey("com.KingEnderBrine.ScrollableLobbyUI");

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