using System.Reflection;
using UnityEngine;

namespace InLobbyConfig
{
    internal static class AssetBundleHelper
    {
        public static AssetBundle MainAssetBundle { get; private set; }

        internal static void LoadAssetBundle()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("InLobbyConfig.kingenderbrine_inlobbyconfig"))
            {
                MainAssetBundle = AssetBundle.LoadFromStream(stream);
            }
        }

        internal static void UnloadAssetBundle()
        {
            if (!MainAssetBundle)
            {
                return;
            }
            MainAssetBundle.Unload(true);
        }

        internal static GameObject LoadPrefab(string name)
        {
            return MainAssetBundle?.LoadAsset<GameObject>($"Assets/Resources/Prefabs/{name}.prefab");
        }
    }
}
