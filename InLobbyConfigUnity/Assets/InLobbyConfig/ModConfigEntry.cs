using InLobbyConfig.Fields;
using System.Collections.Generic;

namespace InLobbyConfig
{
    public class ModConfigEntry
    {
        public string DisplayName { get; set; }
        public BooleanConfigField EnableField { get; set; }
        public Dictionary<string, IEnumerable<IConfigField>> SectionFields { get; } = new Dictionary<string, IEnumerable<IConfigField>>();
        public Dictionary<string, BooleanConfigField> SectionEnableFields { get; } = new Dictionary<string, BooleanConfigField>();
    }
}
