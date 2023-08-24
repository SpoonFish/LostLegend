using LostLegend.Master;
using LostLegend.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LostLegend.Entities.InventoryStuff;
using System.Threading.Tasks;
using LostLegend.Entities.Parts;
using Microsoft.Xna.Framework;

namespace LostLegend.Entities
{
    class PlayerEntity
    {
        public StatsHolder BaseStats;
        public Inventory Inventory;
        public EquipmentHolder Equipment;
        public MapInfo.MapAreaIDs CurrentArea;
		public Vector2 Position;
		public string SelectedAttack;

		public PlayerEntity(MasterManager master)
        {
			SelectedAttack = null;
			Position = new Vector2(150 - 8, 200);
            BaseStats = new StatsHolder(50,50,50,50,5,5);
            Equipment = new EquipmentHolder();
            CurrentArea = (MapInfo.MapAreaIDs)master.storedDataManager.CurrentSaveFile.CurrentArea;
            Inventory = new Inventory(new List<Item>(), 200);
        }


		public string FormatTotalStats()
		{
            StatsHolder stats = new StatsHolder();
            stats.Add(BaseStats);
            Item item = Inventory.ItemByIndex(Equipment.Head);
			if ( item != null && item.IsEquipable) {
                stats.Add(item.EquipStats);
            }

			item = Inventory.ItemByIndex(Equipment.Chest);
			if (item != null && item.IsEquipable)
			{
				stats.Add(item.EquipStats);
			}

			item = Inventory.ItemByIndex(Equipment.Legs);
			if (item != null && item.IsEquipable)
			{
				stats.Add(item.EquipStats);
			}


			item = Inventory.ItemByIndex(Equipment.Weapon);
			if (item != null && item.IsEquipable)
			{
				stats.Add(item.EquipStats);
			}

			item = Inventory.ItemByIndex(Equipment.Accessory);
			if (item != null && item.IsEquipable)
			{
				stats.Add(item.EquipStats);
			}



			return $"HP: {stats.MaxHp} ({stats.Hp}) MP: {stats.MaxMp} ({stats.Mp})\\STR: {stats.Strength} DEF: {stats.Defence}";
		}
	}
}
