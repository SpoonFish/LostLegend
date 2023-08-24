using Android.Graphics;
using Android.Icu.Number;
using Android.Systems;
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


		}


		public override void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2(), float opacity = 0)
		{
			foreach (AnimatedTexture texture in ImageLayers)
			{

				texture.Draw(spriteBatch, Position*2 + offset, opacity, new Vector2(32, 60));
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
