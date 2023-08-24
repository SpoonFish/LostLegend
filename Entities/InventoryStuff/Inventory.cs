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
		public List<Item > Items;
		public int MaxItems;
		public string SelectedItem;
		public string CurrentCategory;

		public int SelectedIndex;

		public Inventory (List<Item> items, int maxItems)
		{
			SelectedIndex = -1;
			CurrentCategory = "materials";
			Items = items;
			MaxItems = maxItems;
			SelectedItem = "";
		}

		public List<ItemIndexPair> GetOnlyMisc()
		{
			List < ItemIndexPair > misc = new List<ItemIndexPair> ();
			int i = 0;
			foreach (Item item in Items)
			{ 
				if (item.Type == Statics.ItemInfo.ItemTypes.Material || item.Type == Statics.ItemInfo.ItemTypes.Consumable)
					misc.Add (new ItemIndexPair(item, i));
				i++;
			}

			return misc;
		}
		public List<ItemIndexPair> GetOnlyKey()
		{
			List<ItemIndexPair> key = new List<ItemIndexPair>();
			int i = 0;
			foreach (Item item in Items)
			{
				if (item.Type == Statics.ItemInfo.ItemTypes.Key)
					key.Add(new ItemIndexPair(item, i));
				i++;
			}

			return key;
		}
		public List<ItemIndexPair> GetOnlyArmour()
		{
			List<ItemIndexPair> misc = new List<ItemIndexPair>();
			int i = 0;
			foreach (Item item in Items)
			{
				if (item is Item && ( item.Type == Statics.ItemInfo.ItemTypes.Legs || item.Type == Statics.ItemInfo.ItemTypes.Head || item.Type == Statics.ItemInfo.ItemTypes.Chest || item.Type == Statics.ItemInfo.ItemTypes.Accessory))
					misc.Add(new ItemIndexPair(item, i));
				i++;
			}

			return misc;
		}
		public List<ItemIndexPair> GetOnlyWeapons()
		{
			List<ItemIndexPair> misc = new List<ItemIndexPair>();
			int i = 0;
			foreach (Item item in Items)
			{
				if (item is Item && (item.Type == Statics.ItemInfo.ItemTypes.Sword || item.Type == Statics.ItemInfo.ItemTypes.Wand || item.Type == Statics.ItemInfo.ItemTypes.Gloves || item.Type == Statics.ItemInfo.ItemTypes.Musical))
					misc.Add(new ItemIndexPair(item, i));
				i++;
			}

			return misc;
		}

		public void AddItem(string item)
		{
			Items.Add(ItemInfo.ItemDict[item.ToLower().Replace('_',' ')]);
		}

		public Item ItemByIndex(int index)
		{
			if (index == -1)
				return null;
			return Items[index];
		}
	}
}
