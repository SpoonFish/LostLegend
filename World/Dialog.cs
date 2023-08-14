using Java.Lang.Annotation;
using LostLegend.Graphics.GUI;
using LostLegend.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;
using Microsoft.Xna.Framework;

namespace LostLegend.World
{
	class Dialog
	{

		public string Text;
		public bool Continues;
		public int NextDialogStage;
		public int NextNpcStage;
		public DialogChoice Choice1;
		public DialogChoice Choice2;


		public Dialog(string text, bool continues = true, int nextNpcStage = -1, int nextStage = -1, DialogChoice choice1 = null, DialogChoice choice2 = null)
		{
			Continues = continues;
			NextNpcStage = nextNpcStage;
			NextDialogStage = nextStage;
			Text = text;
			Choice1 = choice1;
			Choice2 = choice2;
		
		}
	}
}