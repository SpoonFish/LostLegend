using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Text;
using LostLegend.Graphics.GUI.Interactions;
using Microsoft.Xna.Framework.Input;
using Android.Transitions;
using LostLegend.Master;

namespace LostLegend.Graphics.GUI
{
    class MapInteractionButton : IGuiButton

    {
        public Vector2 Position;
        private int Width;
        private int Height;
        private Rectangle ClickArea;
        private bool CurrentlyClicked;
        private bool CurrentlyHovered;
        private ButtonSignalEvent Signal;
        private AnimatedTexture Texture;

        public MapInteractionButton(Vector2 position, ButtonSignalEvent signal, string iconType, Vector2 size = new Vector2()) 
        {
            Signal = signal;
            CurrentlyClicked = false;
            Position = position;
            LoadTextureFromIconType(iconType);
            if (size != new Vector2())
            {

                Width = (int)size.X;
                Height = (int)size.Y;
            }
            else
            {
                Width = Texture.Width;
                Height = Texture.Height;
            }
            ClickArea = new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        private void LoadTextureFromIconType(string iconType)
        {
            switch (iconType)
            {
                case "lithram_house":
                    Texture = ContentLoader.Images["lithram_house_icon"];
                    break;
                default:
                    Texture = ContentLoader.Images[iconType+"_icon"];
                    break;

            }
        }

        public ButtonSignalEvent Update(Vector2 touchPos, Vector2 touchPos2, bool isScreenTouched, Point offset = new Point())
        {
            Rectangle OffsetClickArea = new Rectangle(ClickArea.X + offset.X, ClickArea.Y + offset.Y, ClickArea.Width, ClickArea.Height);
            CurrentlyHovered = OffsetClickArea.Contains(touchPos);
            if (OffsetClickArea.Contains(touchPos))
            {
                if (isScreenTouched && CurrentlyHovered == true)
                {
                    CurrentlyClicked = true;
                }
                else if(!isScreenTouched)
                    CurrentlyHovered = true;
                else
                    return new ButtonSignalEvent();




                if (isScreenTouched && CurrentlyClicked)
                {
                    return Signal;
                }
            }
            else
            {
                CurrentlyHovered = false;
                CurrentlyClicked = false;
            }
            return new ButtonSignalEvent();
        }

        public void Draw(SpriteBatch spriteBatch, MasterManager master, Vector2 offset = new Vector2())
        {
            Texture.Draw(spriteBatch, Position+offset, 1, new Vector2(Width, Height));
        }
    }
}
