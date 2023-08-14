using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Spextria.StoredData
{
    class SaveFile
    {
        public string Name { get; set; }
        public int Coins { get; set; }
        public bool ShowIntro { get; set; }
        public bool New { get; set; }

        public int CurrentArea { get; set; }

        public List<int> DiscoveredAreas { get; set; }
        public List<string> CharacterAppearance { get; set; }
    }
}

