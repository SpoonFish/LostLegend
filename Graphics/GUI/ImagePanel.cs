using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Text;
using LostLegend.Graphics.GUI.Interactions;
using Microsoft.Xna.Framework.Input;

namespace LostLegend.Graphics.GUI
{
    class ImagePanel : IGuiComponent
    {
        public Vector2 Position;
        public Vector2 Size;
        public AnimatedTexture Image;
        public float Opacity;

        public ImagePanel(Vector2 position, AnimatedTexture image, Vector2 size = new Vector2(), float opacity = 1f)
        {
            Opacity = opacity; 
            Position = position;
            Image = image;

            if (size == new Vector2())
            {
                Size.X = image.Texture.Width;
                Size.Y = image.Texture.Height;
            }
            else
                Size = size;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2(), float opacity = 0)
        {
            Image.Draw(spriteBatch, Position + offset, Opacity, Size);
            //Vector2 origin = new Vector2(0, 0);
            
            //Texture2D image = Image.Texture;
            //Vector2 drawPos = Position;


            //Rectangle sourceRectangle = new Rectangle(0, 0, image.Width, image.Height);

            //Rectangle destinationRectangle = new Rectangle((int)drawPos.X, (int)drawPos.Y, (int)Size.X, (int)Size.Y);

            //spriteBatch.Draw(image, destinationRectangle, sourceRectangle, Color.White, 0, origin, SpriteEffects.None, 1);
            
        }
    }
}
