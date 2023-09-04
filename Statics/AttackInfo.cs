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
	static class AttackInfo
	{
		static public Dictionary<string, Attack> AttackDict;

		static public void LoadAttacks()
		{
			AttackDict = new Dictionary<string, Attack>()
			{
				{"running_slash" , new Attack(260, new AttackAnimation(0.3f, 0.1,"move","knockback"), 0.8f, 0, 2)},
				{"lol_slash" , new Attack(260, new AttackAnimation(0.3f, 0.1,"move","knockback"), 0.8f, 0, 3)}
			};
		}
	}
}
