using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{
	class StatusEffectDescriber
	{
		public string Name;
		public string Desc;
		public int MaxPotence;

		public StatusEffectDescriber(string name, int maxPotence, string desc)
		{
		Name = name;
			Desc = desc;
				MaxPotence = maxPotence;
		}
	}
}
