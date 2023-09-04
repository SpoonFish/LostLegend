
using LostLegend.Entities;
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
		private ProgressBar HealthBar;
		private ProgressBar TurnBar;
		private bool StillHovered;

		private bool Selected;
		private bool SelectedForAttack;

		public DisplayableMonster(Vector2 position, AnimatedTexture texture, int entityIndex)
		{
			TurnBar = new ProgressBar(position - new Vector2(0, 32), new Vector2(40, 6), "monster_turn_bar_small", ContentLoader.Images["turn_bar_small"], 1);
			HealthBar = new ProgressBar(position - new Vector2(0, 15), new Vector2(40, 6), "monster_hp_bar_small", ContentLoader.Images["hp_bar_small"], 1, true);
			StillHovered = false;
			Selected = false;
			SelectedForAttack = false;
			Position = position;
			EntityIndex = entityIndex;
			Texture = texture;
			Hitbox = new Rectangle((position).ToPoint(), new Point(Texture.Width  *2 , Texture.Height * 2));
		}


		public override void SetPos(Vector2 pos)
		{
			pos = pos.ToPoint().ToVector2();
			Position = pos;
			HealthBar.Position = pos - new Vector2(0, 15);
			TurnBar.Position = pos - new Vector2(0, 32);
		}
		public override void Update(MasterManager master, Vector2 offset)
		{
			MonsterEntity entity = master.entityManager.MonsterEntities[EntityIndex];
			HealthBar.Update(entity.BaseStats.Hp, entity.BaseStats.MaxHp);

			TurnBar.Update(entity.Cooldown, entity.MaxCooldown);

			Rectangle hitbox = new Rectangle((Position).ToPoint() + offset.ToPoint(), Hitbox.Size);
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
			{
				Selected = true;
				if (master.entityManager.Player.SelectedAttack != null)
					SelectedForAttack = true;
				else
					SelectedForAttack = false;
			}
			else
			{
				SelectedForAttack = false;
				Selected = false;
			}
		}
		public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2(), float opacity = 0)
		{
			if (!Selected)
				Texture.Draw(spriteBatch, Position + offset , opacity, new Vector2(Texture.Width*2, Texture.Height*2));
			else
				if (SelectedForAttack)
					Texture.Draw(spriteBatch, Position + offset, opacity, new Vector2(Texture.Width * 2, Texture.Height * 2), 0, "red");
				else
					Texture.Draw(spriteBatch, Position + offset, opacity, new Vector2(Texture.Width * 2, Texture.Height * 2),0,"green");
			HealthBar.Draw(spriteBatch, offset);
			TurnBar.Draw(spriteBatch, offset);
		}

	}
}
