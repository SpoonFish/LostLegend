using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LostLegend.Master;
using LostLegend.Statics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostLegend.Graphics.GUI.PlayScreen
{
    class Bar
    {
        public Vector2 Position;
        public float MaxValue;
        public float MinValue;
        private string Style;
        private int MaxSegments;
        private int SegWidth;
        private List<BarSegment> Segments;

        public Bar(Vector2 position, float maxVal, float minVal, string style)
        {
            MaxSegments = 10;
            SegWidth = 10;
            Position = position;
            MaxValue = maxVal;
            MinValue = minVal;
            Style = style;
            Segments = new List<BarSegment>();

            ResetSegments();
        }

        public void ResetSegments()
        {

            Vector2 segmentPos = new Vector2(Position.X, Position.Y);
            Segments.Clear();
            switch (Style)
            {
                case "enemy_hp":
                    MaxSegments = 10;
                    SegWidth = 3;
                    for (int i = 0; i < MaxSegments; i++)
                    {
                        Segments.Add(new BarSegment(segmentPos, ContentLoader.UniqueImage(ContentLoader.Images["enemy_bar_segment"])));
                        segmentPos.X += SegWidth; //width of a segment
                    }
                    break;
                case "player_hp":
                    MaxSegments = 10;
                    SegWidth = 10;
                    for (int i = 0; i < MaxSegments; i++)
                    {
                        Segments.Add(new BarSegment(segmentPos, ContentLoader.UniqueImage(ContentLoader.Images["green_player_bar_segment"])));
                        segmentPos.X += SegWidth; //width of a segment
                    }
                    break;
                case "player_se": //rainbow
                    MaxSegments = 10;
                    SegWidth = 10;
                    //looks kinda weird: string[] colourOrder = { "purple", "red", "orange", "yellow", "blue", "purple", "red", "orange", "yellow", "blue" };

                    for (int i = 0; i < MaxSegments; i++)
                    {
                        Segments.Add(new BarSegment(segmentPos, ContentLoader.UniqueImage(ContentLoader.Images["se_player_bar_segment"])));
                        segmentPos.X += SegWidth;
                    }
                    break;
            }
        }
        public void Update(float value, MasterManager master)
        {
            float progress = Math.Max((value - MinValue) / (MaxValue - MinValue), 0);
            int currentSegment = (int)(progress * MaxSegments);

            for (int i = 0; i < 10; i++)
            {
                if (i < currentSegment)
                {
                    Segments[i].Update(master, 1);
                    Segments[i].Texture.CurrentFrame = Segments[0].Texture.CurrentFrame;
                }
                else if (i > currentSegment)
                    Segments[i].Update(master, 0);
            }

            if (currentSegment != 10)
            {
                float segProgress = (progress - (float)(currentSegment / (float)MaxSegments)) * MaxSegments;
                Segments[currentSegment].UpdateState(segProgress);
            }

            for (int i = 0; i < MaxSegments; i++)
            {
                Segments[i].Position = new Vector2(Position.X + i * SegWidth, Position.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(BarSegment segment in Segments)
            {
                if (segment.SegmentProgress > 0)
                    segment.Texture.Draw(spriteBatch, segment.Position);
            }
        }
    }
}
