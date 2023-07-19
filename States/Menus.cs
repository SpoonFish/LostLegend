using LostLegend.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Text;
using LostLegend.Master;
using Microsoft.Xna.Framework;
using LostLegend.Statics;
using LostLegend.Graphics.GUI.Interactions;
using System.ComponentModel.Design;

namespace LostLegend.States
{
    static class Menus
    {
        // Different object for fade in/out because if you have both in one menu then their opacities would override each other constantly

        static private ImagePanel totalFadein;
        static private ImagePanel totalFadeout;
        static public void LoadDefaultGuiComponents(MasterManager master)
        {
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
                    components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(Measurements.EighthScreen.X, Measurements.EighthScreen.X), ContentLoader.Images["lostlegend_logo"], new Vector2(Measurements.ThreeQuarterScreen.X, Measurements.ThreeQuarterScreen.X/3*2)));
                    //components.MainScreen.BackgroundComponents.Add(new ImagePanel(new Vector2(0, 0), ContentLoader.Images["test2"], master.ScreenSize));
                    //ImagePanel logo = new ImagePanel(new Vector2(0, 0), ContentLoader.Images["spextria_logo"], new Vector2(426, 233));
                    //components.MainScreen.BasicComponents.Add(logo);
                    //components.FadingImages.Add(new FadingImage(logo, "in", 1.5));
                    AddFade(components, false, 0.5);

                    components.MainScreen.Buttons.Add(new Button("#white#PLAY", "#lightyellow#PLAY", new Vector2(Measurements.QuarterScreen.X, Measurements.QuarterScreen.Y + 90), new Vector2(Measurements.HalfScreen.X, 40), new ButtonSignalEvent("change_menu", "titlescreen")));
                   
                    components.MainScreen.Buttons.Add(new Button("#white#SETTINGS", "#lightblue#SETTINGS", new Vector2(Measurements.QuarterScreen.X, Measurements.HalfScreen.Y + 55), new Vector2(Measurements.HalfScreen.X, 40), new ButtonSignalEvent("change_menu", "opening1")));
                  
                    components.MainScreen.Buttons.Add(new Button("#white#EXIT", "#lightred#EXIT", new Vector2(Measurements.QuarterScreen.X, Measurements.ThreeQuarterScreen.Y + 20), new Vector2(Measurements.HalfScreen.X, 40), new ButtonSignalEvent("exit")));
                    break;

               
            }
            return components;
        }
        static private List<string> CheckWeaponUnlocked(List<string> list, string weaponName, MasterManager master)
        {
            //if (master.storedDataManager.CheckSkillUnlock(weaponName))
            //    list.Add(weaponName);
            return list;

        }
        static private GuiContent AddWeaponSlots(bool isReload, MasterManager master, GuiContent components)
        {
            int x = 0;
            int y = 0;
            List<string> availableWeapons = new List<string>();
            availableWeapons = CheckWeaponUnlocked(availableWeapons, "slash", master);
            availableWeapons = CheckWeaponUnlocked(availableWeapons, "smash", master);
            availableWeapons = CheckWeaponUnlocked(availableWeapons, "skew", master);
            availableWeapons = CheckWeaponUnlocked(availableWeapons, "stab", master);
            availableWeapons = CheckWeaponUnlocked(availableWeapons, "steel_fists", master);
            availableWeapons = CheckWeaponUnlocked(availableWeapons, "bolt", master);

            components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(213, y+1), ContentLoader.Images["neutral_category_separator"], new Vector2(213, 32)));
            components.Screens[0].BackgroundComponents.Add(new TextBox("#white#NEUTRAL", new Vector2(213+10, y+10), 213, "none"));
            y += 1;
            foreach(string weapon in availableWeapons)
            {
                components.Screens[0].Buttons.Add(new ButtonImage(new ImagePanel(new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), ContentLoader.Images["weapon_slot"]), new ImagePanel(new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), ContentLoader.Images["weapon_slot_hovered"]), new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), new Vector2(32, 32), new ButtonSignalEvent("change_weapon", weapon)));
                components.Screens[0].BasicComponents.Add(new ImagePanel(new Vector2(213 + 7 + x * 35, 0 + 7 + y * 35), ContentLoader.Images[weapon]));
                x += 1;
                if (x > 6)
                {
                    y += 1;
                    x = 0;
                }
            }
            availableWeapons.Clear();
            availableWeapons = CheckWeaponUnlocked(availableWeapons, "gold_fists", master);
            components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(213, (y+1)*35), ContentLoader.Images["light_category_separator"], new Vector2(213,32)));
            components.Screens[0].BackgroundComponents.Add(new TextBox("#yellow#LIGHT", new Vector2(213 + 10, y*35 + 43), 213, "none"));
            y += 1;
            x = 0;
            if (availableWeapons.Count != 0)
                y += 1;
            foreach (string weapon in availableWeapons)
            {
                components.Screens[0].Buttons.Add(new ButtonImage(new ImagePanel(new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), ContentLoader.Images["weapon_slot"]), new ImagePanel(new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), ContentLoader.Images["weapon_slot_hovered"]), new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), new Vector2(32, 32), new ButtonSignalEvent("change_weapon", weapon)));
                components.Screens[0].BasicComponents.Add(new ImagePanel(new Vector2(213 + 7 + x * 35, 0 + 7 + y * 35), ContentLoader.Images[weapon]));
                x += 1;
                if (x > 6)
                {
                    y += 1;
                    x = 0;
                }
            }

            availableWeapons.Clear();
            availableWeapons = CheckWeaponUnlocked(availableWeapons, "torch", master);
            components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(213, (y + 1) * 35), ContentLoader.Images["flame_category_separator"], new Vector2(213, 32)));
            components.Screens[0].BackgroundComponents.Add(new TextBox("#orange#FLAME", new Vector2(213 + 10, y * 35 + 43), 213, "none"));
            y += 1;
            x = 0;
            if (availableWeapons.Count != 0)
                y += 1;
            foreach (string weapon in availableWeapons)
            {
                components.Screens[0].Buttons.Add(new ButtonImage(new ImagePanel(new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), ContentLoader.Images["weapon_slot"]), new ImagePanel(new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), ContentLoader.Images["weapon_slot_hovered"]), new Vector2(213 + 3 + x * 35, 0 + 3 + y * 35), new Vector2(32, 32), new ButtonSignalEvent("change_weapon", weapon)));
                components.Screens[0].BasicComponents.Add(new ImagePanel(new Vector2(213 + 7 + x * 35, 0 + 7 + y * 35), ContentLoader.Images[weapon]));
                x += 1;
                if (x > 6)
                {
                    y += 1;
                    x = 0;
                }
            }
            availableWeapons.Clear();

            components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(213, (y + 1) * 35), ContentLoader.Images["growth_category_separator"], new Vector2(213, 32)));
            components.Screens[0].BackgroundComponents.Add(new TextBox("#green#GROWTH", new Vector2(213 + 10, y * 35 + 43), 213, "none"));
            y += 1;
            x = 0;
            if (availableWeapons.Count != 0)
                y += 1;
            components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(213, (y + 1) * 35), ContentLoader.Images["frost_category_separator"], new Vector2(213, 32)));
            components.Screens[0].BackgroundComponents.Add(new TextBox("#blue#FROST", new Vector2(213 + 10, y * 35 + 43), 213, "none"));
            y += 1;
            x = 0;
            if (availableWeapons.Count != 0)
                y += 1;
            components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(213, (y + 1) * 35), ContentLoader.Images["shadow_category_separator"], new Vector2(213, 32)));
            components.Screens[0].BackgroundComponents.Add(new TextBox("#purple#SHADOW", new Vector2(213 + 10, y * 35 + 43), 213, "none"));
            y += 1;
            x = 0;
            if (availableWeapons.Count != 0)
                y += 1;
            components.Screens[0].BackgroundComponents.Add(new ImagePanel(new Vector2(213, (y + 1) * 35), ContentLoader.Images["torment_category_separator"], new Vector2(213, 32)));
            components.Screens[0].BackgroundComponents.Add(new TextBox("#red#TORMENT", new Vector2(213 + 10, y * 35 + 43), 213, "none"));
            y += 1;
            x = 0;
            if (availableWeapons.Count != 0)
                y += 1;
            return components;
        }
        static private string AddCutsceneTrigger(MasterManager master, int level, string cutsceneName)
        {

            if (true)//master.storedDataManager.CurrentSaveFile.CurrentLevel == level && !master.storedDataManager.CurrentSaveFile.Cutscenes.Contains(cutsceneName))
            {
                //master.storedDataManager.CurrentSaveFile.Cutscenes.Add(cutsceneName);
                return cutsceneName + "-1";
            }
            else
                return "exit";
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
            components.MainScreen.BasicComponents.Add(new TextBox(name, new Vector2(60, 12), 300, "black", true));
            components.MainScreen.BasicComponents.Add(new TextBox(desc, new Vector2(20, 48), 200, "black", true));
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
                if (isReload)
                {
                    components.MainScreen.Buttons.Add(new Button($"CANT AFFORD", $"#red#CANT AFFORD", new Vector2(60, 185), new Vector2(140, 32), new ButtonSignalEvent(), "button_red", "button_red_hovered", "button_red_clicked"));
                }
                else
                {
                    if (name != "info")
                    {
                        components.MainScreen.Buttons.Add(new Button($"#white#BUY: {cost}", $"#yellow#BUY: {cost}", new Vector2(60, 185), new Vector2(140, 32), new ButtonSignalEvent("purchase", name.Replace(' ', '_') + $"/{cost}/{currency}"), "button_yellow", "button_yellow_hovered", "button_yellow_clicked", true, new Vector2(-10, 0)));
                        components.MainScreen.BasicComponents.Add(new ImagePanel(new Vector2(152, 192) + offset, ContentLoader.Images[currency], new Vector2(16, 16)));
                    }
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
            components.MainScreen.FadingImages.Add(new FadingImage(totalFadein, "in", time, false, 0, true));
            if (!isReload)
                components.MainScreen.FadingImages.Add(new FadingImage(totalFadeout, "out", time, true));
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
        