using LostLegend.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities
{
    class EntityManager
    {
        public PlayerEntity Player;

        public EntityManager(MasterManager master) 
        { 
        }

        public void LoadPlayer(MasterManager master)
        {
            Player = new PlayerEntity(master);
        }
     }
}
