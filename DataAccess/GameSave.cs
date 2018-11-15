using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class GameSave : JsonLoader
    {
        public List<PlayerSave> playerSaves = new List<PlayerSave>();

        public GameSave() : base(new List<string>
        {
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Saved Games",
            "SomeRPG"
        }, "save.json")
        { }

        public void Load()
        {
            playerSaves = Load<List<PlayerSave>>();
        }

        public void Save(PlayerSave playerSave)
        {
            Load();
            var playerFound = false;
            if (playerSaves != null
                && playerSaves.Count > 0)
            {
                for (int i = 0; i < playerSaves.Count; ++i)
                    if (playerSaves[i].Name == playerSave.Name)
                    {
                        playerSaves[i] = playerSave;
                        playerFound = true;
                    }
            }
            else
            {
                var di = Directory.CreateDirectory(JsonFolder);
                playerSaves = new List<PlayerSave>();
            }
            if (!playerFound)
                playerSaves.Add(playerSave);
            using (StreamWriter file = File.CreateText(JsonFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, playerSaves);
            }
        }
    }
}
