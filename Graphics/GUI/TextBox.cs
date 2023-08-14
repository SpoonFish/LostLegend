using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Text;
using Android.Hardware.Lights;

namespace LostLegend.Graphics.GUI
{
    class TextBox : IGuiComponent

    {
        private Panel Box;
        public string Text;
        public Vector2 Position;
        private bool ThickOutline;
        private int Width;
        private int MaxWidth;
        private int MaxHeight;
        private string BoxType;
        private string Alignment;
        public List<LetterSprite> BoxImageText;

        public TextBox(string text, Vector2 position, int width, string boxType = "basic", string alignment = "left", bool thickOutline = false)
        {
            Alignment = alignment;
            BoxType = boxType;
            Text = text;
            if (!Text.EndsWith(' '))
                Text += ' ';
            Position = position;
            Width = width;
            ThickOutline = thickOutline;
            MaxWidth = 0;
            MaxHeight = 0;
            BoxImageText = new List<LetterSprite>();
            LoadText();
        }

        private void LoadText()
        {
            BoxImageText.Clear();
            int lineX = 0;
            int lineY = 0;
            int currentCharacterIndex = 0;
            int currentLineWidth = 0;
            int charactersInLine = 0;
            string colourString = "";
            Color currentColour = Color.White;
            bool searchingColour = false;

            
            for (int i = 0; i < Text.Length; i ++)
            {
                char letter = Text[i];
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
                    if ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!?.,:+-=%()/><' ".Contains(letter))
                        image = ContentLoader.FontDict[letter];

                    LetterSprite letterSprite = new LetterSprite(image, new Vector2(lineX, lineY), currentColour);
                    lineX += image.Texture.Width + image.Spacing;
                    currentLineWidth += image.Spacing;
                    charactersInLine += 1;
                    currentLineWidth += image.Texture.Width;
                    if ((lineX + image.Texture.Width > Width && letter == ' ') || letter == '\\' || i == Text.Length-1)
                    {
                        if (Alignment == "centre")
                            for (int j = 0; j < charactersInLine - 1; j++)
                            {
                                BoxImageText[currentCharacterIndex - 1 - j].Position.X += (Width - currentLineWidth) / 2;
                            }
                        lineX = 0;
                        charactersInLine = 0;
                        currentLineWidth = 0;
                        lineY += 20;
                       
                        continue;
                    }

                    if (MaxWidth < lineX)
                        MaxWidth = lineX;
                    if (MaxHeight < lineY + 20)
                        MaxHeight = lineY + 20;
                    BoxImageText.Add(letterSprite);
                    currentCharacterIndex += 1;

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
                if (!ThickOutline)
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
            else

                foreach (LetterSprite letterSprite in BoxImageText)
                {
                    Vector2 drawPos;
                    Rectangle sourceRectangle;
                    Rectangle destinationRectangle;
                    Texture2D image = letterSprite.Letter.Texture;
                    int width = image.Width;
                    int height = image.Height;
                    for (int j = -1; j < 6; j += 2)
                    {
                        Vector2 outlineOffset;
                        if (j < 2)
                            outlineOffset = new Vector2(j, 0);
                        else
                            outlineOffset = new Vector2(0, j-4);
                                
                        drawPos = new Vector2(Position.X + letterSprite.Position.X + offset.X+outlineOffset.X, Position.Y + letterSprite.Position.Y + offset.Y+outlineOffset.Y);


                        sourceRectangle = new Rectangle(0, 0, width, height);
                            
                        destinationRectangle = new Rectangle((int)drawPos.X, (int)drawPos.Y, width, height);

                        spriteBatch.Draw(image, destinationRectangle, sourceRectangle, letterSprite.Colour * opacity, 0, origin, SpriteEffects.None, 1);
                    }
                    sourceRectangle = new Rectangle(0, 0, width, height);
                    drawPos = new Vector2(Position.X + letterSprite.Position.X + offset.X, Position.Y + letterSprite.Position.Y + offset.Y);
                    destinationRectangle = new Rectangle((int)drawPos.X, (int)drawPos.Y, width, height);

                    spriteBatch.Draw(image, destinationRectangle, sourceRectangle, letterSprite.Colour * opacity, 0, origin, SpriteEffects.None, 1);
                }
        }
    }
}
