using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{

	class LootTable
	{
		public List<LootTableEntry> Possibilities;
		public bool ExclusiveOutcome;

		public LootTable(List<LootTableEntry> possibilities, bool exclusive)
		{
			Possibilities = possibilities;
			ExclusiveOutcome = exclusive;
		
		}
	}
}
