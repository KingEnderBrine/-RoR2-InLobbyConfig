using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using InLobbyConfig.Components;
using MonoMod.RuntimeDetour;
using RoR2;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace InLobbyConfig
{
    [BepInDependency("com.KingEnderBrine.ScrollableLobbyUI", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.KingEnderBrine.InLobbyConfig", "In Lobby Config", "1.5.0")]
    public class InLobbyConfigPlugin : BaseUnityPlugin
    {
        internal static InLobbyConfigPlugin Instance { get; private set; }
        internal static ManualLogSource InstanceLogger { get => Instance?.Logger; }
        internal static bool IsScrollableLobbyUILoaded { get; private set; }

        private void Start()
        {
            Instance = this;

            IsScrollableLobbyUILoaded = Chainloader.PluginInfos.ContainsKey("com.KingEnderBrine.ScrollableLobbyUI");

            AssetBundleHelper.LoadAssetBundle();

            new Hook(typeof(CharacterSelectController).GetMethod(nameof(CharacterSelectController.Awake), (System.Reflection.BindingFlags)(-1)), typeof(ConfigPanelController).GetMethod(nameof(ConfigPanelController.CharacterSelectControllerAwake), (System.Reflection.BindingFlags)(-1)));

            Language.collectLanguageRootFolders += CollectLanguageRootFolders;
        }

        private static void CollectLanguageRootFolders(List<string> folders)
        {
            folders.Add(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Instance.Info.Location), "Language"));
        }
    }
}