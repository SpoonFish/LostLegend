
using LostLegend.Master;
using LostLegend.Statics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Graphics.GUI
{
	class DisplayableMonster : GuiEntity
	{
		public Vector2 Position;
		public AnimatedTexture Texture;
		public int EntityIndex;
		private Rectangle Hitbox;

		private bool StillHovered;

		private bool Selected;

		public DisplayableMonster(Vector2 position, AnimatedTexture texture, int entityIndex)
		{
			StillHovered = false;
			Selected = false;
			Position = position;
			EntityIndex = entityIndex;
			Texture = texture;
			Hitbox = new Rectangle((position*2).ToPoint(), new Point(Texture.Width * 2, Texture.Height * 2));
		}

		public override void Update(MasterManager master, Vector2 offset)
		{
			Rectangle hitbox = new Rectangle((Position*2).ToPoint() + offset.ToPoint(), Hitbox.Size);
			if (hitbox.Contains(master.TouchPos) && master.IsScreenTouched && !StillHovered)
			{

				if (master.entityManager.SelectedMonsterIndex == EntityIndex)
					master.entityManager.SelectedMonsterIndex = -1;
				else
					master.entityManager.SelectedMonsterIndex = EntityIndex;
				StillHovered = true;

			}
			else if (hitbox.Contains(master.TouchPos) && !master.IsScreenTouched && StillHovered)
				StillHovered = false;

			if (master.entityManager.SelectedMonsterIndex == EntityIndex)
				Selected = true;
			else
				Selected = false;
		}
		public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2(), float opacity = 0)
		{
			if (!Selected)
				Texture.Draw(spriteBatch, Position * 2 + offset , opacity, new Vector2(Texture.Width*2, Texture.Height*2));
			else
				Texture.Draw(spriteBatch, Position * 2 + offset, opacity, new Vector2(Texture.Width * 2, Texture.Height * 2),0,"green");
		}

	}
}
