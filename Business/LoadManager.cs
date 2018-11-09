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

        private void applyStatus(Player player, List<StatusSave> Buff)
        {
            foreach (var status in Buff)
            {
                switch (status.StatusType)
                {
                    case "Buff":
                        var buff = (status as BuffSave);
                        Buff _buff = new Buff(buff.Id);
                        _buff.Apply(player);
                        _buff.RemainingDuration = buff.RemainingDuration;
                        break;

                    case "Dot":
                        var dot = (status as DotSave);
                        Dot _dot = new Dot()
                        {
                            Damage = dot.Damage,
                            Frequency = dot.Frequency,
                            Quantity = dot.Quantity,
                            Type = dot.Type
                        };
                        _dot.Apply(player);
                        _dot.RemainingQuantity = dot.RemainingQuantity;
                        _dot.TimeBeforeNextTick = dot.TimeBeforeNextTick;
                        break;
                }
            }
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
                    BaseRightMinAttack = 5,
                    BaseRightMaxAttack = 10,
                    BaseLeftMinAttack = 0,
                    BaseLeftMaxAttack = 0,
                    BaseStrengh = 10,
                    BaseVitality = 10,
                    BaseAgility = 10,
                    BasePrecision = 10,
                    BaseDexterity = 10,
                    BaseHP = 42,
                    Money = SavedGames[index].Money,
                    RightHand = ((SavedGames[index].RightHand != 0) ? (new Weapon(SavedGames[index].RightHand)) : (null)),
                    LeftHand = ((SavedGames[index].LeftHand != 0) ? (new Weapon(SavedGames[index].LeftHand)) : (null)),
                    ChestArmor = ((SavedGames[index].ChestArmor != 0) ? (new ChestArmor(SavedGames[index].ChestArmor)) : (null)),
                    LegsArmor = ((SavedGames[index].LegsArmor != 0) ? (new LegsArmor(SavedGames[index].LegsArmor)) : (null)),
                    SleevesArmor = ((SavedGames[index].SleevesArmor != 0) ? (new SleevesArmor(SavedGames[index].SleevesArmor)) : (null)),
                    FeetArmor = ((SavedGames[index].FeetArmor != 0) ? (new FeetArmor(SavedGames[index].FeetArmor)) : (null)),
                    HandsArmor = ((SavedGames[index].HandsArmor != 0) ? (new HandsArmor(SavedGames[index].HandsArmor)) : (null)),
                    HeadArmor = ((SavedGames[index].HeadArmor != 0) ? (new HeadArmor(SavedGames[index].HeadArmor)) : (null)),
                    Buffs = new List<Status>(),
                    DeBuffs = new List<Status>(),
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

                applyStatus(player, SavedGames[index].Buffs);
                applyStatus(player, SavedGames[index].DeBuffs);

                foreach (var item in SavedGames[index].Inventory.Weapons)
                    player.Inventory.Add(new Weapon(item.Id));
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
                foreach (var item in SavedGames[index].Inventory.StatusEffectPotions)
                    player.Inventory.Add(new StatusEffectPotion(item.Id) { Quantity = item.Quantity });
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
