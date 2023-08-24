using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Master;

namespace LostLegend.Graphics.GUI
{
    abstract class GuiEntity
    {
        public virtual void Update(MasterManager master, Vector2 currentScroll)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2(), float opacity = 1)
        {
        }
    }
}
