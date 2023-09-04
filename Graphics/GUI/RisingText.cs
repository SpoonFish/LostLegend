using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostLegend.Graphics.GUI
{
	public class RisingText
	{
		private Vector2 YOffset;
		public Vector2 Position;
		public string Text;
		private double FadeDuration;
		private double FadeTimer;
		public float Opacity;
		private TextBox TextBox;

		public RisingText(Vector2 position, string text, string colour, double duration)
		{
			Opacity = 1;
			YOffset = new Vector2();
			FadeTimer = -0.5;
			Position = position;
			Text = text;
			FadeDuration = duration;
			TextBox = new TextBox($"#{colour}#"+text, position - new Vector2(new Random().Next(-35, -10) + 10 * text.Length, 10), 1000, "none");
		}

		public void Update(double timePassed)
		{
			FadeTimer += timePassed;
			if (FadeTimer > 0)
			{
				Opacity = (float)-(FadeTimer / FadeDuration) + 1;
			}
			YOffset.Y -= (float)(60 * timePassed / 2);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			TextBox.Draw(spriteBatch, YOffset, Opacity);
		}

	}
}
