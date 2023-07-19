using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LostLegend.Graphics.GUI.Text
{
    class LetterSprite
    {
        public TextLetter Letter;
        public Vector2 Position;
        public Color Colour;

        public LetterSprite(TextLetter letter, Vector2 position, Color colour)
        {
            Letter = letter;
            Position = position;
            Colour = colour;
        }
    }
}

