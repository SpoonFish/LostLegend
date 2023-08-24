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
using Spextria.StoredData;
using Org.Apache.Http.Impl.Entity;
using LostLegend.Entities;
using LostLegend.World;

namespace LostLegend.Master
{
    class MasterManager
    {
        public Effect CurrentSpriteEffect;
        public bool IsScreenTouched;
        public Vector2 TouchPos;
        public float ScrollMomentum;
        public Vector2 OrigScrollDif;
        public Vector2 TouchPosAfter;
        public Vector2 ScrollPos;
        public bool IsMapMoving;
        public Vector2 PreviousWorldPos;
        public Vector2 ScrollDif;
        public Point MidPos;
        public Vector2 ScreenSize;
        public double dt;
        public double timePassed;
        public float GameSpeed;
        public GameTime gameTime;
        public RenderTarget2D MenuTarget;
        public RenderTarget2D EntitiesTarget;
        public StoredDataManager storedDataManager;
        public EntityManager entityManager;
        public WorldManager worldManager;

		public Point SavedScroll;

		//public StoredDataManager storedDataManager;

		public MasterManager(GameWindow window, GraphicsDevice graphics, GraphicsDeviceManager graphicsDeviceManager, ContentManager content)
		{
			SavedScroll = new Point();
			IsMapMoving = false;
            PreviousWorldPos = new Vector2(0, 0);
            ScrollMomentum = 0;
            ScreenSize = new Vector2((graphics.DisplayMode.Width), (int)(graphics.DisplayMode.Height))/4;
            ScrollPos = new Vector2(0, 0);
            ScrollDif = new Vector2(0, 0);
            OrigScrollDif = new Vector2(0, 0);
            TouchPos = new Vector2(0, 0);
            GameSpeed = 1f;
            MenuTarget = new RenderTarget2D(graphics, 426, 233);
            EntitiesTarget = new RenderTarget2D(graphics, 426, 233);
            CurrentSpriteEffect = null;
            storedDataManager = new StoredDataManager();
            worldManager = new WorldManager();
            entityManager = new EntityManager(this);
           // PlayerEntity player = new PlayerEntity(ContentLoader.Images["player1"], new Vector2(40, 18), new PlayerStats(),2f, 2.7f, weight: 1.8f);
          //  entityManager = new EntityManager(player);
        }
           
    }
}
