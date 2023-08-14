using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Content;
using System;
using System.IO;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework.Media;
using LostLegend.States;
using LostLegend.Master;
using System.Diagnostics.Metrics;
using Android.Hardware.Camera2;
using Android.Media.Effect;
using LostLegend.Statics;
using LostLegend.Graphics.GUI;
using Android.Icu.Number;
using LostLegend.Statics;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework.Input.Touch;
using System.ComponentModel.Design;
using Android.Content;

namespace LostLegend
{
    class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MasterManager _masterManager;

        private double timer;

        private Context _context;
        private State _currentState;
        private State _nextState;
        private Song song;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1(Activity1 context)
        {
            _context = context;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_graphics.IsFullScreen = userRequestedFullScreen;
            //_graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height - 30;
            IsFixedTimeStep = false;
            //lol _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.ApplyChanges();
            timer = 0;

            TouchPanel.EnabledGestures =
                GestureType.Tap |
                GestureType.VerticalDrag |
                GestureType.HorizontalDrag |
                GestureType.FreeDrag |
                GestureType.None |
                GestureType.Pinch | GestureType.PinchComplete |
                GestureType.Hold;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.song = Content.Load<Song>("Music/Songs/titletheme");
            ContentLoader.LoadTextureDict(Content);
            ContentLoader.LoadColours();
            NpcInfo.LoadNpcInfo();
            ItemInfo.LoadItems();
            //LevelInfo.LoadLevelInfo();
            //Npcs.LoadNpcInfo();
            ContentLoader.LoadFont(Content, GraphicsDevice);
            //Effects.LoadEffects(Content);

            _masterManager = new MasterManager(Window, GraphicsDevice, _graphics, Content);
            _masterManager.storedDataManager.CurrentContext = _context;
            Menus.LoadDefaultGuiComponents(_masterManager);
            Measurements.LoadMeasurements(_masterManager);
            MediaPlayer.Play(song);
            //  Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            _currentState = new MenuState("opening1", _masterManager);//GameState();//
            _nextState = null;
            //Texture2D texture2 = Content.Load<Texture2D>("ContentLoader./Characters/player1");

            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        void MediaPlayer_MediaStateChanged(object sender, System.
                                           EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0f;
            //MediaPlayer.Play(song);
        }



        protected override void Update(GameTime gameTime)
        {

            //MediaPlayer.Volume = _masterManager.storedDataManager.Settings.MusicVolume / 100f;
            
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            double framerate = (1 / gameTime.ElapsedGameTime.TotalSeconds);
            if (timer > 0.5) //rate of refresh the debug
            {
                framerate = (1 / gameTime.ElapsedGameTime.TotalSeconds);
            /*
                string colour = "green";
                if (framerate < 60)
                    colour = "yellow";
                else if (framerate < 30)
                    colour = "orange";
                else if (framerate < 10)
                    colour = "red";
                else if (framerate < 1)
                    colour = "blue";

                _masterManager.playGuiManager.ReloadFps(colour, framerate);
            */
            }
            

            _masterManager.dt = 60 * gameTime.ElapsedGameTime.TotalSeconds  * _masterManager.GameSpeed;
            _masterManager.timePassed = gameTime.ElapsedGameTime.TotalSeconds * _masterManager.GameSpeed;
            _masterManager.gameTime = gameTime;


            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            
            ContentLoader.UpdateTextures(gameTime.ElapsedGameTime.TotalSeconds);

            _currentState.Update(_masterManager, gameTime, this, GraphicsDevice);

            TouchCollection touch = TouchPanel.GetState();
            foreach (TouchLocation touchLocation in touch)
            {
                _masterManager.TouchPos = touchLocation.Position/4;
            }

            if (!TouchPanel.IsGestureAvailable)
            {
                _masterManager.IsScreenTouched = false;
            }
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                switch(gesture.GestureType)
                {
                    case GestureType.Tap:
                        _masterManager.IsScreenTouched = true;
                        _masterManager.TouchPosAfter = gesture.Position/4;
                        break;
                    case GestureType.VerticalDrag:
                        if (_masterManager.ScrollMomentum < 0.95)
                        {
                            _masterManager.OrigScrollDif = Vector2.Zero;
                            _masterManager.ScrollDif = Vector2.Zero;
                        }
                        else
                        {
                            _masterManager.ScrollDif = gesture.Position / 4 - _masterManager.ScrollPos;
                            _masterManager.OrigScrollDif = gesture.Position / 4 - _masterManager.ScrollPos;
                        }
                        //_masterManager.IsScreenTouched = true;
                        _masterManager.ScrollMomentum = 1f;
                        _masterManager.ScrollPos = gesture.Position / 4;
                        break;
                    case GestureType.HorizontalDrag:
                        if (_masterManager.ScrollMomentum < 0.95)
                        {
                            _masterManager.OrigScrollDif = Vector2.Zero;
                            _masterManager.ScrollDif = Vector2.Zero;
                        }
                        else
                        {
                            _masterManager.ScrollDif = gesture.Position / 4 - _masterManager.ScrollPos;
                            _masterManager.OrigScrollDif = gesture.Position / 4 - _masterManager.ScrollPos;
                        }
                        //_masterManager.IsScreenTouched = true;
                        _masterManager.ScrollMomentum = 1f;
                        _masterManager.ScrollPos = gesture.Position / 4;
                        break;
                    case GestureType.FreeDrag:
                        if (_masterManager.ScrollMomentum < 0.95)
                        {
                            _masterManager.OrigScrollDif = Vector2.Zero;
                            _masterManager.ScrollDif = Vector2.Zero;
                        }
                        else
                        {
                            _masterManager.ScrollDif = gesture.Position / 4 - _masterManager.ScrollPos;
                            _masterManager.OrigScrollDif = gesture.Position / 4 - _masterManager.ScrollPos;
                        }
                        //_masterManager.IsScreenTouched = true;
                        _masterManager.ScrollMomentum = 1f;
                        _masterManager.ScrollPos = gesture.Position / 4;
                        break;
                    case GestureType.Pinch:
                        if (_masterManager.ScrollMomentum < 0.95)
                        {
                            _masterManager.OrigScrollDif = Vector2.Zero;
                            _masterManager.ScrollDif = Vector2.Zero;
                        }
                        else
                        {
                            _masterManager.ScrollDif = gesture.Position / 4 - _masterManager.ScrollPos;
                            _masterManager.OrigScrollDif = gesture.Position / 4 - _masterManager.ScrollPos;
                        }
                        //_masterManager.IsScreenTouched = true;
                        _masterManager.ScrollMomentum = 1f;
                        _masterManager.ScrollPos = gesture.Position / 4;
                        break;
                    default:
                        break;
                }
            }

            _masterManager.ScrollMomentum = Math.Max(0, _masterManager.ScrollMomentum - (float)gameTime.ElapsedGameTime.TotalSeconds);
            _masterManager.ScrollDif = _masterManager.OrigScrollDif * _masterManager.ScrollMomentum;
            if (timer > 0.5)
                timer = 0;

            base.Update(gameTime);
        }

        private void DebugRect(SpriteBatch spriteBatch, Rectangle rect, Color colour)
        {
            Texture2D _texture;

            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { colour });
            spriteBatch.Draw(_texture, rect, colour);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            GraphicsDevice.Clear(new Color(0,0,0));
            _currentState.Draw(_masterManager, _spriteBatch, GraphicsDevice);
            base.Draw(gameTime);
        }
    }
}
