
using Android.Icu.Number;
using Android.Systems;
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
	class DisplayablePlayer : GuiEntity
	{
		public Vector2 Position;
		public List<AnimatedTexture> ImageLayers;
		private ProgressBar HealthBar;
		private ProgressBar MpBar;

		public DisplayablePlayer(Vector2 position, MasterManager master)
		{
			Position = position;
			ImageLayers = new List<AnimatedTexture>();
			ImageLayers.Add(ContentLoader.Images["head_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]]);
			ImageLayers.Add(ContentLoader.Images["eyes_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[2]]);
			ImageLayers.Add(ContentLoader.Images["body_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]]);
			if (master.entityManager.Player.Equipment.GetLegs(master) != null)
				ImageLayers.Add(ContentLoader.Images["legs_" + master.entityManager.Player.Equipment.GetLegs(master).Name.Replace(' ', '_')]);
			else
				ImageLayers.Add(ContentLoader.Images["legs_gray_shorts"]);


			if (master.entityManager.Player.Equipment.GetChest(master) != null)
				ImageLayers.Add(ContentLoader.Images["chest_" + master.entityManager.Player.Equipment.GetChest(master).Name.Replace(' ', '_')]);
			else
				ImageLayers.Add(ContentLoader.Images["chest_leaf_shirt"]);

			if (master.entityManager.Player.Equipment.GetHead(master) != null)
				ImageLayers.Add(ContentLoader.Images["head_" + master.entityManager.Player.Equipment.GetHead(master).Name.Replace(' ', '_')]);
			else
				ImageLayers.Add(ContentLoader.Images[master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] + "_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]]);

			HealthBar = new ProgressBar(new Vector2(12, 8), new Vector2(160, 18), "monster_hp_bar_small", ContentLoader.Images["hp_bar"], master.entityManager.Player.BaseStats.MaxHp, master.entityManager.Player.BaseStats.Hp,  new Color(0, 255, 0), new Color(255, 0, 0));

			MpBar = new ProgressBar(new Vector2(12, 38), new Vector2(160, 18), "monster_hp_bar_small", ContentLoader.Images["hp_bar"], master.entityManager.Player.BaseStats.MaxMp, master.entityManager.Player.BaseStats.Hp, new Color(50, 50,255), new Color(125, 0, 125));


		}

		public override void SetPos(Vector2 pos)
		{
			Position = pos;
		}

		public override void Update(MasterManager master, Vector2 offset)
		{
			PlayerEntity player = master.entityManager.Player;
			HealthBar.Update(player.BaseStats.Hp, player.BaseStats.MaxHp);
			MpBar.Update(player.BaseStats.Hp, player.BaseStats.MaxMp);
		}
		public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2(), float opacity = 0)
		{
			foreach (AnimatedTexture texture in ImageLayers)
			{

				texture.Draw(spriteBatch, Position + offset, opacity, new Vector2(32, 60));
			}
			if (opacity == 1)
			{

				HealthBar.Draw(spriteBatch, offset);
				MpBar.Draw(spriteBatch, offset);
			}
			//Vector2 origin = new Vector2(0, 0);

			//Texture2D image = Image.Texture;
			//Vector2 drawPos = Position;


			//Rectangle sourceRectangle = new Rectangle(0, 0, image.Width, image.Height);

			//Rectangle destinationRectangle = new Rectangle((int)drawPos.X, (int)drawPos.Y, (int)Size.X, (int)Size.Y);

			//spriteBatch.Draw(image, destinationRectangle, sourceRectangle, Color.White, 0, origin, SpriteEffects.None, 1);

		}
	}
}
