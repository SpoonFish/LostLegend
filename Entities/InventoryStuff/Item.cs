using LostLegend.Entities.Parts;
using LostLegend.Graphics;
using LostLegend.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.InventoryStuff
{
	class Item
	{
		public string Name;
		public string Desc;
		public AnimatedTexture Texture;
		public ItemInfo.ItemTypes Type;
		public int SellValue;
		public bool IsEquipable;
		public bool IsConsumable;
		public StatsHolder EquipStats;
		public ConsumableStats ConsumeStats;

		public Item(string name, string desc, int sellValue, ItemInfo.ItemTypes type, AnimatedTexture texture, bool isEquipable = false, bool isConsumable = false, StatsHolder stats = null, ConsumableStats consumeStats = null)
		{
			Name = name;
			Desc = desc;
			SellValue = sellValue;
			Type = type;
			Texture = texture;
			IsConsumable = isConsumable;
			IsEquipable = isEquipable;
			EquipStats = stats;
			if (IsEquipable && EquipStats == null)
				EquipStats = new StatsHolder();
			ConsumeStats = consumeStats;
		}

		public string FormatStats()
		{
			string statString = "";
			if (EquipStats.MaxHp > 0)
				statString += $"+{EquipStats.MaxHp} HP, ";
			if (EquipStats.MaxMp > 0)
				statString += $"+{EquipStats.MaxMp} MP, ";
			if (EquipStats.Strength > 0)
				statString += $"+{EquipStats.Strength} STR, ";
			if (EquipStats.Defence > 0)
				statString += $"+{EquipStats.Defence} DEF, ";
			statString = statString.Remove(statString.Length-2, 2);
			return statString;
		}
	}
}
