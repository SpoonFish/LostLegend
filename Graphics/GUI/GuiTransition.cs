
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
    class GuiTransition
    {
        private bool Active;
        private bool HasFade;
        private Vector2 Translation; // position will start with this translation for convenience purposes (so i dont have to like subtract the vector from the orignal position manually)
        private double Duration;
        private double StartTimeSinceMenuLoad;
        private double Timer;
        public float CurrentComponentOpacity;
        public Vector2 CurrentComponentPosOffset;


        public GuiTransition(bool hasFade = false, double duration = 1, Vector2 translation = new Vector2(), double startTimeSinceMenuLoad = 0)
        {
            Active = true;
            Timer = 0;
            Translation = translation;
            HasFade = hasFade;
            Duration = duration;
            StartTimeSinceMenuLoad = startTimeSinceMenuLoad;
            CurrentComponentOpacity = 1;
            if (HasFade)
            {
                CurrentComponentOpacity = 0;
            }

            CurrentComponentPosOffset = new Vector2();
        }

        public void Update(double timePassed)
        {
            if (!Active)
                return;
            Timer += timePassed;
            if (Timer > StartTimeSinceMenuLoad)
            {
                double transitionProgress = (Timer - StartTimeSinceMenuLoad) / Duration;

                if (transitionProgress > 1)
                {
                    Active = false;
                    transitionProgress = 1;
                }
                if (HasFade)
                    CurrentComponentOpacity = (float)transitionProgress;

                if (Translation != Vector2.Zero)
                {
                    CurrentComponentPosOffset = Translation * (1+(-1*(float)transitionProgress));
                }

            }
        }
    }
}
