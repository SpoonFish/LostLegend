using LostLegend.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Text;
using LostLegend.Master;
using Microsoft.Xna.Framework;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Interactions;
using System.ComponentModel.Design;
using Android.Systems;
using LostLegend.World;
using System.ComponentModel;
using System.Threading;
using LostLegend.Entities.InventoryStuff;
using LostLegend.Entities.Parts;
using LostLegend.Entities;
using Android.Telephony.Ims;

namespace LostLegend.States
{
	static class Menus
	{
		// Different object for fade in/out because if you have both in one menu then their opacities would override each other constantly

		static private ImagePanel totalFadein;
		static private ImagePanel totalFadeout;

		static private float PreviousActionMenuY;
		static public void LoadDefaultGuiComponents(MasterManager master)
		{
			PreviousActionMenuY = 0;
			totalFadein = new ImagePanel(new Vector2(0, 0), ContentLoader.Images["blackout"], master.ScreenSize);
			totalFadeout = new ImagePanel(new Vector2(0, 0), ContentLoader.Images["blackout"], master.ScreenSize);
		}
		static public GuiContent LoadMenu(string menuName, MasterManager master = null, bool isReload = false)
		{
			GuiContent components = new GuiContent();
			switch (menuName)
			{
				case "opening1":
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(0, 0), ContentLoader.Images["blackout"], master.ScreenSize*3));
					components.MainScreen.FadingImages.Add(new FadingImage(new ImagePanel(new Vector2(Measurements.ThreeEighthScreen.X - 50, Measurements.HalfScreen.Y - Measurements.QuarterScreen.X / 2 - 50), ContentLoader.Images["spoonfishstudios_logo"], new Vector2(Measurements.QuarterScreen.X + 100, Measurements.QuarterScreen.X + 100)),"in", 0.5, false, 0, false, false, 1.5, "opening2"));

					break;
				case "opening2":
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(0, 0), ContentLoader.Images["blackout"], master.ScreenSize*3));
					components.MainScreen.FadingImages.Add(new FadingImage(new ImagePanel(new Vector2(Measurements.ThreeEighthScreen.X-50, Measurements.HalfScreen.Y-Measurements.QuarterScreen.X/2-50), ContentLoader.Images["spoonfishstudios_logo"], new Vector2(Measurements.QuarterScreen.X + 100, Measurements.QuarterScreen.X + 100)), "out", 0.5, false, 0, false, false, 1, "titlescreen"));
 
					break;
				case "titlescreen":
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(Measurements.OverflowBackgroundPosition, ContentLoader.Images["ancient_pond"], Measurements.OverflowBackgroundSize));
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.EighthScreen.X, Measurements.EighthScreen.X), ContentLoader.Images["lostlegend_logo"], new Vector2(Measurements.ThreeQuarterScreen.X, Measurements.ThreeQuarterScreen.X/3*2)));
					//components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(0, 0), ContentLoader.Images["test2"], master.ScreenSize));
					//ImagePanel logo = new ImagePanel(new Vector2(0, 0), ContentLoader.Images["spextria_logo"], new Vector2(426, 233));
					//components.MainScreen.BasicComponents.Add(logo);
					//components.FadingImages.Add(new FadingImage(logo, "in", 1.5));
					AddFade(components, false, 0.5);

					components.MainScreen.Buttons.Add(new Button("#white#PLAY", "#lightyellow#PLAY", new Vector2(Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y + 70), new Vector2(Measurements.HalfScreen.X, 40), new ButtonSignalEvent("change_menu", "save_select"), "br_thick", new GuiTransition(true, 0.4, new Vector2(-200, 0), 0)));
				   
					components.MainScreen.Buttons.Add(new Button("#white#SETTINGS", "#lightblue#SETTINGS", new Vector2(Measurements.QuarterScreen.X, Measurements.HalfScreen.Y + 20), new Vector2(Measurements.HalfScreen.X, 40), new ButtonSignalEvent("change_menu", "opening1"), "br_thick", new GuiTransition(true, 0.4, new Vector2(200, 0), 0.3)));
				  
					components.MainScreen.Buttons.Add(new Button("#white#EXIT", "#lightred#EXIT", new Vector2(Measurements.QuarterScreen.X, Measurements.ThreeQuarterScreen.Y -30), new Vector2(Measurements.HalfScreen.X, 40), new ButtonSignalEvent("exit"), "br_thick", new GuiTransition(true, 0.4, new Vector2(-200, 0), 0.6)));
					break;
				case "save_select":

					components.MainScreen.Buttons.Add(new Button("#lightgray#SELECT A CHARACTER", "#lightgray#SELECT A CHARACTER", new Vector2(-10, -10), new Vector2(Measurements.FullScreen.X + 20, Measurements.EighthScreen.Y), new ButtonSignalEvent(), "br_thick", null, new Vector2(20,10)));

					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(-10, Measurements.FullScreen.Y-Measurements.EighthScreen.Y+10), new Vector2(Measurements.FullScreen.X + 20, Measurements.EighthScreen.Y), new ButtonSignalEvent(),"br_thick", null, new Vector2(0, 10)));
					components.MainScreen.Buttons.Add(new Button("<", "#lightred#<", new Vector2(5, 15), new Vector2(30, 30), new ButtonSignalEvent("change_menu", "titlescreen"), "br_thin", null));
					if (master.storedDataManager.NextAvailableSaveSlot == 10)

						components.MainScreen.Buttons.Add(new Button("#gray#CREATE CHARACTER", "#gray#CREATE CHARACTER", new Vector2(10, Measurements.FullScreen.Y - Measurements.EighthScreen.Y + 20), new Vector2(Measurements.FullScreen.X - 20, Measurements.EighthScreen.Y - 30), new ButtonSignalEvent("create_character"), "br_thin", null));

					else
						components.MainScreen.Buttons.Add(new Button("CREATE CHARACTER", "#lightgray#CREATE CHARACTER", new Vector2(10, Measurements.FullScreen.Y - Measurements.EighthScreen.Y + 20), new Vector2(Measurements.FullScreen.X - 20, Measurements.EighthScreen.Y - 30), new ButtonSignalEvent("create_character"), "br_thin", null));

					//components.MainScreen.BackgroundComponents.Add(new TextBox("SELECT A CHARACTER", new Vector2(Measurements.EighthScreen.X, Measurements.EighthScreen.X), (int)Measurements.ThreeQuarterScreen.X, "none"));
					//components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(0, 0), ContentLoader.Images["test2"], master.ScreenSize));
					//ImagePanel logo = new ImagePanel(new Vector2(0, 0), ContentLoader.Images["spextria_logo"], new Vector2(426, 233));
					//components.MainScreen.BasicComponents.Add(logo);
					//components.FadingImages.Add(new FadingImage(logo, "in", 1.5));
					AddFade(components, false, 0.5);
					components.Screens.Add(new ScrollScreen(new Vector2(0,Measurements.EighthScreen.Y -8), new Vector2(Measurements.FullScreen.X , Measurements.ThreeQuarterScreen.Y+24), new Vector2(0,Measurements.EighthScreen.Y - 20), new Vector2(0, Measurements.EighthScreen.Y - 635), new Point(0, (int)Measurements.EighthScreen.Y - 20), false, true));


					for (int i =0; i < 10; i++)
					{
						Vector2 pos = new Vector2(Measurements.EighthScreen.X / 2, Measurements.EighthScreen.Y - 28 + i * 100);
						if (master.storedDataManager.SaveFiles[i].New)
						{
							components.Screens[0].Buttons.Add(new Button($"#gray#EMPTY SLOT", $"#gray#EMPTY SLOT", pos, new Vector2(Measurements.ThreeQuarterScreen.X + Measurements.EighthScreen.X, 80), new ButtonSignalEvent()));
						}


						else
						{

							components.Screens[0].BasicComponents.Add(new ImagePanel(pos + new Vector2(15, 7), ContentLoader.UniqueImage(ContentLoader.Images["head_" + master.storedDataManager.SaveFiles[i].CharacterAppearance[3]], 0), new Vector2(50, (50 * 30 / 16))));
							components.Screens[0].BasicComponents.Add(new ImagePanel(pos + new Vector2(15, 7), ContentLoader.Images[master.storedDataManager.SaveFiles[i].CharacterAppearance[0] + "_" + master.storedDataManager.SaveFiles[i].CharacterAppearance[1]], new Vector2(50, (50 * 30 / 16))));
							components.Screens[0].BasicComponents.Add(new ImagePanel(pos + new Vector2(15, 7), ContentLoader.Images["eyes_" + master.storedDataManager.SaveFiles[i].CharacterAppearance[2]], new Vector2(50, (50*30/16))));

							components.Screens[0].Buttons.Add(new Button($"#white#{master.storedDataManager.SaveFiles[i].Name}", $"#lightyellow#{master.storedDataManager.SaveFiles[i].Name}", new Vector2(Measurements.EighthScreen.X / 2, Measurements.EighthScreen.Y - 28 + i * 100), new Vector2(Measurements.ThreeQuarterScreen.X + Measurements.EighthScreen.X, 80), new ButtonSignalEvent("select_save", i.ToString()), "bronze", null, new Vector2(25, 0)));
						}
					}

					break;

				case "create_character":
					components.MainScreen.Buttons.Add(new Button("#lightgray#CHARACTER CREATOR", "#lightgray#CHARACTER CREATOR", new Vector2(-10, -10), new Vector2(Measurements.FullScreen.X + 20, Measurements.EighthScreen.Y), new ButtonSignalEvent(), "br_thick", null, new Vector2(20, 10)));

					components.MainScreen.Buttons.Add(new Button("<", "#lightred#<", new Vector2(5, 15), new Vector2(30, 30), new ButtonSignalEvent("change_menu", "save_select"), "br_thin", null));

					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.QuarterScreen.X*0.6f, Measurements.QuarterScreen.Y * 0.6f - 30), ContentLoader.UniqueImage(ContentLoader.Images["head_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]], 0), new Vector2(Measurements.HalfScreen.X*1.4f, (int)(Measurements.HalfScreen.X * 1.4f * (30f/16f)))));
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.QuarterScreen.X * 0.6f, Measurements.QuarterScreen.Y * 0.6f - 30), ContentLoader.Images[master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] + "_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]], new Vector2(Measurements.HalfScreen.X * 1.4f, (int)(Measurements.HalfScreen.X * 1.4f* (30f / 16f)))));
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.QuarterScreen.X * 0.6f, Measurements.QuarterScreen.Y * 0.6f - 30), ContentLoader.Images["eyes_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[2]], new Vector2(Measurements.HalfScreen.X * 1.4f, (int)(Measurements.HalfScreen.X * 1.4f * (30f / 16f)))));
					components.MainScreen.Buttons.Add(new Button("<", "#lightgray#<", new Vector2(Measurements.EighthScreen.X - 25, Measurements.QuarterScreen.Y + 25), new Vector2(30, 30), new ButtonSignalEvent("change_character", "head_direction,prev"), "br_thin"));
					components.MainScreen.Buttons.Add(new Button(">", "#lightgray#>", new Vector2(Measurements.FullScreen.X - Measurements.EighthScreen.X - 5, Measurements.QuarterScreen.Y + 25), new Vector2(30, 30), new ButtonSignalEvent("change_character", "head_direction,next"), "br_thin"));

					components.MainScreen.Buttons.Add(new Button("Hair Type", "Hair Type", new Vector2(Measurements.QuarterScreen.X, Measurements.HalfScreen.Y + 20), new Vector2(Measurements.HalfScreen.X, 20), new ButtonSignalEvent(), "br_thin"));
					components.MainScreen.Buttons.Add(new Button("<", "#lightgray#<", new Vector2(Measurements.EighthScreen.X- 15, Measurements.HalfScreen.Y + 15), new Vector2(30,30), new ButtonSignalEvent("change_character", "hair_type,prev"), "br_thin"));
					components.MainScreen.Buttons.Add(new Button(">", "#lightgray#>", new Vector2(Measurements.FullScreen.X - Measurements.EighthScreen.X - 15, Measurements.HalfScreen.Y + 15), new Vector2(30, 30), new ButtonSignalEvent("change_character", "hair_type,next"), "br_thin"));

					components.MainScreen.Buttons.Add(new Button("Hair Colour", "Hair Colour", new Vector2(Measurements.QuarterScreen.X, Measurements.HalfScreen.Y + 70), new Vector2(Measurements.HalfScreen.X, 20), new ButtonSignalEvent(), "br_thin"));
					components.MainScreen.Buttons.Add(new Button("<", "#lightgray#<", new Vector2(Measurements.EighthScreen.X - 15, Measurements.HalfScreen.Y + 65), new Vector2(30, 30), new ButtonSignalEvent("change_character", "hair_colour,prev"), "br_thin"));
					components.MainScreen.Buttons.Add(new Button(">", "#lightgray#>", new Vector2(Measurements.FullScreen.X - Measurements.EighthScreen.X - 15, Measurements.HalfScreen.Y + 65), new Vector2(30, 30), new ButtonSignalEvent("change_character", "hair_colour,next"), "br_thin"));

					components.MainScreen.Buttons.Add(new Button("Eye Colour", "Eye Colour", new Vector2(Measurements.QuarterScreen.X, Measurements.HalfScreen.Y + 120), new Vector2(Measurements.HalfScreen.X, 20), new ButtonSignalEvent(), "br_thin"));
					components.MainScreen.Buttons.Add(new Button("<", "#lightgray#<", new Vector2(Measurements.EighthScreen.X - 15, Measurements.HalfScreen.Y + 115), new Vector2(30, 30), new ButtonSignalEvent("change_character", "eye_colour,prev"), "br_thin"));
					components.MainScreen.Buttons.Add(new Button(">", "#lightgray#>", new Vector2(Measurements.FullScreen.X - Measurements.EighthScreen.X - 15, Measurements.HalfScreen.Y + 115), new Vector2(30, 30), new ButtonSignalEvent("change_character", "eye_colour,next"), "br_thin"));

					components.MainScreen.Buttons.Add(new Button("Skin Tone", "Skin Tone", new Vector2(Measurements.QuarterScreen.X, Measurements.HalfScreen.Y + 170), new Vector2(Measurements.HalfScreen.X, 20), new ButtonSignalEvent(), "br_thin"));
					components.MainScreen.Buttons.Add(new Button("<", "#lightgray#<", new Vector2(Measurements.EighthScreen.X - 15, Measurements.HalfScreen.Y + 165), new Vector2(30, 30), new ButtonSignalEvent("change_character", "skin_tone,prev"), "br_thin"));
					components.MainScreen.Buttons.Add(new Button(">", "#lightgray#>", new Vector2(Measurements.FullScreen.X - Measurements.EighthScreen.X - 15, Measurements.HalfScreen.Y + 165), new Vector2(30, 30), new ButtonSignalEvent("change_character", "skin_tone,next"), "br_thin"));


					components.MainScreen.Buttons.Add(new Button("DONE", "#lightgray#DONE", new Vector2(10, Measurements.FullScreen.Y - Measurements.EighthScreen.Y + 17), new Vector2(Measurements.FullScreen.X - 20, Measurements.EighthScreen.Y - 27), new ButtonSignalEvent("change_menu", "name_character")));

					AddFade(components, isReload, 0.5);

					break;

				case "name_character":
					components.MainScreen.BasicComponents.Add(new TextBox("CHOOSE A NAME FOR THIS CHARACTER ", new Vector2(Measurements.EighthScreen.X, Measurements.QuarterScreen.Y), (int)Measurements.ThreeQuarterScreen.X, "none", "centre"));
					components.MainScreen.Buttons.Add(new Button("<", "#lightred#<", new Vector2(5, 15), new Vector2(30, 30), new ButtonSignalEvent("change_menu", "create_character"), "br_thin", null));

					components.MainScreen.TextInputs.Add(new TextInput("Enter Name..", new Vector2(20, Measurements.FullScreen.Y - Measurements.HalfScreen.Y-20), (int)Measurements.FullScreen.X - 40, 15, 2, "bronze", "centre", 10));
					components.MainScreen.Buttons.Add(new Button("DONE", "#lightyellow#DONE", new Vector2(40, Measurements.ThreeQuarterScreen.Y - 37), new Vector2(Measurements.FullScreen.X - 80, Measurements.EighthScreen.X), new ButtonSignalEvent("complete_character")));

					AddFade(components, isReload, 0.5);

					break;
				case "save_options":
					components.MainScreen.Buttons.Add(new Button($"#lightgray#{master.storedDataManager.CurrentSaveFile.Name}", $"#lightgray#{master.storedDataManager.CurrentSaveFile.Name}", new Vector2(-10, -10), new Vector2(Measurements.FullScreen.X + 20, Measurements.EighthScreen.Y), new ButtonSignalEvent(), "br_thick", null, new Vector2(20, 10)));

					components.MainScreen.Buttons.Add(new Button("<", "#lightred#<", new Vector2(5, 15), new Vector2(30, 30), new ButtonSignalEvent("change_menu", "save_select"), "br_thin", null));

					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.EighthScreen.X+25, Measurements.EighthScreen.Y), ContentLoader.UniqueImage(ContentLoader.Images["head_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]], 0), new Vector2(Measurements.ThreeQuarterScreen.X-50, (Measurements.ThreeQuarterScreen.X-50) * (30f / 16f))));
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.EighthScreen.X+25, Measurements.EighthScreen.Y), ContentLoader.Images[master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] + "_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]], new Vector2(Measurements.ThreeQuarterScreen.X-50, (Measurements.ThreeQuarterScreen.X-50) * (30f / 16f))));
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.EighthScreen.X + 25, Measurements.EighthScreen.Y), ContentLoader.Images["eyes_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[2]], new Vector2(Measurements.ThreeQuarterScreen.X-50, (Measurements.ThreeQuarterScreen.X-50) * (30f / 16f))));
					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.EighthScreen.X + 25, Measurements.EighthScreen.Y), ContentLoader.Images["body_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]], new Vector2(Measurements.ThreeQuarterScreen.X-50, (Measurements.ThreeQuarterScreen.X-50) * (30f / 16f))));
					Vector2 saveCharacterPos = new Vector2(Measurements.EighthScreen.X + 25, Measurements.EighthScreen.Y);
					Vector2 saveCharacterSize = new Vector2(Measurements.ThreeQuarterScreen.X - 50, (Measurements.ThreeQuarterScreen.X - 50) * (30f / 16f));
					if (master.entityManager.Player.Equipment.GetLegs(master) != null)
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(saveCharacterPos, ContentLoader.Images["legs_" + master.entityManager.Player.Equipment.GetLegs(master).Name.Replace(' ', '_')], saveCharacterSize));
					else
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(saveCharacterPos, ContentLoader.Images["legs_gray_shorts"], saveCharacterSize));


					if (master.entityManager.Player.Equipment.GetChest(master) != null)
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(saveCharacterPos, ContentLoader.Images["chest_" + master.entityManager.Player.Equipment.GetChest(master).Name.Replace(' ', '_')], saveCharacterSize));
					else
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(saveCharacterPos, ContentLoader.Images["chest_leaf_shirt"], saveCharacterSize));

					if (master.entityManager.Player.Equipment.GetHead(master) != null)
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(saveCharacterPos, ContentLoader.Images["head_" + master.entityManager.Player.Equipment.GetHead(master).Name.Replace(' ', '_')], saveCharacterSize));
					else
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(saveCharacterPos, ContentLoader.Images[master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] + "_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]], saveCharacterSize));

					components.MainScreen.Buttons.Add(new Button("#lightgreen#PLAY", "#green#PLAY", new Vector2(40, Measurements.ThreeQuarterScreen.Y - 15), new Vector2(Measurements.FullScreen.X - 80, Measurements.EighthScreen.Y/2+10), new ButtonSignalEvent("change_menu", "world_map")));
					components.MainScreen.Buttons.Add(new Button("#lightred#DELETE", "#red#DELETE", new Vector2(40, Measurements.ThreeQuarterScreen.Y + Measurements.EighthScreen.Y/2+20), new Vector2(Measurements.FullScreen.X - 80, Measurements.EighthScreen.Y/2+10), new ButtonSignalEvent("change_menu","delete_character")));

					AddFade(components, isReload, 0.5);

					break;
				case "delete_character":
					components.MainScreen.BasicComponents.Add(new TextBox("#red#ARE YOU SURE YOU WANT TO DELETE THIS CHARACTER? ", new Vector2(Measurements.QuarterScreen.X-15, Measurements.EighthScreen.Y), (int)Measurements.HalfScreen.X+30, "none", "centre"));
					components.MainScreen.BasicComponents.Add(new TextBox("TYPE THIS CHARACTER'S NAME TO CONFIRM DELETION ", new Vector2(Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y + 40), (int)Measurements.HalfScreen.X, "none", "centre"));
					components.MainScreen.Buttons.Add(new Button("<", "#lightred#<", new Vector2(5, 15), new Vector2(30, 30), new ButtonSignalEvent("change_menu", "save_options"), "br_thin", null));

					components.MainScreen.TextInputs.Add(new TextInput("Confirm Name..", new Vector2(20, Measurements.FullScreen.Y - Measurements.HalfScreen.Y + 20), (int)Measurements.FullScreen.X - 40, 15, 0, "bronze", "centre", 10));
					components.MainScreen.Buttons.Add(new Button("#lightred#DELETE", "#red#DELETE", new Vector2(40, Measurements.ThreeQuarterScreen.Y), new Vector2(Measurements.FullScreen.X - 80, Measurements.EighthScreen.X), new ButtonSignalEvent("delete_character")));

					AddFade(components, isReload, 0.5);

					break;
				case "battle":
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X - 45, 10), new Vector2(40, 40), new ButtonSignalEvent("exit_fight"), "br_outline_round", null));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X - 44, 11), ContentLoader.Images["burger_bar"], new Vector2(36, 36)));

					if (!master.entityManager.TurnPassing && !master.entityManager.IsPlayerDead())
					{
						components.MainScreen.Buttons.Add(new Button("", "", new Vector2(-10, Measurements.FullScreen.Y - Measurements.EighthScreen.Y * 1.5f + 10), new Vector2(Measurements.FullScreen.X + 20, 2), new ButtonSignalEvent(), "bronze_battle_category", null, new Vector2(0, 10)));

						if (master.entityManager.Player.SelectedAttack == null)
						{
							components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(10 + Measurements.EighthScreen.X / 2, Measurements.FullScreen.Y - Measurements.QuarterScreen.Y + 2), ContentLoader.Images["attack_icon"], new Vector2(Measurements.EighthScreen.X, Measurements.EighthScreen.X)));

							if (master.entityManager.CurrentBattleMenuCategory == "attacks")
							{
								components.MainScreen.Buttons.Add(new Button("", "", new Vector2(10, Measurements.FullScreen.Y - Measurements.QuarterScreen.Y), new Vector2(Measurements.FullScreen.X / 3 - 20, Measurements.EighthScreen.Y / 2 + 5), new ButtonSignalEvent(), "bronze_battle_category"));
							}
							else
							{
								components.MainScreen.Buttons.Insert(0, new Button("", "", new Vector2(10, Measurements.FullScreen.Y - Measurements.QuarterScreen.Y), new Vector2(Measurements.FullScreen.X / 3 - 20, Measurements.EighthScreen.Y / 2 + 5), new ButtonSignalEvent("change_battle_category", "attacks"), "bronze_battle_category"));
							}
							components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(10 + Measurements.FullScreen.X / 3 + Measurements.EighthScreen.X / 2, Measurements.FullScreen.Y - Measurements.QuarterScreen.Y + 2), ContentLoader.Images["useitem_icon"], new Vector2(Measurements.EighthScreen.X, Measurements.EighthScreen.X)));


							if (master.entityManager.CurrentBattleMenuCategory == "items")
							{
								components.MainScreen.Buttons.Add(new Button("", "", new Vector2(10 + Measurements.FullScreen.X / 3, Measurements.FullScreen.Y - Measurements.QuarterScreen.Y), new Vector2(Measurements.FullScreen.X / 3 - 20, Measurements.EighthScreen.Y / 2 + 5), new ButtonSignalEvent(), "bronze_battle_category"));
							}
							else
							{
								components.MainScreen.Buttons.Insert(0, new Button("", "", new Vector2(10 + Measurements.FullScreen.X / 3, Measurements.FullScreen.Y - Measurements.QuarterScreen.Y), new Vector2(Measurements.FullScreen.X / 3 - 20, Measurements.EighthScreen.Y / 2 + 5), new ButtonSignalEvent("change_battle_category", "items"), "bronze_battle_category"));
							}
						}
						else
							components.MainScreen.Buttons.Add(new Button("ATTACK", "#lightred#ATTACK", new Vector2(20, Measurements.ThreeQuarterScreen.Y - 12), new Vector2(Measurements.FullScreen.X - 40, Measurements.EighthScreen.Y / 2), new ButtonSignalEvent("attack")));
					}



					components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(0, Measurements.EighthScreen.Y), ContentLoader.Images["battle_lithram_beach"], new Vector2(Measurements.FullScreen.X, Measurements.FullScreen.X*10/9)));

				//	if (master.entityManager.Player.SelectedAttack != null)
		//			{
		//				Attack attack = master.entityManager.Player.SelectedAttack;
		//				
		//			}
					components.MainScreen.Entities.Add(new DisplayablePlayer(master.entityManager.Player.Position, master));

					for (int i = 0; i < master.entityManager.MonsterEntities.Count; i ++)
					{
						MonsterEntity entity = master.entityManager.MonsterEntities[i];
						components.MainScreen.Entities.Add(new DisplayableMonster(master.entityManager.GetEntityPos(i), entity, i));

					}



					if (!master.entityManager.TurnPassing && !master.entityManager.IsPlayerDead())
					{
						switch (master.entityManager.CurrentBattleMenuCategory)
						{
							case "attacks":
								components.Screens.Add(new ScrollScreen(new Vector2(00, Measurements.FullScreen.Y - Measurements.EighthScreen.Y * 1.5f + 20), new Vector2(Measurements.FullScreen.X, Measurements.QuarterScreen.Y), new Vector2(0, 0), new Vector2(-100, 0), new Point(0, 0), true, false));
								components.Screens[0].Buttons.Add(new Button("", "", new Vector2(-10, Measurements.FullScreen.Y - Measurements.EighthScreen.Y * 1.5f + 10), new Vector2(Measurements.FullScreen.X * 2, 1000 + Measurements.FullScreen.Y * 3), new ButtonSignalEvent(), "bronze_battle_category", null, new Vector2(0, 10)));

								components.Screens[0].Buttons.Add(new Button("", "", new Vector2(10, Measurements.ThreeQuarterScreen.Y + Measurements.EighthScreen.Y / 2 + 24), new Vector2(Measurements.EighthScreen.Y), new ButtonSignalEvent("select_attack", "running_slash")));
								components.Screens[0].BasicComponents.Add(new ImagePanel(new Vector2(15, Measurements.ThreeQuarterScreen.Y + Measurements.EighthScreen.Y / 2 + 29), ContentLoader.Images["running_slash_icon"], new Vector2(Measurements.EighthScreen.Y - 10)));


								break;
						}
					}
					else if (master.entityManager.IsPlayerDead())
					{
						components.MainScreen.FadingImages.Add(new FadingImage(new ImagePanel(new Vector2(0, Measurements.EighthScreen.Y), ContentLoader.Images["blackout"], new Vector2(Measurements.FullScreen.X, Measurements.FullScreen.X * 10 / 9)),"in",1));
						components.MainScreen.BasicComponents.Add(new TextBox("#red#DEFEAT", new Vector2(Measurements.HalfScreen.X - 30, Measurements.FullScreen.Y-Measurements.EighthScreen.Y), 1000, "none"));
						components.MainScreen.Buttons.Add(new Button("CONTINUE", "#lightred#CONTINUE", new Vector2(20, Measurements.ThreeQuarterScreen.Y - 12), new Vector2(Measurements.FullScreen.X - 40, Measurements.EighthScreen.Y / 2), new ButtonSignalEvent("change_menu", "world_map")));

					}


					break;
				case "world_map":
					Vector2 scrollFocusPos = master.worldManager.CurrentMap.MapAreas[master.entityManager.Player.CurrentArea].CenterPos;
					Vector2 startScroll = scrollFocusPos - Measurements.HalfScreen + new Vector2(15, 8);
					if (startScroll.X < 0)
						startScroll.X = 0;
					else if (startScroll.X > 960 - Measurements.FullScreen.X)
						startScroll.X = 960 - Measurements.FullScreen.X;
					if (startScroll.Y < 0)
						startScroll.Y = 0;
					else if (startScroll.Y > 960 - Measurements.FullScreen.Y)
						startScroll.Y = 960 - Measurements.FullScreen.Y;
					startScroll *= -1;

					// components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(-10, -10), ContentLoader.Images["burger_bar"], new Vector2(400, 400)));
					components.MainScreen.BasicComponents.Add(new TextBox(master.worldManager.CurrentMap.Name, new Vector2(17, 17), 1000, "none", "left", true));
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X - 45, 10), new Vector2(40, 40), new ButtonSignalEvent("exit_fight"), "br_outline_round", null));
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X - 100, 10), new Vector2(40, 40), new ButtonSignalEvent("change_menu", "inventory"), "br_outline_round", null));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X - 44, 11), ContentLoader.Images["burger_bar"], new Vector2(36, 36)));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X - 99, 11), ContentLoader.Images["backpack"], new Vector2(36, 36)));
					int y = 0;
					GuiTransition transition = null;
					foreach (MapInfo.MapAreaActions action in master.worldManager.CurrentMap.MapAreas[master.entityManager.Player.CurrentArea].Actions)
					{
						y++;
						Vector2 pos = new Vector2(10, Measurements.FullScreen.Y - (Measurements.EighthScreen.Y * 0.8f) * y);

						if (master.IsMapMoving)
							transition = new GuiTransition(false, 0.5, new Vector2(0, PreviousActionMenuY - (pos.Y - 10)));
						switch (action)
						{
							case MapInfo.MapAreaActions.Fight:
								components.MainScreen.Buttons.Add(new Button("FIGHT", "#lightgray#FIGHT", pos, new Vector2(Measurements.FullScreen.X - 20, Measurements.EighthScreen.Y *0.5f), new ButtonSignalEvent("start_fight"), transition: transition));

								break;
							case MapInfo.MapAreaActions.Harvest:
								components.MainScreen.Buttons.Add(new Button("HARVEST", "#lightgray#HARVEST", pos, new Vector2(Measurements.FullScreen.X - 20, Measurements.EighthScreen.Y * 0.5f), new ButtonSignalEvent("change_menu", "name_character"), transition: transition));

								break;
							case MapInfo.MapAreaActions.Boss:
								components.MainScreen.Buttons.Add(new Button("BOSS", "#lightgray#BOSS", pos, new Vector2(Measurements.FullScreen.X - 20, Measurements.EighthScreen.Y * 0.5f), new ButtonSignalEvent("change_menu", "name_character"), transition: transition));

								break;
						}
					}
					if (y != 0)
						components.MainScreen.Buttons.Insert(0, new Button("", "", new Vector2(-10, Measurements.FullScreen.Y - 10 - (Measurements.EighthScreen.Y * 0.8f) * y), new Vector2(Measurements.FullScreen.X + 20, Measurements.FullScreen.Y), new ButtonSignalEvent(), "br_thick", new GuiTransition(false, 0.5, new Vector2(0, PreviousActionMenuY - (Measurements.FullScreen.Y - 10 - Measurements.EighthScreen.Y * 0.8f * y)))));
					else
					{
						if (master.IsMapMoving)
							components.MainScreen.Buttons.Insert(0, new Button("#lightgray#NO MAIN ACTIONS HERE", "#lightgray#NO MAIN ACTIONS HERE", new Vector2(-10, Measurements.FullScreen.Y - 40), new Vector2(Measurements.FullScreen.X + 20, Measurements.FullScreen.Y), new ButtonSignalEvent(), "br_thick", new GuiTransition(false, 0.5, new Vector2(0, PreviousActionMenuY - (Measurements.FullScreen.Y - 40))), new Vector2(0, +20 - Measurements.HalfScreen.Y)));
						else
							components.MainScreen.Buttons.Insert(0, new Button("#lightgray#NO MAIN ACTIONS HERE", "#lightgray#NO MAIN ACTIONS HERE", new Vector2(-10, Measurements.FullScreen.Y - 40), new Vector2(Measurements.FullScreen.X + 20, Measurements.FullScreen.Y), new ButtonSignalEvent(), "br_thick", null, new Vector2(0, +20 - Measurements.HalfScreen.Y)));
					}
					PreviousActionMenuY = Measurements.FullScreen.Y - 10 - (Measurements.EighthScreen.Y * 0.8f) * y;
					/*
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X - 33, 70), new Vector2(21, 21), new ButtonSignalEvent("zoom", "in"), "br_outline_round", null));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X - 32, 71), ContentLoader.Images["plus"], new Vector2(18, 18)));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X - 35, 100), ContentLoader.Images["magnify"], new Vector2(24, 24)));
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X - 33, 135), new Vector2(21, 21), new ButtonSignalEvent("zoom", "out"), "br_outline_round", null));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X - 32, 136), ContentLoader.Images["minus"], new Vector2(18, 18)));
					*/
					if (!isReload)
						AddFade(components, isReload, 0.5);
					if (master.worldManager.CurrentMap.Scrolls)
						components.Screens.Add(new ScrollScreen(new Vector2(0, 0), new Vector2(Measurements.FullScreen.X, Measurements.FullScreen.Y), new Vector2(0, 0), new Vector2(-960 + Measurements.FullScreen.X, Measurements.FullScreen.Y - 960), startScroll.ToPoint(),true, true));
					else
						components.Screens.Add(new ScrollScreen(new Vector2(0, 0), new Vector2(Measurements.FullScreen.X, Measurements.FullScreen.Y-960), new Vector2(0, 0), new Vector2(-480 + Measurements.FullScreen.X, Measurements.FullScreen.Y - 960), new Point((int)startScroll.X, 0), true, false));

					Vector2 size = new Vector2(master.worldManager.CurrentMap.MapSize.Width, master.worldManager.CurrentMap.MapSize.Height);

					foreach (MapArea area in master.worldManager.CurrentMap.MapAreas.Values)
					{
						if (area.IsDiscovered)
							components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(0, 0), area.Texture, size, 0.25f));
						if (area.Description == master.worldManager.CurrentMap.MapAreas[master.entityManager.Player.CurrentArea].Description)
						{
							components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(0, 0), area.Texture, size));
							if (!master.IsMapMoving)
							{

								foreach (TravelButtonData travelButton in area.TravelButtons)
								{
									components.Screens[0].Buttons.Add(new Button("", "", (travelButton.Position - new Vector2(10, 10)), new Vector2(21, 21), new ButtonSignalEvent("travel", ((int)travelButton.NextArea).ToString()+","+ ((int)travelButton.NextMap).ToString()), "blue_outline_round"));
									components.Screens[0].BasicComponents.Add(new ImagePanel(travelButton.Position - new Vector2(9, 9), ContentLoader.Images[travelButton.ButtonDirection + "_arrow"], new Vector2(18, 18)));
								}
								foreach (MapInteractionButton interactionButton in area.InteractionButtons)
								{
									components.Screens[0].InteractionButtons.Add(interactionButton);
								}
							}
						}

						// if active add a fading image over the faint one
					}
					Vector2 position = master.worldManager.CurrentMap.MapAreas[master.entityManager.Player.CurrentArea].CenterPos;
					if(!master.IsMapMoving)
						DrawPlayer(master, components, position);

					if (master.worldManager.CurrentNpc != "")
					{
						Npc npc = NpcInfo.Npcs[master.worldManager.CurrentNpc];
						Dialog dialog = npc.Dialogs[npc.TalkStage][npc.CurrentDialog];
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(Vector2.Zero, ContentLoader.Images["blackout"], Measurements.FullScreen, 0.5f));
						ImagePanel image = new ImagePanel(new Vector2(Measurements.EighthScreen.X / 2, Measurements.ThreeQuarterScreen.Y - Measurements.QuarterScreen.X + 30), npc.Texture, new Vector2(Measurements.QuarterScreen.X, Measurements.QuarterScreen.X));

						if (isReload)
							components.MainScreen.BasicComponents.Add(image);
						else
							components.MainScreen.FadingImages.Add(new FadingImage(image, "in", 1));
						components.MainScreen.Buttons.Add(new Button("", "", new Vector2(-10, Measurements.ThreeQuarterScreen.Y+40), new Vector2(Measurements.FullScreen.X + 20, Measurements.QuarterScreen.Y + 110), new ButtonSignalEvent()));
						components.MainScreen.BasicComponents.Add(new TextBox("#lightbronze#"+npc.Dialogs[npc.TalkStage][npc.CurrentDialog].Text, new Vector2(5, Measurements.ThreeQuarterScreen.Y+50), (int)Measurements.FullScreen.X - 85, "none"));
						if (dialog.Choice1 == null && dialog.Choice2 == null)
							components.MainScreen.Buttons.Add(new Button(">>>", "#gray#>>>", new Vector2(Measurements.HalfScreen.X+20, Measurements.ThreeQuarterScreen.Y-10), new Vector2(Measurements.HalfScreen.X -30, 30), new ButtonSignalEvent("npc", "continue")));
						if (dialog.Choice1 != null)
							components.MainScreen.Buttons.Add(new Button(dialog.Choice1.Text, "#bronze#" + dialog.Choice1.Text, new Vector2(Measurements.HalfScreen.X + 20, Measurements.ThreeQuarterScreen.Y - 10), new Vector2(Measurements.HalfScreen.X - 30, 30), new ButtonSignalEvent("npc", dialog.Choice1.Action), "br_outline_round"));
						if (dialog.Choice2 != null)
							components.MainScreen.Buttons.Add(new Button(dialog.Choice2.Text, "#bronze#" + dialog.Choice2.Text, new Vector2(Measurements.HalfScreen.X + 20, Measurements.ThreeQuarterScreen.Y - 60), new Vector2(Measurements.HalfScreen.X - 30, 30), new ButtonSignalEvent("npc", dialog.Choice2.Action), "br_outline_round"));
					}

					break;

				case "inventory":
					master.entityManager.Player.Inventory.AddItem("palm log");
					master.entityManager.Player.Inventory.AddItem("palm hat");
					master.entityManager.Player.Inventory.AddItem("guard_helm");
					master.entityManager.Player.Inventory.AddItem("palm sword");
					master.entityManager.Player.Inventory.AddItem("leather tunic");
					master.entityManager.Player.Inventory.AddItem("leather leggings");
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(-10, -10), new Vector2(Measurements.FullScreen.X + 20, Measurements.EighthScreen.Y), new ButtonSignalEvent(), "br_thick", null, new Vector2(20, 10)));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(0, Measurements.EighthScreen.Y - 3), ContentLoader.Images["gradient_edge"], new Vector2(Measurements.FullScreen.X, 15)));
					string categoryHeader = "";
					components.MainScreen.Buttons.Add(new Button("<", "#lightred#<", new Vector2(5, 15), new Vector2(30, Math.Min(30,Measurements.FullScreen.X/6)), new ButtonSignalEvent("change_menu", "world_map"), "br_thin", null));
					string boxStyle = "category_outline";
					Inventory inventory = master.entityManager.Player.Inventory;
					List<ItemIndexPair> smallerList = new List<ItemIndexPair>();
					if (master.entityManager.Player.Inventory.CurrentCategory == "equipment")
					{
						boxStyle = "category_outline_sl";
						categoryHeader = "Equipment";
					}
					else
						boxStyle = "category_outline";
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X / 6 * 1 + 3, 0), new Vector2(Measurements.FullScreen.X / 6 - 6, Measurements.EighthScreen.Y - 13), new ButtonSignalEvent("change_inv_cat", "equipment"), boxStyle));

					if (master.entityManager.Player.Inventory.CurrentCategory == "armour")
					{
						smallerList = inventory.GetOnlyArmour();
						boxStyle = "category_outline_sl";
						categoryHeader = "Armour + Accessories";
					}
					else
						boxStyle = "category_outline";
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X / 6 * 2 + 3, 0), new Vector2(Measurements.FullScreen.X / 6 - 6, Measurements.EighthScreen.Y - 13), new ButtonSignalEvent("change_inv_cat", "armour"), boxStyle));

					if (master.entityManager.Player.Inventory.CurrentCategory == "weapons")
					{
						smallerList = inventory.GetOnlyWeapons();
						boxStyle = "category_outline_sl";
						categoryHeader = "Weapons";
					}
					else
						boxStyle = "category_outline"; 
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X / 6 * 3 + 3, 0), new Vector2(Measurements.FullScreen.X / 6 - 6, Measurements.EighthScreen.Y - 13), new ButtonSignalEvent("change_inv_cat", "weapons"), boxStyle));

					if (master.entityManager.Player.Inventory.CurrentCategory == "key")
					{
						smallerList = inventory.GetOnlyKey();

						categoryHeader = "Key/Quest Items";
						boxStyle = "category_outline_sl";
					}
					else
						boxStyle = "category_outline"; 
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X / 6 * 4 + 3, 0), new Vector2(Measurements.FullScreen.X / 6 - 6, Measurements.EighthScreen.Y - 13), new ButtonSignalEvent("change_inv_cat", "key"), boxStyle));

					if (master.entityManager.Player.Inventory.CurrentCategory == "materials")
					{
						smallerList = inventory.GetOnlyMisc();

						categoryHeader = "Food + Materials";
						boxStyle = "category_outline_sl";
					}
					else
						boxStyle = "category_outline"; 
					components.MainScreen.Buttons.Add(new Button("", "", new Vector2(Measurements.FullScreen.X / 6 * 5 + 3, 0), new Vector2(Measurements.FullScreen.X / 6 - 6, Measurements.EighthScreen.Y - 13), new ButtonSignalEvent("change_inv_cat", "materials"), boxStyle));

					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X / 6 * 1 + 7, 2), ContentLoader.Images["equipment"], new Vector2(29, 46)));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X / 6 * 2 + 7, 2), ContentLoader.Images["armour_category"], new Vector2(29, 46)));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X / 6 * 3 + 7, 2), ContentLoader.Images["weapon_category"], new Vector2(29, 46)));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X / 6 * 4 + 7, 2), ContentLoader.Images["key_category"], new Vector2(29, 46)));
					components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.FullScreen.X / 6 * 5 + 7, 2), ContentLoader.Images["misc_category"], new Vector2(29, 46)));



					if (!isReload)
					{
						inventory.SelectedItem = "";
						inventory.SelectedIndex = -1;
					}


					if (inventory.CurrentCategory != "equipment")
					{
						components.Screens.Add(new ScrollScreen(new Vector2(0, Measurements.EighthScreen.Y - 8), new Vector2(Measurements.FullScreen.X, Measurements.ThreeQuarterScreen.Y + 24), new Vector2(0, Measurements.EighthScreen.Y + 20), new Vector2(0, Measurements.EighthScreen.Y - master.entityManager.Player.Inventory.MaxItems / 4 * Measurements.QuarterScreen.X + 40), new Point(0, (int)Measurements.EighthScreen.Y + 20), false, true));
						components.Screens[0].BasicComponents.Add(new TextBox(categoryHeader, new Vector2(10, -10), (int)Measurements.FullScreen.X, "none", "centre"));

						for (int i = 0; i < 10; i++)
						{
							for (int j = 0; j < 4; j++)
							{
								Vector2 pos = new Vector2(j * Measurements.QuarterScreen.X + 5, i * Measurements.QuarterScreen.X - 40 + Measurements.EighthScreen.Y);
								if (smallerList.Count > i * 4 + j)
								{
									components.Screens[0].BasicComponents.Add(new ImagePanel(pos + new Vector2(5), smallerList[i * 4 + j].Item.Texture, new Vector2(Measurements.QuarterScreen.X - 20)));

									if (!master.entityManager.Player.Equipment.IsEquipped(smallerList[i * 4 + j].Index))
										components.Screens[0].Buttons.Add(new Button("", "", pos, new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent("item_info", smallerList[i * 4 + j].Item.Name+','+ smallerList[i * 4 + j].Index), "br_outline_round"));
									else
										components.Screens[0].Buttons.Add(new Button("#lightbronze#E", "#lightbronze#E", pos, new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent("item_info", smallerList[i * 4 + j].Item.Name +','+ smallerList[i * 4 + j].Index), "br_thin", null, new Vector2(-Measurements.EighthScreen.X +15)));
								}


								else
									components.Screens[0].Buttons.Add(new Button("", "", pos, new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent(), "br_outline_round"));
							}
						}
					}
					else
					{
						components.MainScreen.BasicComponents.Add(new TextBox(categoryHeader, new Vector2(10, Measurements.EighthScreen.Y + 10), (int)Measurements.FullScreen.X, "none", "centre"));

						Vector2 pos = new Vector2(Measurements.HalfScreen.X + Measurements.EighthScreen.X / 2, Measurements.QuarterScreen.Y - Measurements.EighthScreen.Y / 2);
						Vector2 characSize = new Vector2(Measurements.ThreeEighthScreen.X, Measurements.ThreeEighthScreen.X * 30 / 16);
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.UniqueImage(ContentLoader.Images["head_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]], 0), characSize));
						 components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.Images["eyes_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[2]], characSize));
						components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.Images["body_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]], characSize));
						if (master.entityManager.Player.Equipment.GetLegs(master) != null)
							components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.Images["legs_"+ master.entityManager.Player.Equipment.GetLegs(master).Name.Replace(' ', '_')], characSize));
						else
							components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.Images["legs_gray_shorts"], characSize));


						if (master.entityManager.Player.Equipment.GetChest(master) != null)
							components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.Images["chest_" + master.entityManager.Player.Equipment.GetChest(master).Name.Replace(' ', '_')], characSize));
						else
							components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.Images["chest_leaf_shirt"], characSize));

						if (master.entityManager.Player.Equipment.GetHead(master) != null)
							components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.Images["head_" + master.entityManager.Player.Equipment.GetHead(master).Name.Replace(' ', '_')], characSize));
						else
							components.MainScreen.BackgroundComponents.Add(new ImagePanel(pos, ContentLoader.Images[master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] + "_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]], characSize));

						EquipmentHolder equipment = master.entityManager.Player.Equipment;
						if (equipment.Head > -1)
						{
							components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(10, Measurements.QuarterScreen.Y - 15), equipment.GetHead(master).Texture, new Vector2(Measurements.QuarterScreen.X - 20)));
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5, Measurements.QuarterScreen.Y-20), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent("item_info", equipment.GetHead(master).Name + ',' +equipment.Head.ToString()), "br_outline_round"));
						}
						else
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5, Measurements.QuarterScreen.Y-20), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent(), "br_outline_round"));

						if (equipment.Chest > -1)
						{
							components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(10, Measurements.QuarterScreen.Y - 15 + Measurements.QuarterScreen.X), equipment.GetChest(master).Texture, new Vector2(Measurements.QuarterScreen.X - 20)));
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5, Measurements.QuarterScreen.Y-20 + Measurements.QuarterScreen.X), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent("item_info", equipment.GetChest(master).Name + ',' + equipment.Chest.ToString()), "br_outline_round"));
						}
						else
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5, Measurements.QuarterScreen.Y - 20 + Measurements.QuarterScreen.X), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent(), "br_outline_round"));

						if (equipment.Legs > -1)
						{
							components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(10, Measurements.QuarterScreen.Y - 15 + Measurements.QuarterScreen.X*2), equipment.GetLegs(master).Texture, new Vector2(Measurements.QuarterScreen.X - 20)));
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5, Measurements.QuarterScreen.Y - 20 + Measurements.QuarterScreen.X*2), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent("item_info", equipment.GetLegs(master).Name + ',' + equipment.Legs.ToString()), "br_outline_round"));
						}
						else
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5, Measurements.QuarterScreen.Y - 20 + Measurements.QuarterScreen.X*2), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent(), "br_outline_round"));


						if (equipment.Weapon > -1)
						{
							components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(10 + Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y - 15), equipment.GetWeapon(master).Texture, new Vector2(Measurements.QuarterScreen.X - 20)));
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5 + Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y - 20), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent("item_info", equipment.GetWeapon(master).Name +','+ equipment.Weapon.ToString()), "br_outline_round"));
						}
						else
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5 + Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y - 20), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent(), "br_outline_round"));

						if (equipment.Accessory > -1)
						{
							components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(10 + Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y -15 + Measurements.QuarterScreen.X), equipment.GetAccessory(master).Texture, new Vector2(Measurements.QuarterScreen.X - 20)));
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5 + Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y - 20 + Measurements.QuarterScreen.X), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent("item_info", equipment.GetAccessory(master).Name + ',' + equipment.Accessory.ToString()), "br_outline_round"));
						}
						else
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5 + Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y - 20 + Measurements.QuarterScreen.X), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent(), "br_outline_round"));

						components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(10 + Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y - 15 + Measurements.QuarterScreen.X * 2), ContentLoader.Images["stats"], new Vector2(Measurements.QuarterScreen.X - 20)));
						components.MainScreen.Buttons.Add(new Button("", "", new Vector2(5 + Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y - 20 + Measurements.QuarterScreen.X*2), new Vector2(Measurements.QuarterScreen.X - 10), new ButtonSignalEvent("item_info", "clear"), "br_thin"));

					}

					if (inventory.SelectedItem == "")
						if(inventory.CurrentCategory != "equipment")
							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(-10, Measurements.FullScreen.Y - Measurements.EighthScreen.Y + 10), new Vector2(Measurements.FullScreen.X + 20, Measurements.EighthScreen.Y), new ButtonSignalEvent(), "br_thick", null, new Vector2(0, 10)));
						else
						{

							components.MainScreen.Buttons.Add(new Button("", "", new Vector2(-10, Measurements.FullScreen.Y - Measurements.QuarterScreen.Y + 10), new Vector2(Measurements.FullScreen.X + 20, Measurements.FullScreen.Y), new ButtonSignalEvent(), "br_thick", null, new Vector2(0, 10)));
							components.MainScreen.BasicComponents.Add(new TextBox("STATS:", new Vector2(10, Measurements.ThreeQuarterScreen.Y + 20), (int)Measurements.FullScreen.X, "none", "left"));
							components.MainScreen.BasicComponents.Add(new TextBox("#lightgray#" + master.entityManager.Player.FormatTotalStats(), new Vector2(10, Measurements.ThreeQuarterScreen.Y + 45), (int)Measurements.FullScreen.X - 40, "none", "left"));

						}
					else
					{
						Item item = ItemInfo.ItemDict[inventory.SelectedItem];
						components.MainScreen.Buttons.Add(new Button("", "", new Vector2(-10, Measurements.FullScreen.Y - Measurements.QuarterScreen.Y - 10), new Vector2(Measurements.FullScreen.X + 20, Measurements.FullScreen.Y), new ButtonSignalEvent(), "br_thick", null, new Vector2(0, 10)));
						components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.EighthScreen.X*0.68f, Measurements.ThreeQuarterScreen.Y- Measurements.HalfScreen.X * 0.8f-11), ContentLoader.Images["item_gradient_bg"], new Vector2(Measurements.HalfScreen.X*0.8f)));
						components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(Measurements.EighthScreen.X * 0.68f+20, Measurements.ThreeQuarterScreen.Y - Measurements.HalfScreen.X * 0.8f +14), ItemInfo.ItemDict[inventory.SelectedItem].Texture, new Vector2(Measurements.HalfScreen.X * 0.8f - 40), 1, "white"));
						components.MainScreen.BasicComponents.Add(new TextBox(Capitalise(item.Name), new Vector2(10, Measurements.ThreeQuarterScreen.Y), (int)Measurements.FullScreen.X, "none", "left"));
						components.MainScreen.BasicComponents.Add(new TextBox("#lightgray#"+item.Desc, new Vector2(10, Measurements.ThreeQuarterScreen.Y + 25), (int)Measurements.FullScreen.X-40, "none", "left"));

						if (item.IsEquipable)
						{
							components.MainScreen.BasicComponents.Add(new TextBox("#lightyellow#" + item.FormatStats(), new Vector2(10, Measurements.ThreeQuarterScreen.Y + 50), (int)Measurements.FullScreen.X - 40, "none", "left"));

							if (master.entityManager.Player.Equipment.IsEquipped(inventory.SelectedIndex))
							{
								components.MainScreen.Buttons.Add(new Button("#lightgray#EQUIP", "#gray#EQUIP", new Vector2(20, Measurements.ThreeQuarterScreen.Y + Measurements.EighthScreen.Y + 15), new Vector2(Measurements.ThreeEighthScreen.X - 10, Measurements.EighthScreen.Y / 2), new ButtonSignalEvent(), "br_outline", null));
								components.MainScreen.Buttons.Add(new Button("#lightred#UNEQUIP", "#red#UNEQUIP", new Vector2(Measurements.FullScreen.X - Measurements.ThreeEighthScreen.X - 10, Measurements.ThreeQuarterScreen.Y + Measurements.EighthScreen.Y + 15), new Vector2(Measurements.ThreeEighthScreen.X - 10, Measurements.EighthScreen.Y / 2), new ButtonSignalEvent("unequip", item.Name), "red_outline", null));
							}
							else
							{
								components.MainScreen.Buttons.Add(new Button("#lightgreen#EQUIP", "#green#EQUIP", new Vector2(20, Measurements.ThreeQuarterScreen.Y + Measurements.EighthScreen.Y + 15), new Vector2(Measurements.ThreeEighthScreen.X - 10, Measurements.EighthScreen.Y / 2), new ButtonSignalEvent("equip", item.Name+','+inventory.SelectedIndex.ToString()), "green_outline", null));
								components.MainScreen.Buttons.Add(new Button("#lightgray#UNEQUIP", "#gray#UNEQUIP", new Vector2(Measurements.FullScreen.X - Measurements.ThreeEighthScreen.X - 10, Measurements.ThreeQuarterScreen.Y + Measurements.EighthScreen.Y + 15), new Vector2(Measurements.ThreeEighthScreen.X - 10, Measurements.EighthScreen.Y / 2), new ButtonSignalEvent(), "br_outline", null));
							}
						}
					}
					break;
			}
			return components;
		}
		static private GuiContent DrawPlayer(MasterManager master, GuiContent components, Vector2 position)
		{
			Vector2 offset = Vector2.Zero;
			components.Screens[0].BackgroundComponents.Add(new ImagePanel(position + offset, ContentLoader.UniqueImage(ContentLoader.Images["head_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[3]], 0), new Vector2(32, 60)));
			components.Screens[0].BackgroundComponents.Add(new ImagePanel(position + offset, ContentLoader.Images[master.storedDataManager.CurrentSaveFile.CharacterAppearance[0] + "_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[1]], new Vector2(32, 60)));
			components.Screens[0].BackgroundComponents.Add(new ImagePanel(position + offset, ContentLoader.Images["eyes_" + master.storedDataManager.CurrentSaveFile.CharacterAppearance[2]], new Vector2(32, 60)));

			return components;
		}

		static private string Capitalise(string words)
		{
			string capWord = "";
			foreach(string word in words.Split(' '))
			{
				capWord += word[0].ToString().ToUpper();
				for (int i = 1; i<word.Length; i++)
				{
					capWord += word[i];

				}
				capWord += ' ';
			}
			return capWord;
		}
		static private GuiContent AddBuyLayout(bool isReload, MasterManager master, GuiContent components, string name, string desc, int cost, string currency, string backMenu)
		{
			int playerCurrencyAvailable = 0;
			switch (currency)
			{
				case "coin":
					{
						playerCurrencyAvailable = 0;// master.storedDataManager.CurrentSaveFile.Coins;
						break;
					}
			}

			components.MainScreen.BasicComponents.Add(new TextBox($": {playerCurrencyAvailable}", new Vector2(380, 4), 100, "none"));
			components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(360, 4), ContentLoader.Images[currency], new Vector2(16, 16)));

			components.MainScreen.Buttons.Add(new Button("", "", new Vector2(8, 8), new Vector2(32, 16), new ButtonSignalEvent("change_menu", backMenu)));

			components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(7, 7), ContentLoader.Images["arrow_icon"]));
			components.MainScreen.BasicComponents.Add(new TextBox(name, new Vector2(60, 12), 300, "black"));
			components.MainScreen.BasicComponents.Add(new TextBox(desc, new Vector2(20, 48), 200, "black"));
			components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(0, 0), ContentLoader.Images["stars"], new Vector2(426, 233)));
			components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(287, 50), ContentLoader.Images[name.Replace(' ','_')], new Vector2(120, 120)));



			//check if item is already bought
			if (true)//!master.storedDataManager.CurrentSaveFile.Purchases.Contains(name.Replace(' ', '_')))
			{
				Vector2 offset = new Vector2(-5, 0);
				if (cost < 100 && cost > 9)
					offset = new Vector2(0, 0);
				else if (cost < 1000)
					offset = new Vector2(5, 0);
				else if (cost < 10000)
					offset = new Vector2(10, 0);
				else if (cost < 100000)
					offset = new Vector2(15, 0);
				//maybe possibility for 2 currencies to purchase later
				else
				{
				}
			}
			else
			{
				components.MainScreen.Buttons.Add(new Button($"#grey#BOUGHT", $"#grey#BOUGHT", new Vector2(60, 185), new Vector2(140, 32), new ButtonSignalEvent()));
			}
			
			return components;
		}

		static private GuiContent AddFade(GuiContent components, bool isReload = false, double time = 0.3, string image = "blackout")
		{
			ImagePanel totalFadein = new ImagePanel(new Vector2(0, 0), ContentLoader.Images[image], Measurements.FullScreen);
			ImagePanel totalFadeout = new ImagePanel(new Vector2(0, 0), ContentLoader.Images[image], Measurements.FullScreen);
			if (!isReload)
			{
				components.MainScreen.FadingImages.Add(new FadingImage(totalFadein, "in", time, false, 0, true));
				components.MainScreen.FadingImages.Add(new FadingImage(totalFadeout, "out", time, true));
			}
			return components;
		}
		static private string Rainbowify(string text)
		{
			List<string> colours = new List<string> {"#red#", "#orange#", "#yellow#" , "#green#", "#blue#", "#purple#" };
			string rainbowText = "";
			for (int i = 0; i < text.Length; i++)
			{
				rainbowText += colours[i%6] + text[i];
			}
			rainbowText += "#white#";
			return rainbowText;
		}
		
	}
		
}
		