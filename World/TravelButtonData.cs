using LostLegend.Statics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.World
{
    class TravelButtonData
	{
		public MapInfo.MapIDs NextMap;
		public MapInfo.MapAreaIDs NextArea;
        public Vector2 Position;
        public string ButtonDirection;

        public TravelButtonData (MapInfo.MapAreaIDs nextArea, Vector2 position, string buttonDirection, MapInfo.MapIDs nextMap = MapInfo.MapIDs.Null)
        {
            NextMap = nextMap;
            NextArea = nextArea;
            Position = position;
            ButtonDirection = buttonDirection;
        }
    }
}
