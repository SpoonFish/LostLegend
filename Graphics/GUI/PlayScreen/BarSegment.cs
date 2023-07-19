using Microsoft.Xna.Framework;
using LostLegend.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace LostLegend.Graphics.GUI.PlayScreen
{
    class BarSegment
    {
        public Vector2 Position;
        public AnimatedTexture Texture;
        public float SegmentProgress;

        public BarSegment(Vector2 position, AnimatedTexture texture)
        {
            Position = position;
            Texture = texture;
            SegmentProgress = 1;
        }
        public void Update(MasterManager master, float progress)
        {
            SegmentProgress = progress;
            if (SegmentProgress == 1)
            {
                Texture.SetType(0);
                Texture.Update(master.timePassed);

            }
            else
            {
                Texture.SetType(1);
                Texture.CurrentFrame = 4;

            }
        }

        public void UpdateState(float progress)
        {
            Texture.SetType(1);
            SegmentProgress = progress;
            int frame = -(int)(progress * 5)+4;
            Texture.CurrentFrame = frame;
        }
    }
}
