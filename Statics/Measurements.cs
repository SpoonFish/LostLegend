﻿using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text;
using Microsoft.Xna.Framework;
using LostLegend.Master;
using Microsoft.Xna.Framework.Content;
using Android.Hardware.Lights;
using MonoGame.Extended;

namespace LostLegend.Statics
{
    static class Measurements
    {
        public static Vector2 FullScreen;
        public static Vector2 QuarterScreen;
        public static Vector2 HalfScreen;
        public static Vector2 EighthScreen;
        public static Vector2 ThreeQuarterScreen;
        public static Vector2 ThreeEighthScreen;

        static public void LoadMeasurements(MasterManager master)
        {
            FullScreen = master.ScreenSize;
            QuarterScreen = new Vector2((master.ScreenSize.X / 4), (master.ScreenSize.Y / 4));
            HalfScreen = new Vector2((master.ScreenSize.X / 2), (master.ScreenSize.Y / 2));
            EighthScreen = new Vector2((master.ScreenSize.X / 8), (master.ScreenSize.Y / 8));

            ThreeQuarterScreen = new Vector2(Math.Max(QuarterScreen.X+1,(master.ScreenSize.X - QuarterScreen.X)), Math.Max(QuarterScreen.Y + 1, (master.ScreenSize.Y - QuarterScreen.Y)));
            ThreeEighthScreen = new Vector2((master.ScreenSize.X / 8 * 3), ((master.ScreenSize.Y / 8 * 3)));
        }
    }
}
