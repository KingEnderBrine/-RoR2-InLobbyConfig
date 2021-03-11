using BepInEx;
using BepInEx.Logging;
using R2API;
using R2API.Utils;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace InLobbyConfig
{
    [R2APISubmoduleDependency(nameof(LanguageAPI))]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync)]
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin("com.KingEnderBrine.InLobbyConfig", "In Lobby Config", "1.2.1")]
    public class InLobbyConfigPlugin : BaseUnityPlugin
    {
        internal static InLobbyConfigPlugin Instance { get; private set; }
        internal static ManualLogSource InstanceLogger { get => Instance?.Logger; }

        private void Awake()
        {
            Instance = this;

            LanguageTokens.AddLanguageTokens();
            AssetBundleHelper.LoadAssetBundle();

            On.RoR2.UI.CharacterSelectController.Awake += Components.ConfigPanelController.CharacterSelectControllerAwake;
        }

        private void Destroy()
        {
            Instance = null;

            AssetBundleHelper.UnloadAssetBundle();
            On.RoR2.UI.CharacterSelectController.Awake -= Components.ConfigPanelController.CharacterSelectControllerAwake;
        }
    }
}