using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{
	class StatusEffectHolder
	{
		public List<StatusEffect> Effects;

		public StatusEffectHolder (List<StatusEffect> effects)
		{
			Effects = effects;
		}
	}
}
