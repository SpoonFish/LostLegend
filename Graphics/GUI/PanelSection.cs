using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostLegend.Graphics.GUI
{
    class PanelSection
    {
        public Vector2 Size;
        public Vector2 StretchSize;
        public Vector2 Position;
        public Vector2 StretchPosition;
        public PanelSection(Vector2 position, Vector2 stretchPosition, Vector2 size, Vector2 stretchSize)
        {
            Position = position;
            StretchPosition = stretchPosition;
            Size = size;
            StretchSize = stretchSize;
        }
    }
}
