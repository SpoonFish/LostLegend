using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostLegend.Graphics.GUI
{
    class Panel
    {
        private int Radius;
        private Texture2D Texture;
        private List<PanelSection> Sections;
        public Panel(AnimatedTexture AnimatedTexture, Vector2 size, int radius)
        {
            Radius = radius;
            Texture = AnimatedTexture.Texture;
            Sections = new List<PanelSection>();

            //top left
            PanelSection section = new PanelSection(new Vector2(0, 0), new Vector2(0, 0), new Vector2(radius, radius), new Vector2(radius, radius));
            Sections.Add(section);

            //top
            section = new PanelSection(new Vector2(radius, 0), new Vector2(radius, 0), new Vector2(1, radius), new Vector2(size.X, radius));
            Sections.Add(section);

            //top right
            section = new PanelSection(new Vector2(radius + 1, 0), new Vector2(radius + size.X, 0), new Vector2(radius, radius), new Vector2(radius, radius));
            Sections.Add(section);

            //mid left
            section = new PanelSection(new Vector2(0, radius), new Vector2(0, radius), new Vector2(radius, 1), new Vector2(radius, size.Y));
            Sections.Add(section);

            //mid
            section = new PanelSection(new Vector2(radius, radius), new Vector2(radius, radius), new Vector2(1, 1), new Vector2(size.X, size.Y));
            Sections.Add(section);

            //mid right
            section = new PanelSection(new Vector2(radius + 1, radius), new Vector2(radius + size.X, radius), new Vector2(radius, 1), new Vector2(radius, size.Y));
            Sections.Add(section);

            //bot left
            section = new PanelSection(new Vector2(0, radius + 1), new Vector2(0, radius + size.Y), new Vector2(radius, radius), new Vector2(radius, radius));
            Sections.Add(section);

            //bot
            section = new PanelSection(new Vector2(radius, radius + 1), new Vector2(radius, radius + size.Y), new Vector2(1, radius), new Vector2(size.X, radius));
            Sections.Add(section);

            //bot right
            section = new PanelSection(new Vector2(radius + 1, radius + 1), new Vector2(radius + size.X, radius + size.Y), new Vector2(radius, radius), new Vector2(radius, radius));
            Sections.Add(section);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float opacity = 1)
        {
            foreach (PanelSection section in Sections)
            {
                Rectangle sourceRectangle = new Rectangle((int)section.Position.X, (int)section.Position.Y, (int)section.Size.X, (int)section.Size.Y);

                Rectangle destinationRectangle = new Rectangle((int)position.X - Radius - 1 + (int)section.StretchPosition.X, (int)position.Y - Radius - 1 + (int)section.StretchPosition.Y, (int)section.StretchSize.X, (int)section.StretchSize.Y);

                Vector2 origin = new Vector2(0, 0);

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White * opacity, 0, origin, SpriteEffects.None, 1);
            }
        }
    }
}
