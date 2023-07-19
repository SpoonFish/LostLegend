using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LostLegend.Graphics.GUI;
using LostLegend.Graphics.GUI.Interactions;
using LostLegend.Master;
using LostLegend.Statics;
using System;
using System.Collections.Generic;
using LostLegend.Statics;

namespace LostLegend.States
{
    class MenuState : State
    {
        private bool PendingChange;
        private bool Frozen;
        private string CurrentMenu;
        private GuiContent Components;
        private TextBox Debug;
        private List<string> MenuStack; // remembers what menus you were last on for when you go 'back'

        private TextBox _textBox;
        private TextBox _textBox2;
        public MenuState(string menu = "level_select_luxiar", MasterManager master = null)
        {
            PendingChange = true;
            Frozen = false;
            CurrentMenu = menu;
            MenuStack = new List<string>();
            Debug = new TextBox("", new Vector2(268, 108), 250, "basic", true);
            Components = new GuiContent();
            LoadMenu(menu, master);
        }

        private void CheckButtons(MasterManager master, Game1 game, List<IGuiButton> buttons, Point offset = new Point())
        {

            foreach (Button button in buttons)
            {
                ButtonSignalEvent signal = button.Update(master.TouchPos, master.TouchPosAfter, master.IsScreenTouched, offset);

                if (signal.Action != "none")
                {
                    switch (signal.Action)
                    {
                        case "reset_settings":
                            //master.storedDataManager.ResetSettings();
                            UnloadMenu();
                            LoadMenu("settings", master, true);
                            return;

                        case "change_soundvol":
                            int soundVolume = 0;// master.storedDataManager.Settings.SoundVolume;
                            soundVolume += int.Parse(signal.Subject);
                            if (soundVolume > 100)
                                soundVolume = 100;
                            else if (soundVolume < 0)
                                soundVolume = 0;
                            //master.storedDataManager.Settings.SoundVolume = soundVolume;
                            UnloadMenu();
                            LoadMenu("settings", master, true);
                            return;

                        case "change_musicvol":
                            int musicVolume = 0;//master.storedDataManager.Settings.MusicVolume;
                            musicVolume += int.Parse(signal.Subject);
                            if (musicVolume > 100)
                                musicVolume = 100;
                            else if (musicVolume < 0)
                                musicVolume = 0;
                            //master.storedDataManager.Settings.MusicVolume = musicVolume;
                            UnloadMenu();
                            LoadMenu("settings", master, true);
                            return;

                        case "toggle_attack_type":
                          //  if (master.storedDataManager.Settings.AttackTowardsMouse)
                        //        master.storedDataManager.Settings.AttackTowardsMouse = false;
                   //         else
                        //        master.storedDataManager.Settings.AttackTowardsMouse = true;

                            UnloadMenu();
                            LoadMenu("settings", master, true);
                            return;

                        case "change_menu":
                            MenuStack.Insert(0, CurrentMenu);

                            master.TouchPos = new Vector2(-10, -10);
                            string newMenu = signal.Subject;
                            CurrentMenu = newMenu;
                            foreach (FadingImage image in Components.MainScreen.FadingImages)
                            {
                                if (image.FadeOnExit == true)
                                {
                                    image.Fade();
                                    Frozen = true;
                                    PendingChange = true;
                                    return;
                                }
                            }
                            UnloadMenu();
                            LoadMenu(newMenu, master);
                            return;

                        case "change_menu+reset_scroll":
                            MenuStack.Insert(0, CurrentMenu);

                            string newMenu2 = signal.Subject;
                            CurrentMenu = newMenu2;
                            foreach (FadingImage image in Components.MainScreen.FadingImages)
                            {
                                if (image.FadeOnExit == true)
                                {
                                    image.Fade();
                                    Frozen = true;
                                    PendingChange = true;
                                    return;
                                }
                            }
                            UnloadMenu();
                            LoadMenu(newMenu2, master);
                            return;

                        case "purchase":
                            MenuStack.Insert(0, CurrentMenu);

                            string purchase = signal.Subject.Split("/")[0];
                            int cost = int.Parse(signal.Subject.Split("/")[1]);
                            string currency = signal.Subject.Split("/")[2];

                            int playerCurrencyAvailable = 0;
                            switch (currency)
                            {
                                case "coin":
                                {
                             //       playerCurrencyAvailable = master.storedDataManager.CurrentSaveFile.Coins;
                                    break;
                                }
                               
                            }
                          //  if (!master.storedDataManager.CurrentSaveFile.Purchases.Contains(purchase) && playerCurrencyAvailable >= cost)
                            {
                                playerCurrencyAvailable -= cost;
                            //    master.storedDataManager.CurrentSaveFile.Purchases.Add(purchase);
                            }
                            switch (currency)
                            {
                                case "coin":
                                    {
                                       // master.playGuiManager.ReloadCoinCounter(playerCurrencyAvailable);
                                       // master.storedDataManager.CurrentSaveFile.Coins = playerCurrencyAvailable;
                                        break;
                                    }
                                
                            }
                            //master.storedDataManager.SaveFile();
                            UnloadMenu();
                            LoadMenu("buy_"+purchase, master, true);
                            return;

                        case "back_menu":
                      //      if (CurrentMenu == "settings")
                          //      master.storedDataManager.SaveSettings();
                            string backMenu = MenuStack[0];
                            MenuStack.RemoveAt(0);
                            CurrentMenu = backMenu;
                            foreach (FadingImage image in Components.MainScreen.FadingImages)
                            {
                                if (image.FadeOnExit == true)
                                {
                                    image.Fade();
                                    Frozen = true;
                                    PendingChange = true;
                                    return;
                                }
                            }

                            UnloadMenu();
                            LoadMenu(backMenu, master);
                            return;

                        case "load_save":
                            int loadNumber = int.Parse(signal.Subject);
                           // master.storedDataManager.LoadSelectedFile(loadNumber);
                            MenuStack.Clear();

                            UnloadMenu();
                     //       if (master.storedDataManager.CurrentSaveFile.ShowIntro)
                   //             LoadMenu("intro1", master);
                       //     else
                         //       LoadMenu("planet_select", master);
                            return;

                        case "create_new_save_menu":
                            int createNumber = int.Parse(signal.Subject);
                  //          master.storedDataManager.CurrentSaveNumber = createNumber;
                            MenuStack.Insert(0, CurrentMenu);

                            CurrentMenu = "new_save_creator";
                            UnloadMenu();
                            LoadMenu(CurrentMenu, master);
                            return;


                        case "create_new_save":
                            if (!Components.MainScreen.TextInputs[0].Valid)
                                break;
                            string saveName = Components.MainScreen.TextInputs[0].Text;
                 //           master.storedDataManager.CreateFile(saveName);
                            MenuStack.Clear();
                            MenuStack.Add("titlescreen");
                            CurrentMenu = "save_selection";
                            UnloadMenu();
                            LoadMenu(CurrentMenu, master);
                            return;
                        case "exit":
                            game.Exit();
                            break;
                    }
                }
            }
        }

        public override void Update(MasterManager master, GameTime gameTime, Game1 game, GraphicsDevice graphicsDevice)
        {
            foreach (FadingImage image in Components.MainScreen.FadingImages)
            {
                string possibleNextMenu = image.Update(gameTime);
                if (possibleNextMenu != "")
                {

                    UnloadMenu();
                    LoadMenu(possibleNextMenu, master);
                    return;
                }

                if (image.FreezeMenu && !image.FadeOnExit)
                    Frozen = true;
                if (image.DoneFading)
                {
                    Frozen = false;
                    image.DoneFading = false;
                    if (image.FadeOnExit && PendingChange)
                    {
                        PendingChange = false;
                        UnloadMenu();
                        LoadMenu(CurrentMenu, master);
                        return;
                    }
                }
            }

            if (Frozen)
                return;

            foreach (TextInput input in Components.MainScreen.TextInputs)
            {
                input.Update(master.TouchPos, master.IsScreenTouched, gameTime);
            }

            foreach (ScrollScreen screen in Components.Screens)
            {
                screen.Update(master.TouchPos, master.IsScreenTouched);
            }

            List<IGuiButton> mainButtons = Components.MainScreen.Buttons;
            CheckButtons(master, game, mainButtons);
            CheckButtons(master, game, Components.MainScreen.Nodes);
            for (int i = Components.Screens.Count - 1; i >= 0; i--)
            {
                ScrollScreen screen = (ScrollScreen)Components.Screens[i];
                List<IGuiButton> otherButtons = new List<IGuiButton>();
                otherButtons.AddRange(screen.Buttons);
                otherButtons.AddRange(screen.Nodes);

                Point offset = new Point((int)screen.CurrentScroll.X, (int)screen.CurrentScroll.Y);
                CheckButtons(master, game, otherButtons, offset);
            }

        }

        public void UnloadMenu()
        {
            Components.MainScreen.TextInputs.Clear();
            Components.MainScreen.Buttons.Clear();
            Components.MainScreen.BasicComponents.Clear();
            Components.MainScreen.FadingImages.Clear();
            Components.Screens.Clear();
        }
        public void LoadMenu(string menu, MasterManager master = null, bool isReload = false)
        {
            Components = Menus.LoadMenu(menu, master, isReload);
        }

        public override void Draw(MasterManager master, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            const int scaleX = 4;//(float)_graphics.PreferredBackBufferWidth / 80;
            const int scaleY = 4;//(float)_graphics.PreferredBackBufferHeight / 48;

            

            Matrix matrix = Matrix.CreateScale(scaleX, scaleY, 0f);
            

            graphicsDevice.SetRenderTarget(null);
            //graphicsDevice.SetRenderTarget(normScreen);
            spriteBatch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
            //_spriteBatch.Draw(_text.BoxImage, _text.Position, Color.White);
            _textBox = new TextBox("#main#Silly Goober", new Vector2(Functions.FitToScreen(master, 40, true).X, 10), Functions.FitToScreen(master, 40, true).Width / 4, "bronze");
            _textBox2 = new TextBox($"X:313 Y:1.31   {master.MidPos}", new Vector2(10, 40), 100, "bronze");

            Components.Draw(spriteBatch, master);
            spriteBatch.End();
        }
    }
}
