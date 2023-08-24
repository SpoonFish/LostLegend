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
	static class StatusEffectInfo
	{
		public enum StatusEffectTypes
		{
			Burning,
			Poison,
			Freezing
		};

		static public Dictionary<StatusEffectTypes, StatusEffectDescriber> StatusEffectDict;

		static public void LoadStatusEffects()
		{
			StatusEffectDict = new Dictionary<StatusEffectTypes, StatusEffectDescriber>()
			{
				{ StatusEffectTypes.Burning, new StatusEffectDescriber("burning", 5, "lol ur on fire")},
			};
		}
	}
}
