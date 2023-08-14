using LostLegend.Master;
using LostLegend.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LostLegend.Entities.InventoryStuff;
using System.Threading.Tasks;

namespace LostLegend.Entities
{
    class PlayerEntity
    {
        public Inventory Inventory;
        public MapInfo.MapAreaIDs CurrentArea;

        public PlayerEntity(MasterManager master)
        {
            CurrentArea = (MapInfo.MapAreaIDs)master.storedDataManager.CurrentSaveFile.CurrentArea;
            Inventory = new Inventory(new List<Item>(), 20);
        }
    }
}
