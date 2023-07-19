using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LostLegend.Graphics
{
    public class AnimatedTexture
    {
        public Texture2D Texture;
        public int Types;
        public int Frames;
        private bool IsAnimated;
        private int CurrentType;
        public int CurrentFrame;
        private double FrameTimeCounter;
        public double FrameDuration;
        public string Name;

        public int Width;
        public int Height;

        public AnimatedTexture(Texture2D texture, int frames, double frameDuration = 1, int types = 1, string name = "")
        {
            Name = name;
            Texture = texture;

            // Each row will have a different type of animation (walking left on row 1, walking right on row 2, down 3, up 4, etc.)
            // Default 1 (only 1 type of animation most of the time)
            Types = types;

            // Each column contains a frame of a type of animation (column 1 has first frame of 'walk left' and 'walk right' etc.)
            Frames = frames;

            // How long in seconds each frame of the animation lasts in seconds
            FrameDuration = frameDuration;

            // Width and height of one frame of the image, has 1px transparent padding to solve cropping issues so width/height must be subtracted by frames/types
            if (Frames > 1)
                Width = (Texture.Width - Frames) / Frames;
            else
                Width = Texture.Width / Frames;
            if (Types > 1)
                Height = (Texture.Height - Types) / Types;
            else
                Height = Texture.Height / Types;

            if (FrameDuration == 0)
                IsAnimated = false;
            else
                IsAnimated = true;

            CurrentType = 0;
            CurrentFrame = 0;
            FrameTimeCounter = 0;
        }

        public void RandomiseFrame()
        {
            Random random = new Random();
            CurrentFrame = random.Next(1, Frames);
        }
        public void SetType(int type)
        {
            CurrentType = type;
        }
        public void Update(double timePassed)
        {
            if (IsAnimated)
            {
                FrameTimeCounter += timePassed;
                // increment the current frame of the current animation of the image when its counter goes above the set length of a frame, then reset the counter.
                if (FrameTimeCounter > FrameDuration)
                {
                    FrameTimeCounter -= FrameDuration;// doing this rather than setting the value to zero makes it less weird looking at low frame rates
                    CurrentFrame++;
                    if (CurrentFrame >= Frames)
                        CurrentFrame = 0;
                }
            }
            else
            {
                CurrentFrame = 0;
                CurrentType = 0;
            }
        }
     

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float opacity = 1f, Vector2 size = new Vector2(), float rotation = 0)
        {
            if (size == Vector2.Zero)
                size = new Vector2(Width, Height);
            Rectangle sourceRectangle;
            Rectangle destinationRectangle;


            if (IsAnimated)
            {

                // Get size and pos of a frame image as a Rect
                sourceRectangle = new Rectangle(Width * CurrentFrame + CurrentFrame, Height * CurrentType + CurrentType, Width, Height);

                // Get destination Rect where current frame image will be drawn
                destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            }
            else
            {
                // Simple Simpleton drawing
                sourceRectangle = new Rectangle(0, 0, Width, Height);

                destinationRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            }
            Vector2 origin;
            if (rotation == 0)
                origin = new Vector2(0, 0);
            else
                origin = new Vector2(destinationRectangle.Width / 2, destinationRectangle.Height / 2);

            if (opacity == 1)
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, rotation, origin, SpriteEffects.None, 1);
            else
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White * opacity, rotation, origin, SpriteEffects.None, 1);
        }
    }
}
