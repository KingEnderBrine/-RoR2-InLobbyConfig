using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InLobbyConfig
{
    public static class ModConfigCatalog
    {
        private static readonly HashSet<ModConfigEntry> entries = new HashSet<ModConfigEntry>();
        public static ReadOnlyCollection<ModConfigEntry> ReadonlyList => new ReadOnlyCollection<ModConfigEntry>(entries.ToList());

        public static void Add(ModConfigEntry entry)
        {
            if (entry == null)
            {
                return;
            }
            entries.Add(entry);
        }

        public static void Remove(ModConfigEntry entry)
        {
            entries.Remove(entry);
        }
    }
}
