using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.InventoryStuff
{
	class ItemIndexPair
	{
		public Item Item;

		public int Index;
		public ItemIndexPair(Item item, int index)
		{
			Item = item;
			Index = index;
		}
	}
}
