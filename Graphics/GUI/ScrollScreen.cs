using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LostLegend.Graphics.GUI
{
    class ScrollScreen : GuiScreen
    {
        private Vector2 AddedPrevScroll;
        private Vector2 AddedPrevScroll2;
        private Vector2 Position;
        private Vector2 Size;
        private Point StartingScroll; // The scroll wheel value before the screen was created. used to find the real change in scroll.
        //public Vector2 CurrentScroll; //offset for drawing the components on this screen
        public Vector2 KeyboardScroll;
        private Vector2 MaxScroll;
        private Vector2 MinScroll;
        private Vector2 OriginalWheelScroll;
        private Rectangle ClickArea;
        private bool FirstScroll;
        private bool ScrollHorizontal;
        private bool ScrollVertical;
        private bool ScrollWheel;

        public ScrollScreen(Vector2 position, Vector2 size, Vector2 maxScroll, Vector2 minscroll, Point startingScroll = new Point(), bool scrollHorizontal = true, bool scrollVertical = true, bool usesScrollWheel= false) : base()
        {
            KeyboardScroll = startingScroll.ToVector2();
            StartingScroll = startingScroll;
            CurrentScroll = startingScroll.ToVector2();
            OriginalWheelScroll.X = Mouse.GetState().ScrollWheelValue;
            OriginalWheelScroll.Y = Mouse.GetState().HorizontalScrollWheelValue;
            Size = size;
            Position = position;
            ScrollWheel = usesScrollWheel;
            ClickArea = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            MaxScroll = maxScroll;
            MinScroll = minscroll;
            AddedPrevScroll = new Vector2();
            AddedPrevScroll2 = new Vector2();
            ScrollHorizontal = scrollHorizontal;
            InteractionButtons = new List<MapInteractionButton>();
            ScrollVertical = scrollVertical;
            BackgroundComponents = new List<IGuiComponent>();
            BasicComponents = new List<IGuiComponent>();
            Buttons = new List<IGuiButton>();
            TextInputs = new List<TextInput>();
            FadingImages = new List<FadingImage>();
            FirstScroll = false;
        }

        public void Update(Vector2 touchPos, Vector2 scrollDif, float scrollMomentum)
        {
            if (ScrollHorizontal == false &&  ScrollVertical == false) return;
            if (scrollMomentum == 0)
            {
                return;
            }


            KeyboardScroll += scrollDif;


            // delta y, delta x scrolls

            if (ScrollHorizontal == false)
                KeyboardScroll.X = 0;
            if (ScrollVertical == false)
                KeyboardScroll.Y = 0;
            
            if (KeyboardScroll.Y < MinScroll.Y)
            {
                //StartingScroll.Y -= (int)dyScroll - (int)MinScroll.Y;
                KeyboardScroll.Y = MinScroll.Y;
            }
            else if (KeyboardScroll.Y > MaxScroll.Y)
            {
                //StartingScroll.Y -= (int)dyScroll - (int)MaxScroll.Y;
                KeyboardScroll.Y = MaxScroll.Y;

            }


            if (KeyboardScroll.X < MinScroll.X)
            {
                //StartingScroll.Y -= (int)dyScroll - (int)MinScroll.Y;
                KeyboardScroll.X = MinScroll.X;
            }
            else if (KeyboardScroll.X > MaxScroll.X)
            {
                //StartingScrol.Y -= (int)dyScroll - (int)MaxScroll.Y;
                KeyboardScroll.X = MaxScroll.X;

            }
            //if (Math.Abs(dxScroll) + Math.Abs(dyScroll) == 0)
              //  return;
            
            CurrentScroll = KeyboardScroll; //inverting polarity because offset gets added not deducted
        }
    }
}
