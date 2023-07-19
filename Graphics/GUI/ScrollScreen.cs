using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostLegend.Graphics.GUI
{
    class ScrollScreen : GuiScreen
    {
        private Vector2 Position;
        private Vector2 Size;
        private Point StartingScroll; // The scroll wheel value before the screen was created. used to find the real change in scroll.
        public Vector2 CurrentScroll; //offset for drawing the components on this screen
        public Vector2 KeyboardScroll;
        private Vector2 MaxScroll;
        private Vector2 MinScroll;
        private Vector2 OriginalWheelScroll;
        private Rectangle ClickArea;
        private bool ScrollHorizontal;
        private bool ScrollVertical;
        private bool ScrollWheel;

        public ScrollScreen(Vector2 position, Vector2 size, Vector2 maxScroll, Vector2 minscroll, Point startingScroll = new Point(), bool scrollHorizontal = true, bool scrollVertical = true, bool usesScrollWheel= false) : base()
        {
            KeyboardScroll = Vector2.Zero;
            CurrentScroll = Vector2.Zero;
            StartingScroll = startingScroll;
            OriginalWheelScroll.X = Mouse.GetState().ScrollWheelValue;
            OriginalWheelScroll.Y = Mouse.GetState().HorizontalScrollWheelValue;
            Size = size;
            Position = position;
            ScrollWheel = usesScrollWheel;
            ClickArea = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            MaxScroll = maxScroll;
            MinScroll = minscroll;
            ScrollHorizontal = scrollHorizontal;
            ScrollVertical = scrollVertical;
            BackgroundComponents = new List<IGuiComponent>();
            BasicComponents = new List<IGuiComponent>();
            Buttons = new List<IGuiButton>();
            TextInputs = new List<TextInput>();
            FadingImages = new List<FadingImage>();
        }

        public void Update(Vector2 touchPos, bool isScreenTouched)
        {
            if (!ClickArea.Contains(touchPos))
                return;

            int verticalScroll = 0;
            int horizontalScroll = 0;
            if (ScrollWheel)
            {
                //verticalScroll = (mouseState.ScrollWheelValue - (int)OriginalWheelScroll.X) / 10;
//                horizontalScroll = (mouseState.HorizontalScrollWheelValue - (int)OriginalWheelScroll.Y) / 10;

            }

            KeyboardState currentKeys = Keyboard.GetState();

            if (currentKeys.IsKeyDown(Keys.W) || currentKeys.IsKeyDown(Keys.Up))
                KeyboardScroll.Y += 3;
            if (currentKeys.IsKeyDown(Keys.S) || currentKeys.IsKeyDown(Keys.Down))
                KeyboardScroll.Y -= 3;
            if (currentKeys.IsKeyDown(Keys.A) || currentKeys.IsKeyDown(Keys.Left))
                KeyboardScroll.X += 3;
            if (currentKeys.IsKeyDown(Keys.D) || currentKeys.IsKeyDown(Keys.Right))
                KeyboardScroll.X -= 3;


            if (StartingScroll == Point.Zero)
            {
                StartingScroll = new Point(horizontalScroll, verticalScroll);
            }
            // delta y, delta x scrolls
            float dyScroll = StartingScroll.Y - verticalScroll - KeyboardScroll.Y;
            float dxScroll = StartingScroll.X - horizontalScroll - KeyboardScroll.X;

            if (ScrollHorizontal == false)
                dxScroll = 0;
            if (ScrollVertical == false)
                dyScroll = 0;

            if (dyScroll < MinScroll.Y)
            {
                StartingScroll.Y -= (int)dyScroll - (int)MinScroll.Y;
                dyScroll = MinScroll.Y;
            }
            else if (dyScroll > MaxScroll.Y)
            {
                StartingScroll.Y -= (int)dyScroll - (int)MaxScroll.Y;
                dyScroll = MaxScroll.Y;

            }


            if (dxScroll < MinScroll.X)
            {
                StartingScroll.X -= (int)dxScroll - (int)MinScroll.X;
                dxScroll = MinScroll.X;
            }
            else if (dxScroll > MaxScroll.X)
            {
                StartingScroll.X -= (int)dxScroll - (int)MaxScroll.X;
                dxScroll = MaxScroll.X;

            }
            if (Math.Abs(dxScroll) + Math.Abs(dyScroll) == 0)
                return;
            CurrentScroll = new Vector2(-dxScroll, -dyScroll); //inverting polarity because offset gets added not deducted
        }
    }
}
