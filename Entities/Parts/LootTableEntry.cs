using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{
	class LootTableEntry
	{
		public string Item;
		public int AmountMin;
		public int AmountMax;
		public float Chance;

		public LootTableEntry(string item, float chance, int amountMin=1, int amountMax = 1)
		{
			Item = item;
			Chance = chance;
			AmountMin = amountMin;
			AmountMax = amountMax;
		}
	}
}
