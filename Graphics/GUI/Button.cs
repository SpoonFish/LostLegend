﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Text;
using LostLegend.Graphics.GUI.Interactions;
using Microsoft.Xna.Framework.Input;
using Android.Transitions;

namespace LostLegend.Graphics.GUI
{
    class Button : IGuiButton

    {
        private Panel Box;
        public string Text;
        private string OrigText;
        private string HoverText;
        public Vector2 Position;
        public Vector2 TextOffset;
        private int Width;
        private int Height;
        private string BoxType;
        private string OrigBoxType;
        private Rectangle ClickArea;
        private string HoverBoxType;
        private string ClickBoxType;
        private bool CurrentlyClicked;
        private bool CurrentlyHovered;
        private ButtonSignalEvent Signal;
        public GuiTransition ComponentTransition;
        public List<LetterSprite> BoxImageText;

        public Button(string text, string hoverText, Vector2 position, Vector2 size, ButtonSignalEvent signal, string boxType = "bronze", GuiTransition transition = null, Vector2 textOffset = new Vector2())
        {
            if (transition == null)
                ComponentTransition = new GuiTransition();
            else
                ComponentTransition = transition;
            TextOffset = textOffset;
            Signal = signal;
            CurrentlyClicked = false;
            BoxType = boxType;
            OrigBoxType = boxType;
            HoverBoxType = boxType;
            ClickBoxType = boxType;
            Text = text;
            OrigText = text;
            HoverText = hoverText;
            Position = position;
            Width = (int)size.X;
            Height = (int)size.Y;
            ClickArea = new Rectangle((int)position.X-2, (int)position.Y-2, Width+4, Height+4);
            BoxImageText = new List<LetterSprite>();
            LoadText();
            LoadBoxType();
        }

        private void LoadText()
        {
            BoxImageText.Clear();
            int lineX = 0;
            string colourString = "";
            Color currentColour = Color.White;
            bool searchingColour = false;
            int lineWidth = 0;
            foreach (char letter in Text)
            {
                if (searchingColour)
                {
                    if (letter == '#')
                    {
                        searchingColour = false;
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
                    if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ.,:'!?-+1234567890%>< ".Contains(letter))
                        image = ContentLoader.FontDict[letter];
                    lineX += image.Texture.Width + image.Spacing;

                }

            }
            lineWidth = lineX;
            int margin = (Width - lineWidth) / 2;
            lineX = 0;
            colourString = "";
            currentColour = Color.White; searchingColour = false;
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
                            currentColour = ContentLoader.Colours["gray"];
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
                    if ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!?.,:+-=%()/><'>< ".Contains(letter))
                        image = ContentLoader.FontDict[letter];
                    LetterSprite letterSprite = new LetterSprite(image, new Vector2(lineX + margin, (Height - 17) / 2), currentColour);
                    lineX += image.Texture.Width + image.Spacing;
                    BoxImageText.Add(letterSprite);

                }

            }


        }

        public ButtonSignalEvent Update(Vector2 touchPos, Vector2 touchPos2, bool isScreenTouched, Point offset = new Point(), string newText = "")
        {
            if (newText != "")
            {
                Text = newText;
                LoadText();
            }
            Rectangle OffsetClickArea = new Rectangle(ClickArea.X + offset.X, ClickArea.Y + offset.Y, ClickArea.Width, ClickArea.Height);
            CurrentlyHovered = OffsetClickArea.Contains(touchPos);
            if (OffsetClickArea.Contains(touchPos))
            {
                BoxType = HoverBoxType;

                if (isScreenTouched && CurrentlyHovered == true)
                {
                    BoxType = ClickBoxType;
                    CurrentlyClicked = true;
                }
                else if(!isScreenTouched)
                    CurrentlyHovered = true;
                else
                    return new ButtonSignalEvent();


                if (Text != HoverText)
                {
                    Text = HoverText;
                    ReloadText(HoverText);
                }

                LoadBoxType();

                if (isScreenTouched && CurrentlyClicked)
                {
                    return Signal;
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
                if (Text != OrigText)
                {
                    Text = OrigText;
                    ReloadText(OrigText);
                }
            }
            return new ButtonSignalEvent();
        }

        public void ReloadText(string newText)
        {
            Text = newText;
            LoadText();
        }

        private void LoadBoxType()
        {
            switch (BoxType)
			{
				case "bronze_battle_category":
					Box = new Panel(ContentLoader.Images["bronze_battle_category"], new Vector2(Width, Height), 6);
					break;
				case "bronze":
                    Box = new Panel(ContentLoader.Images["bronze"], new Vector2(Width, Height), 6);
                    break;
                case "br_outline":
                    Box = new Panel(ContentLoader.Images["bronze_outline"], new Vector2(Width, Height), 3);
                    break;
				case "category_outline":
					Box = new Panel(ContentLoader.Images["category_outline"], new Vector2(Width, Height), 3);
					break;
				case "category_outline_sl":
					Box = new Panel(ContentLoader.Images["category_outline_selected"], new Vector2(Width, Height), 11);
					break;
				case "br_outline_round":
                    Box = new Panel(ContentLoader.Images["bronze_outline_round"], new Vector2(Width, Height), 4);
                    break;
                case "blue_outline_round":
                    Box = new Panel(ContentLoader.Images["blue_outline_round"], new Vector2(Width, Height), 4);
                    break;
                case "br_outline_round_light":
                    Box = new Panel(ContentLoader.Images["bronze_outline_round_light"], new Vector2(Width, Height), 4);
                    break;
                case "br_thick":
                    Box = new Panel(ContentLoader.Images["bronze_thick"], new Vector2(Width, Height), 8);
                    break;
                case "br_thin":
                    Box = new Panel(ContentLoader.Images["bronze_thin"], new Vector2(Width, Height), 4);
                    break;
                case "none":
                    Box = new Panel(ContentLoader.Images["no_box"], new Vector2(Width, Height), 1);
                    break;
                default:
                    Box = new Panel(ContentLoader.Images[BoxType], new Vector2(Width, Height), 3);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset = new Vector2())
        {
            Vector2 origin = new Vector2(0, 0);
            Box.Draw(spriteBatch, Position + offset + ComponentTransition.CurrentComponentPosOffset, ComponentTransition.CurrentComponentOpacity);
            foreach (LetterSprite letterSprite in BoxImageText)
            {
                Texture2D image = letterSprite.Letter.Texture;
                int width = image.Width;
                int height = image.Height;
                Vector2 drawPos = Position;
                drawPos = new Vector2((int)Position.X + (int)letterSprite.Position.X, (int)Position.Y + (int)letterSprite.Position.Y);
                drawPos += TextOffset;


                Rectangle sourceRectangle = new Rectangle(0, 0, width, height);

                Rectangle destinationRectangle = new Rectangle((int)drawPos.X + (int)offset.X + (int)ComponentTransition.CurrentComponentPosOffset.X, (int)drawPos.Y+(int)offset.Y + (int)ComponentTransition.CurrentComponentPosOffset.Y, width, height);

                spriteBatch.Draw(image, destinationRectangle, sourceRectangle, letterSprite.Colour * ComponentTransition.CurrentComponentOpacity, 0, origin, SpriteEffects.None, 1);
            }
        }
    }
}
