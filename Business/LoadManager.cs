using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class LoadManager
    {
        public List<PlayerSave> SavedGames;

        public LoadManager()
        {
            var gameSave = new GameSave();
            gameSave.Load();
            SavedGames = gameSave.playerSaves;
        }

        public string ShowSavedGames()
        {
            string result = "";
            for (int i = 0; i < SavedGames.Count; ++i)
                result += "[" + (i + 1) + "] : " + SavedGames[i].Name + " - Level " + SavedGames[i].Level + "\n";
            return (result);
        }

        public Player GetPlayer(int index)
        {
            --index;
            if (index >= 0 && index < SavedGames.Count)
            {
                Player player = new Player
                {
                    Name = SavedGames[index].Name,
                    Level = SavedGames[index].Level,
                    _currentExp = SavedGames[index].CurrentExp,
                    BaseCooldown = 10,
                    BaseMinAttack = 5,
                    BaseMaxAttack = 10,
                    Money = SavedGames[index].Money,
                    RightHand = ((SavedGames[index].RightHand != 0) ? (new RightHand(SavedGames[index].RightHand)) : (null)),
                    ChestArmor = ((SavedGames[index].ChestArmor != 0) ? (new ChestArmor(SavedGames[index].ChestArmor)) : (null)),
                    Inventory = new List<Item>()
                };

                player.BaseHP = (int)(42 * ((SavedGames[index].Level > 1) ? ((SavedGames[index].Level - 1) * player.hpMultiplier) : (1)));
                player.CurrentHP = SavedGames[index].CurrentHP;
                foreach (var hPPotion in SavedGames[index].Inventory.HPPotions)
                {
                    player.Inventory.Add(new HPPotion(hPPotion.Id)
                    {
                        Quantity = hPPotion.Quantity
                    });
                }

                foreach (var rightHand in SavedGames[index].Inventory.RightHands)
                    player.Inventory.Add(new RightHand(rightHand.Id));
                return (player);
            }
            return (null);
        }

        public bool CheckName(string Name)
        {
            foreach (var save in SavedGames)
            {
                if (save.Name == Name)
                    return (false);
            }
            return (true);
        }

        public bool CheckResult(int result)
        {
            return (result > 0 && result <= SavedGames.Count);
        }
    }
}
