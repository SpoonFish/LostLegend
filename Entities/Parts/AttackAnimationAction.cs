using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities.Parts
{
	struct AttackAnimationAction
	{
		public string Action;
		public float Point;

		public AttackAnimationAction(string action, float point)
		{
			Action = action;
			Point = point;
		}
	}
}
