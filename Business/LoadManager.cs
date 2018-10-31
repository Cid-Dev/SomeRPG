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
                    _currentExp = SavedGames[index].CurrentExp,
                    BaseCooldown = 10,
                    BaseMinAttack = 5,
                    BaseMaxAttack = 10,
                    BaseStrengh = 10,
                    BaseVitality = 10,
                    BaseAgility = 10,
                    BasePrecision = 10,
                    BaseHP = 42,
                    Money = SavedGames[index].Money,
                    RightHand = ((SavedGames[index].RightHand != 0) ? (new RightHand(SavedGames[index].RightHand)) : (null)),
                    ChestArmor = ((SavedGames[index].ChestArmor != 0) ? (new ChestArmor(SavedGames[index].ChestArmor)) : (null)),
                    LegsArmor = ((SavedGames[index].LegsArmor != 0) ? (new LegsArmor(SavedGames[index].LegsArmor)) : (null)),
                    SleevesArmor = ((SavedGames[index].SleevesArmor != 0) ? (new SleevesArmor(SavedGames[index].SleevesArmor)) : (null)),
                    FeetArmor = ((SavedGames[index].FeetArmor != 0) ? (new FeetArmor(SavedGames[index].FeetArmor)) : (null)),
                    HandsArmor = ((SavedGames[index].HandsArmor != 0) ? (new HandsArmor(SavedGames[index].HandsArmor)) : (null)),
                    HeadArmor = ((SavedGames[index].HeadArmor != 0) ? (new HeadArmor(SavedGames[index].HeadArmor)) : (null)),
                    Inventory = new List<Item>()
                };
                player.SetLevel(SavedGames[index].Level);
                //player.BaseHP = (int)(42 * Math.Pow(1.1, SavedGames[index].Level - 1));
                player.CurrentHP = SavedGames[index].CurrentHP;
                foreach (var hPPotion in SavedGames[index].Inventory.HPPotions)
                {
                    player.Inventory.Add(new HPPotion(hPPotion.Id)
                    {
                        Quantity = hPPotion.Quantity
                    });
                }

                foreach (var item in SavedGames[index].Inventory.RightHands)
                    player.Inventory.Add(new RightHand(item.Id));
                foreach (var item in SavedGames[index].Inventory.ChestArmors)
                    player.Inventory.Add(new ChestArmor(item.Id));
                foreach (var item in SavedGames[index].Inventory.LegsArmors)
                    player.Inventory.Add(new LegsArmor(item.Id));
                foreach (var item in SavedGames[index].Inventory.SleevesArmors)
                    player.Inventory.Add(new SleevesArmor(item.Id));
                foreach (var item in SavedGames[index].Inventory.FeetArmors)
                    player.Inventory.Add(new FeetArmor(item.Id));
                foreach (var item in SavedGames[index].Inventory.HandsArmors)
                    player.Inventory.Add(new HandsArmor(item.Id));
                foreach (var item in SavedGames[index].Inventory.HeadArmors)
                    player.Inventory.Add(new HeadArmor(item.Id));
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
