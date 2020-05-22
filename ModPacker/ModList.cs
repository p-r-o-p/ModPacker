using System;
using System.Collections.Generic;

namespace ModPacker
{
    class ModList
    {
        public string MinecraftVersion { get; set; }
        public List<string> Mods { get; set; }
        public string PackName { get; set; }
    }
}
