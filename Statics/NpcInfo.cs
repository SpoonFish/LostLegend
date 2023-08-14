using LostLegend.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Statics
{
	static class NpcInfo
	{
		public static Dictionary<string, Npc> Npcs;
		
		public static void LoadNpcInfo()
		{
			Npcs = new Dictionary<string, Npc>()
			{
				{"blacksmith", new Npc("Jerry", new List<List<Dialog>>(){
				new List<Dialog>{new Dialog("Hey im Jerry the Blacksmith. Haven't seen you in a while!", true, 1, 2) },
				new List<Dialog>{new Dialog("Nice to see you here again. Who wouldn't want to see Lithram's best (and only) blacksmith!", true, -1, 2)},
				new List<Dialog>{new Dialog("Are you interesting in buying something today?", false, -1, -1, new DialogChoice("Yes", -1, true, "blacksmith_shop"), new DialogChoice("No", -1, true,"exit")) }
				},
				ContentLoader.Images["blacksmith"],
				0
				) }
			};
		}
	}
}
