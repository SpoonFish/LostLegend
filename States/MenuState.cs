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
using LostLegend.World;

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
        private int StoredCounterValue;
        private TextBox _textBox2;
        public MenuState(string menu = "level_select_luxiar", MasterManager master = null)
        {
            PendingChange = true;
            Frozen = false;
            CurrentMenu = menu;
            MenuStack = new List<string>();
            Debug = new TextBox("", new Vector2(268, 108), 250, "basic");
            StoredCounterValue = 0;
            Components = new GuiContent();
            LoadMenu(menu, master);
        }

        private void CheckButtons(MasterManager master, Game1 game, List<IGuiButton> buttons, Point offset = new Point())
        {

            foreach (Button button in buttons)
            {
                button.ComponentTransition.Update(master.timePassed);
                ButtonSignalEvent signal = button.Update(master.TouchPos, master.TouchPosAfter, master.IsScreenTouched, offset);
                bool exitLoop = CheckSignal(master, signal, game);
                if (exitLoop)
                    return;
            }
        }

        private bool CheckSignal(MasterManager master, ButtonSignalEvent signal, Game1 game)
        {
            if (signal.Action != "none")
            {
                switch (signal.Action)
				{
                    case "block":
                        return true;
					case "npc":
						Npc npc = NpcInfo.Npcs[master.worldManager.CurrentNpc];
                        Dialog dialog = npc.Dialogs[npc.TalkStage][npc.CurrentDialog];
						switch (signal.Subject)
						{
                            case "continue":
							    if (dialog.Continues)
							    {
								    npc.CurrentDialog += 1;

							    }
							    else
							    {
								    npc.CurrentDialog = 0;
								    master.worldManager.CurrentNpc = "";
							    }
							    if (dialog.NextNpcStage != -1)
                                {

								    npc.CurrentDialog = 0; 
                                    npc.SavedTalkStage = dialog.NextNpcStage;

							    }
							    if (dialog.NextDialogStage != -1)
							    {
								    npc.TalkStage = dialog.NextDialogStage;
								    npc.CurrentDialog = 0;

							    }
                                break;
                            case "exit":
								npc.CurrentDialog = 0;
								master.worldManager.CurrentNpc = "";
                                break;

						}
						UnloadMenu();
                        master.TouchPos = new Vector2(-10,-10);
						LoadMenu("world_map", master, true);
						return true;
					case "npc_interact":
                        master.worldManager.CurrentNpc = signal.Subject;
                        NpcInfo.Npcs[master.worldManager.CurrentNpc].TalkStage = NpcInfo.Npcs[master.worldManager.CurrentNpc].SavedTalkStage;
						UnloadMenu();
						LoadMenu("world_map", master, true);
						return true;
					case "change_map":
                        switch (signal.Subject)
                        {
                            case "weapon_shop":
                                master.entityManager.Player.CurrentArea = MapInfo.MapAreaIDs.MainRoom;
                                master.worldManager.CurrentMap = MapInfo.Maps[MapInfo.MapIDs.WeaponShop];
                                break;
                            case "island1":
                                master.entityManager.Player.CurrentArea = MapInfo.MapAreaIDs.CentralLithramVillage;
                                master.worldManager.CurrentMap = MapInfo.Maps[MapInfo.MapIDs.Island1];
                                break;
                        }
                        UnloadMenu();
                        LoadMenu("world_map", master, true);
                        return true;
                    case "travel":
                        master.PreviousWorldPos = master.worldManager.CurrentMap.MapAreas[master.entityManager.Player.CurrentArea].CenterPos;
						int mapInt = int.Parse(signal.Subject.Split(',')[1]);
						int areaInt = int.Parse(signal.Subject.Split(',')[0]);
                        master.entityManager.Player.CurrentArea = (MapInfo.MapAreaIDs)areaInt;
                        if ((MapInfo.MapIDs)mapInt != MapInfo.MapIDs.Null)
						    master.worldManager.CurrentMap = MapInfo.Maps[(MapInfo.MapIDs)mapInt];
						else
							master.IsMapMoving = true;
						master.storedDataManager.CurrentSaveFile.CurrentArea = areaInt;
                        if (master.storedDataManager.CurrentSaveFile.DiscoveredAreas.IndexOf(areaInt) == -1)
                            master.storedDataManager.CurrentSaveFile.DiscoveredAreas.Add(areaInt);
                        master.storedDataManager.SaveFile();
                        MapInfo.LoadMapInfo(master);
                        UnloadMenu();
                        LoadMenu("world_map", master, true);
                        return true;
                    case "delete_character":
                        if (Components.MainScreen.TextInputs[0].Text == master.storedDataManager.CurrentSaveFile.Name)
                        {
                            master.storedDataManager.DebugSaveFile(master.storedDataManager.CurrentSaveNumber);
                            master.storedDataManager.SaveFiles[master.storedDataManager.CurrentSaveNumber] = master.storedDataManager.LoadFile(master.storedDataManager.CurrentSaveNumber);
                            master.storedDataManager.OrderSaveFiles();
                            UnloadMenu();
                            LoadMenu("titlescreen", master);
                            return true;
                        }
                        break;
                    case "complete_character":
                        if (Components.MainScreen.TextInputs[0].Valid)
                        {
                            master.storedDataManager.CurrentSaveFile.New = false;
                            master.storedDataManager.CurrentSaveFile.Name = Components.MainScreen.TextInputs[0].Text;
                            master.storedDataManager.SaveFiles[master.storedDataManager.CurrentSaveNumber].New = false;
                            master.storedDataManager.SaveFiles[master.storedDataManager.CurrentSaveNumber].Name = Components.MainScreen.TextInputs[0].Text;
                            master.storedDataManager.SaveFile();
                            master.storedDataManager.NextAvailableSaveSlot += 1;
                            UnloadMenu();
                            LoadMenu("save_select", master);
                            return true;
                        }
                        break;
                    case "reset_settings":
                        //master.storedDataManager.ResetSettings();
                        UnloadMenu();
                        LoadMenu("settings", master, true);
                        return true;

                    case "change_character":
                        string characteristic = signal.Subject.Split(',')[0];
                        bool goToNext = signal.Subject.Split(',')[1] == "next";
                        List<string> hair_types = new List<string>()
                            {
                                "hair2",
                                "hair2"
                            };
                        List<string> hair_colours = new List<string>()
                            {
                                "brown",
                                "brown2",
                                "blond",
                                "black",
                                "dark_brown",
                                "red",
                                "ginger",
                                "gray"
                            };
                        List<string> eye_colours = new List<string>()
                            {
                                "brown",
                                "hazel",
                                "blue",
                                "green",
                                "red",
                                "yellow",
                                "purple",
                                "turquoise",
                                "bluebrown",
                                "yellowgreen"
                            };
                        List<string> skin_tones = new List<string>()
                            {
                                "tone1",
                                "tone2",
                                "tone3",
                                "tone4",
                                "tone5",
                                "tone6"
                            };
                        int index;
                        switch (characteristic)
                        {
                            case "head_direction":
                                Components.MainScreen.BackgroundComponents.Clear();
                                Components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.QuarterScreen.X * 0.6f, Measurements.QuarterScreen.Y * 0.6f - 30), ContentLoader.UniqueImage(ContentLoader.Images["head_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]], StoredCounterValue), new Vector2(Measurements.HalfScreen.X * 1.4f, (int)(Measurements.HalfScreen.X * 1.4f * (30f / 16f)))));
                                Components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.QuarterScreen.X * 0.6f, Measurements.QuarterScreen.Y * 0.6f - 30), ContentLoader.UniqueImage(ContentLoader.Images[master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] + "_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]], StoredCounterValue), new Vector2(Measurements.HalfScreen.X * 1.4f, (int)(Measurements.HalfScreen.X * 1.4f * (30f / 16f)))));
                                Components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.QuarterScreen.X * 0.6f, Measurements.QuarterScreen.Y * 0.6f - 30), ContentLoader.UniqueImage(ContentLoader.Images["eyes_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[2]], StoredCounterValue), new Vector2(Measurements.HalfScreen.X * 1.4f, (int)(Measurements.HalfScreen.X * 1.4f * (30f / 16f)))));
                                switch (StoredCounterValue)
                                {
                                    case 0:
                                        StoredCounterValue = 3;
                                        break;
                                    case 1:
                                        StoredCounterValue = 2;
                                        break;
                                    case 2:
                                        StoredCounterValue = 0;
                                        break;
                                    case 3:
                                        StoredCounterValue = 1;
                                        break;

                                }
                                return true;
                            case "hair_type":
                                index = hair_types.IndexOf(master.storedDataManager.CurrentSaveFile.CharacterAppearance[0]);
                                if (goToNext)
                                {
                                    index += 1;
                                    if (index > 0)
                                        index = 0;
                                }
                                else
                                {
                                    index -= 1;
                                    if (index < 0)
                                        index = 0;
                                }
                                master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] = hair_types[index];
                                break;
                            case "hair_colour":
                                index = hair_colours.IndexOf(master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]);
                                if (goToNext)
                                {
                                    index += 1;
                                    if (index > hair_colours.Count - 1)
                                        index = 0;
                                }
                                else
                                {
                                    index -= 1;
                                    if (index < 0)
                                        index = hair_colours.Count - 1;
                                }
                                master.storedDataManager.CurrentSaveFile.CharacterAppearance[1] = hair_colours[index];
                                break;
                            case "eye_colour":
                                index = eye_colours.IndexOf(master.storedDataManager.CurrentSaveFile.CharacterAppearance[2]);
                                if (goToNext)
                                {
                                    index += 1;
                                    if (index > eye_colours.Count - 1)
                                        index = 0;
                                }
                                else
                                {
                                    index -= 1;
                                    if (index < 0)
                                        index = eye_colours.Count - 1;
                                }
                                master.storedDataManager.CurrentSaveFile.CharacterAppearance[2] = eye_colours[index];
                                break;
                            case "skin_tone":
                                index = skin_tones.IndexOf(master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]);
                                if (goToNext)
                                {
                                    index += 1;
                                    if (index > skin_tones.Count - 1)
                                        index = 0;
                                }
                                else
                                {
                                    index -= 1;
                                    if (index < 0)
                                        index = skin_tones.Count - 1;
                                }
                                master.storedDataManager.CurrentSaveFile.CharacterAppearance[3] = skin_tones[index];
                                break;
                        }
                        //master.storedDataManager.Settings.SoundVolume = soundVolume;
                        UnloadMenu();
                        master.TouchPos = new Vector2(-30, 30);
                        LoadMenu("create_character", master, true);
                        return true;
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
                        return true;

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
                        return true;
                    case "select_save":
                        master.storedDataManager.CurrentSaveNumber = int.Parse(signal.Subject);
                        master.storedDataManager.CurrentSaveFile = master.storedDataManager.SaveFiles[int.Parse(signal.Subject)];
                        master.entityManager.LoadPlayer(master);
                        MapInfo.LoadMapInfo(master);
                        //master.storedDataManager.Settings.MusicVolume = musicVolume;
                        UnloadMenu();
                        LoadMenu("save_options", master, true);
                        return true;

                    case "toggle_attack_type":
                        //  if (master.storedDataManager.Settings.AttackTowardsMouse)
                        //        master.storedDataManager.Settings.AttackTowardsMouse = false;
                        //         else
                        //        master.storedDataManager.Settings.AttackTowardsMouse = true;

                        UnloadMenu();
                        LoadMenu("settings", master, true);
                        return true;

                    case "create_character":
                        if (master.storedDataManager.NextAvailableSaveSlot == 10)
                            return true;
                        master.storedDataManager.CurrentSaveFile = master.storedDataManager.LoadFile(master.storedDataManager.NextAvailableSaveSlot);
                        master.storedDataManager.CurrentSaveNumber = master.storedDataManager.NextAvailableSaveSlot;
                        master.TouchPos = new Vector2(-40, -40);
                        CurrentMenu = "create_character";
                        foreach (FadingImage image in Components.MainScreen.FadingImages)
                        {
                            if (image.FadeOnExit == true)
                            {
                                image.Fade();
                                Frozen = true;
                                PendingChange = true;
                                return true;
                            }
                        }
                        UnloadMenu();
                        LoadMenu(CurrentMenu, master);
                        return true;
                    case "change_menu":
                        MenuStack.Insert(0, CurrentMenu);

                        master.TouchPos = new Vector2(-40, -40);
                        string newMenu = signal.Subject;
                        CurrentMenu = newMenu;
                        foreach (FadingImage image in Components.MainScreen.FadingImages)
                        {
                            if (image.FadeOnExit == true)
                            {
                                image.Fade();
                                Frozen = true;
                                PendingChange = true;
                                return true;
                            }
                        }
                        UnloadMenu();
                        LoadMenu(newMenu, master);
                        return true;

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
                                return true;
                            }
                        }
                        UnloadMenu();
                        LoadMenu(newMenu2, master);
                        return true;

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
                        LoadMenu("buy_" + purchase, master, true);
                        return true;

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
                                return true;
                            }
                        }

                        UnloadMenu();
                        LoadMenu(backMenu, master);
                        return true;

                    case "load_save":
                        int loadNumber = int.Parse(signal.Subject);
                        // master.storedDataManager.LoadSelectedFile(loadNumber);
                        MenuStack.Clear();

                        UnloadMenu();
                        //       if (master.storedDataManager.CurrentSaveFile.ShowIntro)
                        //             LoadMenu("intro1", master);
                        //     else
                        //       LoadMenu("planet_select", master);
                        return true;

                    case "create_new_save_menu":
                        int createNumber = int.Parse(signal.Subject);
                        //          master.storedDataManager.CurrentSaveNumber = createNumber;
                        MenuStack.Insert(0, CurrentMenu);

                        CurrentMenu = "new_save_creator";
                        UnloadMenu();
                        LoadMenu(CurrentMenu, master);
                        return true;


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
                        return true;
                    case "exit":
                        game.Exit();
                        break;
                }
            }
            return false;
        }

        public override void Update(MasterManager master, GameTime gameTime, Game1 game, GraphicsDevice graphicsDevice)
        {
           // if (Components.MainScreen.Buttons.Count == 2)
           //     Components.MainScreen.Buttons[0] = new Button($"{Components.Screens[0].CurrentScroll.Y}", "", new Vector2(), new Vector2(Measurements.FullScreen.X, 90), new ButtonSignalEvent());
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
                screen.Update(master.TouchPos, master.ScrollDif, master.ScrollMomentum);
            }

            List<IGuiButton> mainButtons = Components.MainScreen.Buttons;
            CheckButtons(master, game, mainButtons);
            for (int i = Components.Screens.Count - 1; i >= 0; i--)
            {
                ScrollScreen screen = (ScrollScreen)Components.Screens[i];
                List<IGuiButton> otherButtons = new List<IGuiButton>();
                otherButtons.AddRange(screen.Buttons);

                Point offset = new Point((int)screen.CurrentScroll.X, (int)screen.CurrentScroll.Y);
                CheckButtons(master, game, otherButtons, offset);

                foreach (MapInteractionButton interactionButton in screen.InteractionButtons)
                {
                    
                    ButtonSignalEvent signal = interactionButton.Update(master.TouchPos, master.TouchPosAfter, master.IsScreenTouched, screen.CurrentScroll.ToPoint());
                    CheckSignal(master, signal, game);
                }

            }

            if (master.IsMapMoving)
            {
                master.worldManager.WorldMovingTime += master.timePassed;
                if (master.worldManager.WorldMovingTime > 1)
                {

                    master.worldManager.WorldMovingTime = 0;
                    master.IsMapMoving = false;
                    MapInfo.LoadMapInfo(master);
                    UnloadMenu();
                    LoadMenu("world_map", master, true);
                }
                else
                {

                    Vector2 scrollFocusPos = master.worldManager.CurrentMap.MapAreas[master.entityManager.Player.CurrentArea].CenterPos;
                    Vector2 incrementVector = (float)master.worldManager.WorldMovingTime * (scrollFocusPos - master.PreviousWorldPos);

                    Vector2 startScroll = (master.PreviousWorldPos + incrementVector) - Measurements.HalfScreen + new Vector2(15, 8);
                    if (startScroll.X < 0)
                        startScroll.X = 0;
                    else if (startScroll.X > 960 - Measurements.FullScreen.X)
                        startScroll.X = 960 - Measurements.FullScreen.X;
                    if (startScroll.Y < 0)
                        startScroll.Y = 0;
                    else if (startScroll.Y > 960 - Measurements.FullScreen.Y)
                        startScroll.Y = 960 - Measurements.FullScreen.Y;
                    startScroll *= -1;

                    Components.Screens[0].CurrentScroll = startScroll;
                }
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

            if (master.IsMapMoving)
            {
                Vector2 scrollFocusPos = master.worldManager.CurrentMap.MapAreas[master.entityManager.Player.CurrentArea].CenterPos;
                Vector2 incrementVector = (float)master.worldManager.WorldMovingTime * (scrollFocusPos - master.PreviousWorldPos);
                Vector2 drawPos = master.PreviousWorldPos + incrementVector;
                Vector2 offset = Vector2.Zero;
                Components.Screens[0].BackgroundComponents.Add(new ImagePanel(drawPos + offset, ContentLoader.UniqueImage(ContentLoader.Images["head_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]], 0), new Vector2(32, 60)));
                Components.Screens[0].BackgroundComponents.Add(new ImagePanel(drawPos + offset, ContentLoader.Images[master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] + "_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]], new Vector2(32, 60)));
                Components.Screens[0].BackgroundComponents.Add(new ImagePanel(drawPos + offset, ContentLoader.Images["eyes_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[2]], new Vector2(32, 60)));

            }
            Components.Draw(spriteBatch, master);

            if (master.IsMapMoving)
            {
                Components.Screens[0].BackgroundComponents.Reverse();
                Components.Screens[0].BackgroundComponents.RemoveRange(0, 3);
                Components.Screens[0].BackgroundComponents.Reverse();

            }
            spriteBatch.End();
        }
    }
}
