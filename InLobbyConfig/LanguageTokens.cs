using R2API;
using System.Collections.Generic;

namespace InLobbyConfig
{
    internal static class LanguageTokens
    {
        public const string IN_LOBBY_CONFIG_POPOUT_PANEL_NAME = nameof(IN_LOBBY_CONFIG_POPOUT_PANEL_NAME);

        public static void AddLanguageTokens()
        {
            AddEnglish();
            AddRussian();
        }

        private static void AddEnglish()
        {
            var dict = new Dictionary<string, string>
            {
                [IN_LOBBY_CONFIG_POPOUT_PANEL_NAME] = "Mods configuration"
            };
            LanguageAPI.Add(dict);
            LanguageAPI.Add(dict, "en");
        }

        private static void AddRussian()
        {
            var dict = new Dictionary<string, string>
            {
                [IN_LOBBY_CONFIG_POPOUT_PANEL_NAME] = "Конфигурация модов"
            };
            LanguageAPI.Add(dict, "RU");
        }
    }
}
