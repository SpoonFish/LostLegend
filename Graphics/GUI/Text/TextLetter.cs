using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LostLegend.Graphics.GUI.Text
{
    class TextLetter
    {
        public Texture2D Texture;
        public int Spacing;
        private char Character;

        public TextLetter(Texture2D texture, int spacing, char character)
        {
            Texture = texture;
            Spacing = spacing;
            Character = character;
        }
    }
}

