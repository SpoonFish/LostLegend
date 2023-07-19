using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Text;
using LostLegend.Graphics.GUI.Interactions;
using Microsoft.Xna.Framework.Input;

namespace LostLegend.Graphics.GUI
{
    class FadingImage
    {
        private ImagePanel Image;
        private string FadeType;
        private double FadeLength;
        private double MenuTriggerTime;
        private string NextMenu;
        public bool FreezeMenu;
        private bool BeginToFade;
        public bool DoneFading;
        public bool FadeOnExit;
        public bool FadeOnCommand;
        private double Timer;
        private double TimeToBeginFade;
        private float Opacity;
        public FadingImage(ImagePanel image, string fadeType, double fadeLength, bool freezeMenu = false, double timeToBeginFade = 0, bool fadeOnExit = false, bool fadeOnCommand = false, double timeToTriggerMenuChange = 0, string nextMenu = "")
        {
            NextMenu = nextMenu;
            MenuTriggerTime = timeToTriggerMenuChange;
            FadeOnCommand = fadeOnCommand;
            Image = image;
            FadeType = fadeType;
            FadeLength = fadeLength;
            FreezeMenu = freezeMenu;
            TimeToBeginFade = timeToBeginFade;
            FadeOnExit = fadeOnExit;
            if (FadeOnExit)
                FreezeMenu = true;
            BeginToFade = false;
            DoneFading = false;
            Timer = 0;
            if (FadeType == "in")
                Opacity = 0f;
            else
                Opacity = 1f;
            Image.Opacity = Opacity;
        }
        public void Fade()
        {
            BeginToFade = true;
        }
        public string Update(GameTime gameTime)
        {
            if (FreezeMenu)
            {
                if (FadeOnExit && BeginToFade)
                {
                    double secondsPast = gameTime.ElapsedGameTime.TotalSeconds;
                    Timer += secondsPast;
                    double timeProgress = Timer;
                    if (timeProgress > 0)
                    {
                        if (FadeType == "in")
                        {
                            Opacity = (float)Math.Min(timeProgress / FadeLength, 1);
                            if (Opacity == 1)
                            {
                                DoneFading = true;
                                FreezeMenu = false;
                            }
                        }
                        else
                        {
                            Opacity = 1 - (float)Math.Min(timeProgress / FadeLength, 1);
                            if (Opacity == 0)
                            {
                                DoneFading = true;
                                FreezeMenu = false;
                            }
                        }

                    }
                }
                else if (!FadeOnExit)
                {
                    double secondsPast = gameTime.ElapsedGameTime.TotalSeconds;
                    Timer += secondsPast;
                    double timeProgress = Timer - TimeToBeginFade;
                    if (timeProgress > 0)
                    {
                        if (FadeType == "in")
                        {
                            Opacity = (float)Math.Min(timeProgress / FadeLength, 1);
                            if (Opacity == 1)
                            {
                                DoneFading = true;
                                FreezeMenu = false;
                            }
                        }
                        else
                        {
                            Opacity = 1 - (float)Math.Min(timeProgress / FadeLength, 1);
                            if (Opacity == 0)
                            {
                                DoneFading = true;
                                FreezeMenu = false;
                            }
                        }

                    }
                }
            }
            else
            {
                double secondsPast = gameTime.ElapsedGameTime.TotalSeconds;
                Timer += secondsPast;
                double timeProgress = Timer - TimeToBeginFade;
                if (timeProgress > 0)
                {
                    if (FadeType == "in")
                        Opacity = (float)Math.Min(timeProgress / FadeLength, 1);
                    else
                        Opacity = 1 - (float)Math.Min(timeProgress / FadeLength, 1);

                }
            }

            if (Timer > MenuTriggerTime && MenuTriggerTime != 0)
                return NextMenu;

            Image.Opacity = Opacity;
            return "";
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
