using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using System.IO.IsolatedStorage;
using Java.Lang;
using Android.Content;
using static Android.Telephony.CarrierConfigManager;
using Org.Apache.Http.Authentication;
using LostLegend;
using Java.Lang.Reflect;
using LostLegend.Statics;

namespace Spextria.StoredData
{
    class StoredDataManager
    {
        private SaveFile DefaultSaveFile;
        private SettingsData DefaultSettings;
        public Context CurrentContext { get; set; }
        public SettingsData Settings;
        public SaveFile CurrentSaveFile;
        private IsolatedStorageFile Store;
        public List<SaveFile> SaveFiles;
        public int CurrentSaveNumber;
        public int NextAvailableSaveSlot;
        public StoredDataManager()
        {
            Store = IsolatedStorageFile.GetUserStoreForApplication();
            DefaultSettings = new SettingsData()
            {
                MusicVolume = 100,
                SoundVolume = 100,
            };
            //SaveSettings(DefaultSettings);
            // Settings = LoadSettings();

            DefaultSaveFile = new SaveFile()
            {
                Name = "EMPTY",
                New = true,
                Coins = 0,
                ShowIntro = true,
                CharacterAppearance = new List<string>() { "hair2", "brown", "blue", "tone1" },
                CurrentArea = ((int)MapInfo.MapAreaIDs.CentralLithramVillage),
                DiscoveredAreas = new List<int>() { (int)MapInfo.MapAreaIDs.CentralLithramVillage}
            };



            //SaveSettings(DefaultSettings);

            if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"Settings.json")))
            {
                Settings = new SettingsData() { MusicVolume = 100, SoundVolume = 100};
                SaveSettings();
            }
            for (int i = 0; i < 10; i ++)
            {

                //DebugSaveFile(i);
                if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"Save{i}.json")))
                {
                    DebugSaveFile(i);
                }
            }
            
            Settings = LoadSettings();
            SaveFiles = new List<SaveFile>();
            OrderSaveFiles();



        }

        public void OrderSaveFiles()
        {
            SaveFiles.Clear();
            for (int i = 0; i < 10; i++)
            {
                SaveFile file = LoadFile(i);
                if (!file.New)
                {
                    SaveFiles.Add(file);
                }


            }
            NextAvailableSaveSlot = SaveFiles.Count;

            for (int i = SaveFiles.Count; i < 10; i++)
            {
                DebugSaveFile(i);
                SaveFiles.Add(LoadFile(i));
            }
        }
        public void SaveSettings()
        {



            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"Settings.json");
            string serializedText = JsonSerializer.Serialize<SettingsData>(Settings);
            Trace.WriteLine(serializedText);
            using (var stream = File.OpenWrite(filePath))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(serializedText);
                stream.Write(bytes);
            }

        }
        public bool CheckSkillUnlock(string skillName)
        {
            return false;
            //return CurrentSaveFile.Purchases.Contains(skillName);
        }

        public SettingsData LoadSettings()
        {
            string settingsData = "";
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"Settings.json")))
            {
                settingsData = sr.ReadToEnd();
            }
            return JsonSerializer.Deserialize<SettingsData>(settingsData);

        }

        public void ResetSettings()
        {
            string serializedText = JsonSerializer.Serialize<SettingsData>(DefaultSettings);
            Trace.WriteLine(serializedText);
            File.WriteAllText("settings.json", serializedText);
            Settings = LoadSettings();
        }


        public void DebugSaveFile(int number)
        {

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"Save{number}.json");
            string serializedText = JsonSerializer.Serialize<SaveFile>(DefaultSaveFile);
            Trace.WriteLine(serializedText);
            using (var stream = File.OpenWrite(filePath))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(serializedText);
                stream.Write(bytes);
            }
        }
        public void SaveFile(int number = -1, SaveFile saveFile = null)
        {
            if (saveFile == null)
                saveFile = CurrentSaveFile;
            if (number == -1)
                number = CurrentSaveNumber;
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"Save{number}.json");
            string serializedText = JsonSerializer.Serialize<SaveFile>(saveFile);
            Trace.WriteLine(serializedText);
            using (var stream = File.OpenWrite(filePath))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(serializedText);
                stream.Write(bytes);
            }
        }

        public SaveFile LoadFile(int number)
        {
            string path = $"Save{number}.json";
            string saveFileData = "";
            using (StreamReader sr = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), path)))
            {
                saveFileData = sr.ReadToEnd();
            }
            if (saveFileData.EndsWith("]}]}"))
                saveFileData = saveFileData.Remove(saveFileData.Length - 2, 2);
            if (saveFileData.EndsWith("]}}"))
                saveFileData = saveFileData.Remove(saveFileData.Length - 1, 1);

            try
            {

                return JsonSerializer.Deserialize<SaveFile>(saveFileData);
            }
            catch
            {
                return DefaultSaveFile;
            }
           

        }

        public void LoadSelectedFile(int number)
        {
            CurrentSaveNumber = number;
            string path = $"Save{number}.json";
            var saveFileData = File.ReadAllText(path);
            CurrentSaveFile = JsonSerializer.Deserialize<SaveFile>(saveFileData);

        }

        public void SortSavefiles()
        {
            List<SaveFile> tempList = new List<SaveFile>() { };
            foreach (SaveFile file in SaveFiles) 
            {
                tempList.Add(file);           
            }
            SaveFiles.Clear();
            foreach (SaveFile file in tempList)
            {
                if (!file.New)
                    SaveFiles.Add(file);
                else
                    SaveFiles.Insert(SaveFiles.Count-1, file);
            }
        }
    }
}
