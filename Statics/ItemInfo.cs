using LostLegend.Entities.InventoryStuff;
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
				{ "palm log", new Item("Palm Log", "Log of palm.", 2, ItemTypes.Material, ContentLoader.Images["palm_log"]) },
			};
		}
	}
}
