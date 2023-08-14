using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.World
{
	class DialogChoice
	{
		public string Text;
		public int NewTalkStage;
		public bool ExitTalking;
		public string Action;

		public DialogChoice(string text, int newTalkStage, bool exitTalking, string action)
		{
			Text = text;
			NewTalkStage = newTalkStage;
			ExitTalking = exitTalking;
			Action = action;
		}
	}
}
