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

		public Item(string name, string desc, int sellValue, ItemInfo.ItemTypes type, AnimatedTexture texture)
		{
			Name = name;
			Desc = desc;
			SellValue = sellValue;
			Type = type;
			Texture = texture;
		}
	}
}
