using System;
using System.Collections.Generic;
using System.Text;
using LostLegend;
using LostLegend.Master;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostLegend.States
{
    abstract class State
    {
        public abstract void Update(MasterManager master, GameTime gameTime, Game1 game, GraphicsDevice graphicsDevice);

        public abstract void Draw(MasterManager master, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    }
}
