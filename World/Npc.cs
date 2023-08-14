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
	class Npc

	{

		public string Name;
		public List<List<Dialog>> Dialogs;
		public AnimatedTexture Texture;
		public int TalkStage;
		public int SavedTalkStage;
		public int CurrentDialog;

		public Npc(string name, List<List<Dialog>> dialogs, AnimatedTexture texture, int talkStage)
		{
			CurrentDialog = 0;
			Name = name;
			Dialogs = dialogs;
			Texture = texture;
			TalkStage = talkStage;
			SavedTalkStage = talkStage;
		}
	}
}