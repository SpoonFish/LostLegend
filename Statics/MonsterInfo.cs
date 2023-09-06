using LostLegend.Entities;
using LostLegend.Entities.InventoryStuff;
using LostLegend.Entities.Parts;
using LostLegend.Graphics.GUI;
using LostLegend.Graphics.GUI.Interactions;
using LostLegend.Master;
using LostLegend.World;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Statics
{
	static class MonsterInfo
	{
		static private Dictionary<string, MonsterEntity> MonsterDict;
		static public Dictionary<string, LootTable> LootTableDict;

		static public MonsterEntity GetMonster(string name, int indexPosition)
		{
			MonsterEntity entity = MonsterDict[name];
			return new MonsterEntity(entity.Texture, name, indexPosition, entity.BaseStats.Copy(), entity.Attack, name=="dead");
		}
		static public void LoadMonsters()
		{
			MonsterDict = new Dictionary<string, MonsterEntity>()
			{
				{"crab", new MonsterEntity(ContentLoader.Images["crab"], "crab", 0, new StatsHolder(20,20,0,0,2,4), AttackInfo.AttackDict["lol_slash"])},
				{"dead", new MonsterEntity(ContentLoader.Images["blackout"], "dead", 0, new StatsHolder(20,20,0,0,2,4), AttackInfo.AttackDict["lol_slash"], true)},
			};
			LootTableDict = new Dictionary<string, LootTable>()
			{
				{"crabb", new LootTable(
					new List<LootTableEntry>()
					{
						new LootTableEntry("crab meat", 0.5f, 1,2),
						new LootTableEntry("crab claw", 0.85f, 1),
					},
					false)},
				{"crab", new LootTable(
					new List<LootTableEntry>()
					{
						new LootTableEntry("palm log", 0.5f, 1,8),
						new LootTableEntry("leather tunic", 0.2f, 1,1),
						new LootTableEntry("leather leggings", 0.2f, 1,1),
					},
					false)},
			};
		}
	}
}
