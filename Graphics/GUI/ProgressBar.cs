using LostLegend.Graphics.GUI.Interactions;
using LostLegend.Graphics.GUI.Text;
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
    class ProgressBar
	{
		private Panel Box;
		private bool ColouredBar;
		public Vector2 Position;
		public Vector2 TextOffset;
		private int Width;
		private int Height;
		private string BoxType;
		public float Progress;
		private float MaxProgress;
		private AnimatedTexture BarTexture;
		private Color Colour;

		public ProgressBar(Vector2 position, Vector2 size, string boxType, AnimatedTexture barTexture, float maxProgress, bool colouredBar = false)
		{
			Colour = Color.White;
			ColouredBar = colouredBar;
			BarTexture = barTexture;
			Progress = 0;
			MaxProgress = maxProgress;
			BoxType = boxType;
			Position = position.ToPoint().ToVector2();
			Width = (int)size.X;
			Height = (int)size.Y;
			LoadBoxType();
		}


		public void Update(float progressValue, float newMaxProgress = -1)
		{
			if (newMaxProgress != -1)
				MaxProgress = newMaxProgress;
			Progress = progressValue / MaxProgress;
			float progress = progressValue / MaxProgress;

			if (ColouredBar)
			{

				Vector3 primaryColour = new Vector3(0, 1, 0);
				Vector3 secondaryColour = new Vector3(1, 0, 0);
				Vector3 colour = ((primaryColour * progress) + (secondaryColour * (-progress + 1))) * 2;
				Colour = new Color(colour.X, colour.Y, colour.Z);
			}
		}
		private void LoadBoxType()
		{
			switch (BoxType)
			{
				default:
					Box = new Panel(ContentLoader.Images[BoxType], new Vector2(Width, Height), 4);
					break;
			}
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
		{
			Vector2 origin = new Vector2(0, 0);
			Box.Draw(spriteBatch, Position + offset);


			BarTexture.Draw(spriteBatch, Position - new Vector2(0.5f,0.5f), 1, new Vector2(Width, Height), 0, null, Colour, new Rectangle(0, 0, (int)(Width * Progress), Height));
		}
	}
}
