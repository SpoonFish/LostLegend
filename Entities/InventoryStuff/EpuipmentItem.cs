using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostLegend.Graphics;
using LostLegend.Statics;

namespace LostLegend.Entities.InventoryStuff

{
	class EquipmentItem : Item
	{
		public int Rarity;
		//public Stats lkol
		public EquipmentItem(string name, string desc, int sellValue, ItemInfo.ItemTypes type, AnimatedTexture texture): base(name, desc, sellValue, type, texture)
		{

		}
	}
}
