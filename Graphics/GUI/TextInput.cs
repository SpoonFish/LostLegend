using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Text;
using Microsoft.Xna.Framework.Input;
using Android.OS;
using LostLegend.Graphics.GUI.Interactions;
using Android.Hardware.Lights;
using System.Threading.Tasks;
using Android.Text.Style;
using Android.Views.InputMethods;
using System.Linq;

namespace LostLegend.Graphics.GUI
{
    class TextInput : IGuiComponent

    {
        private Panel Box;
        private string DefaultText;
        private string NoStyleDefaultText;
        public string Text;
        private string OrigText;
        public Vector2 Position;

        private int Width;
        private int MaxWidth;
        private int Padding;
        private int MaxHeight;
        public bool Valid;
        public bool AllowNumbers;
        public string PopupTitle;

        private int MinCharacters;
        private int MaxCharacters;

        private string OrigBoxType;
        private Rectangle ClickArea;
        private string HoverBoxType;
        private string ClickBoxType;
        private bool CurrentlyClicked;
        private bool CurrentlyHovered;
        private bool Active;
        private int Timer;
        private string Alignment;

        private string BoxType;
        public List<LetterSprite> BoxImageText;

        public TextInput(string defaultText, Vector2 position, int width, int maxCharacters, int minCharacters, string boxType = "bronze", string textAlign = "left", int padding = 0, bool allowNumbers = true, string popupTitle = "")

        {
            Alignment = textAlign;
            Valid = false;
            MinCharacters = minCharacters;
            MaxCharacters = maxCharacters;
            Active = false;
            Padding = padding;
            OrigText = "#lightgray#" + defaultText;
            OrigBoxType = boxType;
            HoverBoxType = boxType;
            ClickBoxType = boxType;
            BoxType = boxType;
            NoStyleDefaultText = defaultText;
            DefaultText = "#lightgray#" + defaultText;
            Position = position;
            Width = width;
            CurrentlyClicked = false;
            CurrentlyHovered = false;
            Text = "";
            MaxWidth = 0;
            Timer = 5;
            MaxHeight = 20;
            BoxImageText = new List<LetterSprite>();
            LoadText(DefaultText);
            ReloadText(DefaultText);

        }
        public async void Update(Vector2 touchPos, bool isScreenTouched, GameTime gameTime)
        {
            if (Text.Length >= MinCharacters && Text.Length <= MaxCharacters && Text != DefaultText)
                Valid = true;
            else
            {
                Valid = false;
            }
            if (!AllowNumbers && Text.Any(char.IsDigit))
                Valid = false;
                

            Rectangle OffsetClickArea = new Rectangle((int)Position.X-Padding, (int)Position.Y-Padding, Width+Padding*2, MaxHeight+Padding*2);
            CurrentlyHovered = OffsetClickArea.Contains(touchPos);
            if (OffsetClickArea.Contains(touchPos))
            {
                BoxType = HoverBoxType;

                if (isScreenTouched && CurrentlyHovered == true)
                {
                    BoxType = ClickBoxType;
                    CurrentlyClicked = true;
                }
                else if (!isScreenTouched)
                    CurrentlyHovered = true;
                else
                    return;


                LoadBoxType();

                if (isScreenTouched && CurrentlyClicked && !KeyboardInput.IsVisible)
                {
                    string extraText = ")";
                    if (!AllowNumbers)
                        extraText = ", You cannot use any numbers)";
                    await Task.Run(async () => {
                        var result = await KeyboardInput.Show(PopupTitle, $"Enter Text Below (Min length: {MinCharacters}, Max length: {MaxCharacters}" + extraText, ""); if (null != result)
                        {
                            //your method to set text goes here
                            ReloadText(result.Trim());
                        }
                    });
                    return;
                }
            }
            else
            {
                CurrentlyHovered = false;
                CurrentlyClicked = false;
                if (BoxType != OrigBoxType)
                {
                    BoxType = OrigBoxType;
                    LoadBoxType();
                }
            }
        }


        private void LoadText(string text)
        {
            BoxImageText.Clear();
            MaxHeight = 0;
            MaxWidth = Width;
            int lineX = 0;
            int lineY = 0;
            int currentCharacterIndex = 0;
            int currentLineWidth = 0;
            int charactersInLine = 0;
            string colourString = "";
            Color currentColour = Color.White;
            bool searchingColour = false;


            for (int i = 0; i < Text.Length; i++)
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
                    else
                        continue;

                    LetterSprite letterSprite = new LetterSprite(image, new Vector2(lineX, lineY), currentColour);
                    lineX += image.Texture.Width + image.Spacing;
                    currentLineWidth += image.Spacing;
                    charactersInLine += 1;
                    currentLineWidth += image.Texture.Width;
                    if ((lineX + image.Texture.Width > Width && letter == ' ') || letter == '\\')
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
                    }

                    if (MaxWidth < lineX)
                        MaxWidth = lineX;
                    if (MaxHeight < lineY + 20)
                        MaxHeight = lineY + 20;
                    BoxImageText.Add(letterSprite);
                    currentCharacterIndex += 1;

                }
            }
            Width += Padding * 2;
            MaxHeight += Padding * 2;
            switch (BoxType)
            {
                case "bronze":
                    Box = new Panel(ContentLoader.Images["bronze"], new Vector2(Width, MaxHeight), 6);
                    break;
                case "none":
                    Box = new Panel(ContentLoader.Images["no_box"], new Vector2(Width, MaxHeight), 1);
                    break;
            }

            Width -= Padding * 2;
            MaxHeight -= Padding * 2;

        }


        public void ReloadText(string newText)
        {
            if (newText == "")
                newText = DefaultText;
            Text = newText;
            LoadText(Text);
            
        }

        private void LoadBoxType()
        {
            Width += Padding * 2;
            MaxHeight += Padding * 2;
            switch (BoxType)
            {
                case "bronze":
                    Box = new Panel(ContentLoader.Images["bronze"], new Vector2(Width, MaxHeight), 6);
                    break;
                case "br_thick":
                    Box = new Panel(ContentLoader.Images["bronze_thick"], new Vector2(Width, MaxHeight), 8);
                    break;
                case "br_thin":
                    Box = new Panel(ContentLoader.Images["bronze_thin"], new Vector2(Width, MaxHeight), 4);
                    break;
                case "none":
                    Box = new Panel(ContentLoader.Images["no_box"], new Vector2(Width, MaxHeight), 1);
                    break;
                default:
                    Box = new Panel(ContentLoader.Images[BoxType], new Vector2(Width, MaxHeight), 3);
                    break;
            }

            Width -= Padding * 2;
            MaxHeight -= Padding * 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(0, 0);
            Box.Draw(spriteBatch, Position- new Vector2(Padding));
            foreach (LetterSprite letterSprite in BoxImageText)
            {
                Texture2D image = letterSprite.Letter.Texture;
                int width = image.Width;
                int height = image.Height;
                Vector2 drawPos = Position;
                drawPos = new Vector2((int)Position.X + (int)letterSprite.Position.X, (int)Position.Y + (int)letterSprite.Position.Y);
                //drawPos += TextOffset;


                Rectangle sourceRectangle = new Rectangle(0, 0, width, height);

                Rectangle destinationRectangle = new Rectangle((int)drawPos.X, (int)drawPos.Y, width, height);
                Color drawColour = letterSprite.Colour;
                if (!Valid && Text != "" && Text != DefaultText)
                    drawColour = ContentLoader.Colours["red"];
                spriteBatch.Draw(image, destinationRectangle, sourceRectangle, drawColour, 0, origin, SpriteEffects.None, 1);
            }
        }
    }
}