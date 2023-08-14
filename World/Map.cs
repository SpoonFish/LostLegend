using Android.Text;
using LostLegend.Graphics;
using LostLegend.Graphics.GUI;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LostLegend.Statics.MapInfo;

namespace LostLegend.World
{
    class Map
    {
        public string Name;
        public Size MapSize;
        public bool Scrolls;
        public Dictionary<MapAreaIDs, MapArea> MapAreas;

        public Map(Dictionary<MapAreaIDs, MapArea> mapAreas, string name, bool scrolls, Size size)
        {
            MapSize = size;
            Scrolls = scrolls;
            Name = name;
            MapAreas = mapAreas;
        }

    }
}
