using LostLegend.Entities.Parts;
using LostLegend.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Entities
{
	class MonsterEntity
	{
		public Vector2 Position;
		public StatsHolder BaseStats;
		public AnimatedTexture Texture;
		public MonsterEntity(AnimatedTexture texture, Vector2 position)
		{
			Texture = texture;
			Position = position;
			BaseStats = new StatsHolder(50, 50, 50, 50, 5, 5);
		}
	}
}
