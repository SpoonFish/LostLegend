using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Text;
using Microsoft.Xna.Framework.Input;

namespace LostLegend.Graphics.GUI
{
    class TextInput : IGuiComponent

    {
        private Panel Box;
        private string DefaultText;
        public string Text;
        private string OrigText;
        public Vector2 Position;

        private int Width;
        private int MaxWidth;
        private int MaxHeight;
        public bool Valid;

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

        private string BoxType;
        public bool ScreenFixed;
        public List<LetterSprite> BoxImageText;

        public TextInput(string defaultText, Vector2 position, int width, int maxCharacters, int minCharacters, string boxType = "button", string hoverBoxType = "button_hovered", string clickBoxType = "button_clicked", bool screenFixed = true)

        {
            
            Valid = false;
            MinCharacters = minCharacters;
            MaxCharacters = maxCharacters;
            Active = false;
            OrigText = "#grey#" + defaultText.ToUpper();
            OrigBoxType = boxType;
            HoverBoxType = hoverBoxType;
            ClickBoxType = clickBoxType;
            BoxType = boxType;
            ScreenFixed = screenFixed;
            DefaultText = "#grey#" + defaultText.ToUpper();
            Position = position;
            Width = width;
            Text = "";
            MaxWidth = 0;
            Timer = 5;
            MaxHeight = 0;
            ScreenFixed = screenFixed;
            BoxImageText = new List<LetterSprite>();
            LoadText(DefaultText);
        }
        public void OnInput(object sender, TextInputEventArgs e)
        {
            var k = e.Key;
            var c = e.Character;
            Text += c.ToString().ToUpper();
            LoadText(Text);
        }

        public static void Type(System.EventHandler<TextInputEventArgs> method, GameWindow window)
        {
            //window.TextInput += method;
        }
        public static void UnType(System.EventHandler<TextInputEventArgs> method, GameWindow window)
        {

           // window.TextInput -= method;
        }
        public void Update(Vector2 touchPos, bool isScreenTouched, GameTime gameTime)
        {


            if (Active)
            {
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                string prevText = Text;
                Timer -= 1;
                foreach (Keys key in keys)
                {
                    char typedChar = (char)key;
                    if (Timer < 0)
                    {
                        Timer = 10;
                        if (key == Keys.Back)
                        {
                            if (Text.Length > 0)
                                Text = Text.Remove(Text.Length - 1, 1);
                        }
                        else
                        {
                            if ((char)key == (char)192)
                                typedChar = '%';
                            if (!"ABCDEFGHIJKLMNOPQRSTUVWXYZ.,:'!?-+1234567890 %".Contains(typedChar))
                                continue;
                            Text += typedChar;
                        }
                    }
                    break;

                }
                Text = Text.TrimStart();
                string suffix = "#grey#X";
                if (prevText != Text)
                {
                    string prefix = "";
                    if (Text.Length < MinCharacters || Text.Length > MaxCharacters || Text.EndsWith(' '))
                    {
                        Valid = false;
                        prefix = "#red#";
                    }
                    else
                        Valid = true;
                    LoadText(prefix + Text + suffix);
                }
            }
                
            if (ClickArea.Contains(touchPos))
            {
                BoxType = HoverBoxType;

                if (isScreenTouched)
                {
                    Text = "";
                    DefaultText = "";
                    BoxType = ClickBoxType;
                    CurrentlyClicked = true;
                    ReloadText("");
                }
                else if (!isScreenTouched)
                    CurrentlyHovered = true;



                LoadBoxType();

                if (isScreenTouched)
                {
                    Active = true;
                }
            }
            else
            {
                CurrentlyHovered = false;


                if (isScreenTouched)
                {
                    DefaultText = OrigText;
                    LoadText(DefaultText);
                    Active = false;
                    BoxType = OrigBoxType;
                    LoadBoxType();
                }
                if (BoxType == HoverBoxType && Active == false)
                {
                    BoxType = OrigBoxType;
                    LoadBoxType();
                }
            }
        }


        private void LoadText(string text)
        {
            BoxImageText.Clear();
            int lineX = 0;
            int lineY = 0;
            string colourString = "";
            Color currentColour = Color.White;
            bool searchingColour = false;


            foreach (char letter in text)
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
                    if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ.,:'!?-+1234567890% ".Contains(letter))
                        image = ContentLoader.FontDict[letter];
                    else
                        continue;


                    if (lineX + image.Texture.Width > Width && letter == ' ')
                    {
                        lineX = 0;
                        lineY += 20;
                        continue;
                    }
                    LetterSprite letterSprite = new LetterSprite(image, new Vector2(lineX, lineY), currentColour);
                    lineX += image.Texture.Width + image.Spacing;
                    if (MaxWidth < lineX)
                        MaxWidth = lineX;
                    if (MaxHeight < lineY + 17)
                        MaxHeight = lineY + 17;
                    ClickArea = new Rectangle((int)Position.X - 2, (int)Position.Y - 2, MaxWidth + 4, MaxHeight + 4);
                    BoxImageText.Add(letterSprite);

                }
            }
            switch (BoxType)
            {
                case "button":
                    Box = new Panel(ContentLoader.Images["button"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_hovered":
                    Box = new Panel(ContentLoader.Images["button_hovered"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_clicked":
                    Box = new Panel(ContentLoader.Images["button_clicked"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_red":
                    Box = new Panel(ContentLoader.Images["button_red"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_red_hovered":
                    Box = new Panel(ContentLoader.Images["button_red_hovered"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_red_clicked":
                    Box = new Panel(ContentLoader.Images["button_red_clicked"], new Vector2(Width, MaxHeight), 3);
                    break;
            }



        }


        public void ReloadText(string newText)
        {
            Text = newText;
            LoadText(Text);
        }

        private void LoadBoxType()
        {
            switch (BoxType)
            {
                case "button":
                    Box = new Panel(ContentLoader.Images["button"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_hovered":
                    Box = new Panel(ContentLoader.Images["button_hovered"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_clicked":
                    Box = new Panel(ContentLoader.Images["button_clicked"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_red":
                    Box = new Panel(ContentLoader.Images["button_red"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_red_hovered":
                    Box = new Panel(ContentLoader.Images["button_red_hovered"], new Vector2(Width, MaxHeight), 3);
                    break;
                case "button_red_clicked":
                    Box = new Panel(ContentLoader.Images["button_red_clicked"], new Vector2(Width, MaxHeight), 3);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(0, 0);
            Box.Draw(spriteBatch, Position);
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

                spriteBatch.Draw(image, destinationRectangle, sourceRectangle, letterSprite.Colour, 0, origin, SpriteEffects.None, 1);
            }
        }
    }
}