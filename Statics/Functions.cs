using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text;
using Microsoft.Xna.Framework;
using LostLegend.Master;
using Microsoft.Xna.Framework.Content;
using Android.Hardware.Lights;

namespace LostLegend.Statics
{
    static class Functions
    {
        public static Rectangle FitToScreen(MasterManager master, int margin =0, bool marginAsPercentage = true)
        {
            int width;
            int height;
            int x;
            int y;
            if (marginAsPercentage)
            {
                width = Math.Max(1, (int)master.ScreenSize.X - ((int)master.ScreenSize.X * (margin/100))*2);
                height = Math.Max(1, (int)master.ScreenSize.Y - ((int)master.ScreenSize.Y * (margin / 100))*2);
                x = (int)master.ScreenSize.X * (margin / 100);
                y = (int)master.ScreenSize.X * (margin / 100);
            }
            else
            {
                width = Math.Max(1, (int)master.ScreenSize.X - margin * 2);
                height = Math.Max(1, (int)master.ScreenSize.Y - margin * 2);
                x = margin;
                y = margin;
            }
            return new Rectangle(x, y, width, height);
        }
    }
}
