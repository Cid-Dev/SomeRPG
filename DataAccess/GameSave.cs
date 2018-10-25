using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GameSave
    {
        string SaveFolder;
        string SaveFile;
        public List<PlayerSave> playerSaves = new List<PlayerSave>();

        public GameSave()
        {
            SaveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games");
            SaveFolder = Path.Combine(SaveFolder, "SomeRPG");
            SaveFile = Path.Combine(SaveFolder, "save.json");
        }

        public void Load()
        {
            
            if (Directory.Exists(SaveFolder)
                && File.Exists(SaveFile))
            {
                playerSaves = JsonConvert.DeserializeObject<List<PlayerSave>>(File.ReadAllText(SaveFile));
            }
        }

        public void Save(PlayerSave playerSave)
        {
            Load();
            var playerFound = false;
            if (playerSaves != null
                && playerSaves.Count > 0)
            {
                for (int i = 0; i < playerSaves.Count; ++i)
                {
                    if (playerSaves[i].Name == playerSave.Name)
                    {
                        playerSaves[i] = playerSave;
                        playerFound = true;
                    }
                }
            }
            else
            {
                var di = Directory.CreateDirectory(SaveFolder);
                playerSaves = new List<PlayerSave>();
            }
            if (!playerFound)
                playerSaves.Add(playerSave);
            using (StreamWriter file = File.CreateText(SaveFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, playerSaves);
            }
        }
    }
}
