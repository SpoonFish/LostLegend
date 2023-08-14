using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Master;

namespace LostLegend.Graphics.GUI
{
    class GuiContent
    {
        public GuiScreen MainScreen;
        public List<GuiScreen> Screens;
        public GuiContent()
        {
            Screens = new List<GuiScreen>();
            MainScreen = new GuiScreen();
        }

        public void Draw(SpriteBatch spriteBatch, MasterManager master)
        {
            foreach (ScrollScreen screen in Screens)
            {
                Vector2 offset = screen.CurrentScroll;
                foreach (IGuiComponent component in screen.BackgroundComponents)
                {
                    component.Draw(spriteBatch, offset);
                }
                foreach (TextInput component in screen.TextInputs)
                {
                    component.Draw(spriteBatch);//doubt we will ever use offset for these components
                }
                foreach (IGuiButton component in screen.Buttons)
                {
                    component.Draw(spriteBatch, offset);
                }
                foreach (IGuiComponent component in screen.BasicComponents)
                {
                    component.Draw(spriteBatch, offset);
                }
                foreach (MapInteractionButton component in screen.InteractionButtons)
                {
                    component.Draw(spriteBatch, master, offset);
                }

                foreach (FadingImage component in screen.FadingImages)
                {
                    component.Draw(spriteBatch);
                }
            }

            foreach (IGuiComponent component in MainScreen.BackgroundComponents)
            {
                component.Draw(spriteBatch);
            }
            foreach (TextInput component in MainScreen.TextInputs)
            {
                component.Draw(spriteBatch);
            }
            foreach (IGuiButton component in MainScreen.Buttons)
            {
                component.Draw(spriteBatch);
            }
            foreach (IGuiComponent component in MainScreen.BasicComponents)
            {
                component.Draw(spriteBatch);
            }
            foreach (MapInteractionButton component in MainScreen.InteractionButtons)
            {
                component.Draw(spriteBatch, master);
            }

            foreach (FadingImage component in MainScreen.FadingImages)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}