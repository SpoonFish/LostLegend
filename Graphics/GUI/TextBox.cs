using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Text;

namespace LostLegend.Graphics.GUI
{
    class TextBox : IGuiComponent

    {
        private Panel Box;
        public string Text;
        public Vector2 Position;
        private int Width;
        private int MaxWidth;
        private int MaxHeight;
        private string BoxType;
        public bool ScreenFixed;
        public List<LetterSprite> BoxImageText;

        public TextBox(string text, Vector2 position, int width, string boxType = "basic", bool screenFixed = false)
        {
            BoxType = boxType;
            ScreenFixed = screenFixed;
            Text = text;
            Position = position;
            Width = width;
            MaxWidth = 0;
            MaxHeight = 0;
            ScreenFixed = screenFixed;
            BoxImageText = new List<LetterSprite>();
            LoadText();
        }

        private void LoadText()
        {
            BoxImageText.Clear();
            int lineX = 0;
            int lineY = 0;
            string colourString = "";
            Color currentColour = Color.White;
            bool searchingColour = false;

            
            foreach (char letter in Text)
            {
                if (searchingColour)
                {
                    if (letter == '#')
                    {
                        searchingColour = false;

                        try
                        {
                            currentColour = ContentLoader.Colours[colourString.ToLower()];
                        }
                        catch
                        {
                            currentColour = ContentLoader.Colours["grey"];
                        }
                        colourString = "";
                        continue;
                    }
                    colourString += letter;
                }
                else
                {
                    if (letter == '#')
                    {
                        searchingColour = true;
                        continue;
                    }
                    TextLetter image = ContentLoader.FontDict[' '];
                    if ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!?.,:+-=%()/ ".Contains(letter))
                        image = ContentLoader.FontDict[letter];


                    if ((lineX + image.Texture.Width > Width && letter == ' ') || letter == '\\')
                    {
                        lineX = 0;
                        lineY += 20;
                        continue;
                    }
                    LetterSprite letterSprite = new LetterSprite(image, new Vector2(lineX, lineY), currentColour);
                    lineX += image.Texture.Width + image.Spacing;
                    if (MaxWidth < lineX)
                        MaxWidth = lineX;
                    if (MaxHeight < lineY + 20)
                        MaxHeight = lineY + 20;
                    BoxImageText.Add(letterSprite);

                }
            }
            switch (BoxType)
            {
                case "bronze":
                    Box = new Panel(ContentLoader.Images["bronze"], new Vector2(Width, MaxHeight), 6);
                    break;
                case "none":
                    Box = new Panel(ContentLoader.Images["no_box"], new Vector2(Width, MaxHeight), 1);
                    break;
            }

            
        }

        public void ReloadText(string newText)
        {
            Text = newText;
            LoadText();
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2(), float opacity = 1)
        {
            Vector2 origin = new Vector2(0, 0);
            Box.Draw(spriteBatch, Position + offset, opacity);
            foreach (LetterSprite letterSprite in BoxImageText)
            {
                Texture2D image = letterSprite.Letter.Texture;
                int width = image.Width;
                int height = image.Height;
                Vector2 drawPos = new Vector2(Position.X + letterSprite.Position.X + offset.X, Position.Y + letterSprite.Position.Y + offset.Y);


                Rectangle sourceRectangle = new Rectangle(0, 0, width, height);

                Rectangle destinationRectangle = new Rectangle((int)drawPos.X, (int)drawPos.Y, width, height);

                spriteBatch.Draw(image, destinationRectangle, sourceRectangle, letterSprite.Colour * opacity, 0, origin, SpriteEffects.None, 1);
            }
        }
    }
}
