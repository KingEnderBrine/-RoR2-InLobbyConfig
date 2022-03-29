using System.IO;
using System.Reflection;
using UnityEngine;

namespace InLobbyConfig
{
    internal static class AssetBundleHelper
    {
        public static AssetBundle MainAssetBundle { get; private set; }

        internal static void LoadAssetBundle()
        {
            MainAssetBundle = AssetBundle.LoadFromFile(GetBundlePath("kingenderbrine_inlobbyconfig"));
        }

        internal static void UnloadAssetBundle()
        {
            if (!MainAssetBundle)
            {
                return;
            }
            MainAssetBundle.Unload(true);
        }

        private static string GetBundlePath(string bundleName)
        {
            return Path.Combine(Path.GetDirectoryName(InLobbyConfigPlugin.Instance.Info.Location), bundleName);
        }

        internal static GameObject LoadPrefab(string name)
        {
            return MainAssetBundle?.LoadAsset<GameObject>($"Assets/Resources/Prefabs/{name}.prefab");
        }
    }
}
