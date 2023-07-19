using System;
using System.Collections.Generic;
using System.Text;
using LostLegend.Graphics;
//using LostLegend.Entities;
//using LostLegend.StoredData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LostLegend.Statics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace LostLegend.Master
{
    class MasterManager
    {
        public Effect CurrentSpriteEffect;
        public bool IsScreenTouched;
        public Vector2 TouchPos;
        public Vector2 TouchPosAfter;
        public Point MidPos;
        public Vector2 ScreenSize;
        public double dt;
        public double timePassed;
        public float GameSpeed;
        public GameTime gameTime;
        public RenderTarget2D MenuTarget;
        public RenderTarget2D EntitiesTarget;


        //public StoredDataManager storedDataManager;

        public MasterManager(GameWindow window, GraphicsDevice graphics, GraphicsDeviceManager graphicsDeviceManager, ContentManager content)
        { 
            ScreenSize = new Vector2((graphics.DisplayMode.Width), (int)(graphics.DisplayMode.Height))/4;


            TouchPos = new Vector2(0, 0);
            GameSpeed = 1f;
            MenuTarget = new RenderTarget2D(graphics, 426, 233);
            EntitiesTarget = new RenderTarget2D(graphics, 426, 233);
            CurrentSpriteEffect = null;

           // PlayerEntity player = new PlayerEntity(ContentLoader.Images["player1"], new Vector2(40, 18), new PlayerStats(),2f, 2.7f, weight: 1.8f);
          //  entityManager = new EntityManager(player);
        }
           
    }
}
