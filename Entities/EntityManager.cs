using LostLegend.Master;
using LostLegend.Statics;
using Microsoft.Xna.Framework;
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
        public List<MonsterEntity> MonsterEntities;
        public int SelectedMonsterIndex;

        public EntityManager(MasterManager master) 
        {
            SelectedMonsterIndex = -1;
            MonsterEntities = new List<MonsterEntity>() { new MonsterEntity(ContentLoader.Images["crab"], new Vector2(150 - 10, 100 - 10)) };
        }

        public void LoadPlayer(MasterManager master)
        {
            Player = new PlayerEntity(master);
        }
     }
}
