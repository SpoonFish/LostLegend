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
	static class ItemInfo
	{
		public enum ItemTypes
		{
			Material,
			Key,
			Consumable,
			Sword,
			Gloves,
			Wand,
			Musical,
			Head,
			Chest,
			Legs,
			Accessory
		};

		static public Dictionary<string, Item> ItemDict;

		static public void LoadItems()
		{
			ItemDict = new Dictionary<string, Item>()
			{
				{ "palm log", new Item("palm log", "Log of palm.", 2, ItemTypes.Material, ContentLoader.Images["palm_log"]) },
				{ "guard helm", new Item("guard helm", "A great disguise", 2, ItemTypes.Head, ContentLoader.Images["guard_helm"], true, stats: new StatsHolder(
					strength: 2,
					defence: 8
					)) },
				{ "palm hat", new Item("palm hat", "Funny goober hat.", 2, ItemTypes.Head, ContentLoader.Images["palm_hat"], true, stats: new StatsHolder(
					maxHp: 3,
					defence: 3
					)) },
				{ "palm sword", new Item("palm sword", "Pointy ouch.", 2, ItemTypes.Sword, ContentLoader.Images["palm_sword"], true, stats: new StatsHolder(
					strength: 3,
					maxHp: 2
					)) },
			};
		}
	}
}
