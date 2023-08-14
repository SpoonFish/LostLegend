using System;
using System.Collections.Generic;
using System.Text;
using LostLegend.Graphics.GUI.Interactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace LostLegend.Graphics.GUI
{
    interface IGuiButton
    {
        public void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
        }

        virtual public ButtonSignalEvent Update(Vector2 touchPos, bool isScreenTouched, Point offset = new Point(), string newText = "")
        {
            return new ButtonSignalEvent();
        }
    }
}
