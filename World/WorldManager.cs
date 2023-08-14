using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.World
{
    class WorldManager
    {
        public double WorldMovingTime;
        public float Zoom;
        public string CurrentNpc;
        public Map CurrentMap;
        public WorldManager ()
        {
            CurrentMap = null;
            CurrentNpc = "";
            WorldMovingTime = 0;
            Zoom = 1;
        }
    }
}
