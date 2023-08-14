using LostLegend.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.InventoryStuff
{
	class Inventory
	{
		public List<Item> Items;
		public int MaxItems;

		public Inventory (List<Item> items, int maxItems)
		{
			Items = items;
			MaxItems = maxItems;
		}

		public List<Item> GetOnlyMisc()
		{
			List < Item > misc = new List<Item> ();
			foreach (Item item in Items)
			{ 
				if (item.Type == Statics.ItemInfo.ItemTypes.Material || item.Type == Statics.ItemInfo.ItemTypes.Consumable)
					misc.Add (item);
			}

			return misc;
		}
		public List<Item> GetOnlyKey()
		{
			List<Item> key = new List<Item>();
			foreach (Item item in Items)
			{
				if (item.Type == Statics.ItemInfo.ItemTypes.Key)
					key.Add(item);
			}

			return key;
		}
		public List<EquipmentItem> GetOnlyArmour()
		{
			List<EquipmentItem> misc = new List<EquipmentItem>();
			foreach (Item item in Items)
			{
				if (item is EquipmentItem && ( item.Type == Statics.ItemInfo.ItemTypes.Legs || item.Type == Statics.ItemInfo.ItemTypes.Head || item.Type == Statics.ItemInfo.ItemTypes.Chest || item.Type == Statics.ItemInfo.ItemTypes.Accessory))
					misc.Add(item as EquipmentItem);
			}

			return misc;
		}
		public List<EquipmentItem> GetOnlyWeapons()
		{
			List<EquipmentItem> misc = new List<EquipmentItem>();
			foreach (Item item in Items)
			{
				if (item is EquipmentItem && (item.Type == Statics.ItemInfo.ItemTypes.Sword || item.Type == Statics.ItemInfo.ItemTypes.Wand || item.Type == Statics.ItemInfo.ItemTypes.Gloves || item.Type == Statics.ItemInfo.ItemTypes.Musical))
					misc.Add(item as EquipmentItem);
			}

			return misc;
		}

		public void AddItem(string item)
		{
			Items.Add(ItemInfo.ItemDict[item.ToLower().Replace('_',' ')]);
		}
	}
}
