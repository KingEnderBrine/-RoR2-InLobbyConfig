using RoR2;

namespace InLobbyConfig
{
    internal static class LanguageTokens
    {
        public const string IN_LOBBY_CONFIG_POPOUT_PANEL_NAME = nameof(IN_LOBBY_CONFIG_POPOUT_PANEL_NAME);

        public static void LoadStrings(On.RoR2.Language.orig_LoadStrings orig, Language self)
        {
            orig(self);

            switch (self.name.ToLower())
            {
                case "ru":
                    self.SetStringByToken(IN_LOBBY_CONFIG_POPOUT_PANEL_NAME, "Конфигурация модов");
                    break;
                default:
                    self.SetStringByToken(IN_LOBBY_CONFIG_POPOUT_PANEL_NAME, "Mods configuration");
                    break;
            }
        }
    }
}
