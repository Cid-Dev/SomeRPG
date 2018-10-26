using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class Player : Character
    {
        public List<Item> Inventory = new List<Item>();
        public int Money { get; set; }

        private void StackItem(Item NewItem)
        {
            foreach (var item in Inventory)
            {
                if (item.Name == NewItem.Name
                    && item is IStackable
                    && (NewItem as IStackable).Quantity > 0)
                {
                    var stackable = item as IStackable;
                    if (stackable.Quantity < stackable.MaxAmount)
                    {
                        if (stackable.Quantity + (NewItem as IStackable).Quantity <= stackable.MaxAmount)
                        {
                            stackable.Quantity += (NewItem as IStackable).Quantity;
                            (NewItem as IStackable).Quantity = 0;
                        }
                        else
                        {
                            (NewItem as IStackable).Quantity -= (stackable.MaxAmount - stackable.Quantity);
                            stackable.Quantity = stackable.MaxAmount;
                        }
                    }
                }
            }
            if ((NewItem as IStackable).Quantity > 0)
                Inventory.Add(NewItem.Clone() as Item);
        }

        public void AddItem(Item item)
        {
            if (item is IStackable)
                StackItem(item);
            else
                Inventory.Add(item.Clone() as Item);
        }

        public override string Attack()
        {
            string report;
            CurrentCooldown = _baseCooldown;
            int damage = seed.Next(CurrentMinAttack, CurrentMaxAttack + 1);
            string bodyPart;
            int TargetHP = Target.Defend(ref damage, out bodyPart);
            report = Name + " attacked " + Target.Name + " on the " + bodyPart + " and dealt " + damage + " damage.\n";
            report += Target.Name + " has " + TargetHP + " HP remaining.\n";
            if (TargetHP <= 0)
            {
                report += "You killed " + Target.Name + " and have earned " + Target.getGivenExp + " exp\n";
                report += setExp(Target.getGivenExp);
            }
            return (report);
        }

        private string ConvertMoney(int money)
        {
            string result = "";

            int cents;

            cents = money % 100;
            if (cents > 0)
                result = " " + cents + " copper";
            money /= 100;
            if (money > 0)
            {
                cents = money % 100;
                if (cents > 0)
                    result = " " + cents + " silver" + result;
                money /= 100;
                if (money > 0)
                {
                    cents = money % 1000;
                    if (cents > 0)
                        result = " " + cents + " gold" + result;
                    money /= 1000;
                    if (money > 0)
                        result = " " + money + " platinium" + result;
                }
            }
            return (result);
        }

        public override string Stats()
        {
            string result = "=== Name : " + Name + " === HP : " + CurrentHP + "/" + BaseHP + " === Damages : " + CurrentMinAttack + " - " + CurrentMaxAttack + " === Level : " + Level + " === Exp : " + _currentExp + "/" + getRequiredExp + " ===\n";
            if (Money > 0)
                result += "=== Money : " + ConvertMoney(Money) + " ===\n";
            return (result);
        }

        private List<dynamic> getAnonHPPotions()
        {
            var HPPotions = new List<dynamic>();
            foreach (var item in Inventory)
            {
                if (item is HPPotion)
                {
                    var hppotion = (item as HPPotion);
                    HPPotions.Add(new
                    {
                        hppotion.Name,
                        hppotion.Quantity
                    });
                }
            }
            return (HPPotions);
        }

        private List<dynamic> getAnonRightHands()
        {
            var RightHands = new List<dynamic>();
            foreach (var item in Inventory)
            {
                if (item is RightHand)
                {
                    var rightHand = (item as RightHand);
                    RightHands.Add(new
                    {
                        rightHand.Name
                    });
                }
            }
            return (RightHands);
        }

        /*
        private List<T1> getItems<T1, T2>() where T1 : class where T2 : class
        {
            var ItemsToGet = new List<T1>();
            foreach (var item in Inventory)
            {
                if (item is T2)
                {
                    var itemToGet = (item as T2);
                    ItemsToGet.Add(new T1
                    {
                        Id = itemToGet.Id,
                        Quantity = itemToGet.Quantity
                    });
                }
            }
            return (ItemsToGet);
        }
        */

        private List<HPPotionsSave> getHPPotions()
        {
            var HPPotions = new List<HPPotionsSave>();
            foreach (var item in Inventory)
            {
                if (item is HPPotion)
                {
                    var hppotion = (item as HPPotion);
                    HPPotions.Add(new HPPotionsSave
                    {
                        Id = hppotion.Id,
                        Quantity = hppotion.Quantity
                    });
                }
            }
            return (HPPotions);
        }

        private List<RightHandsSave> getRightHands()
        {
            var RightHands = new List<RightHandsSave>();
            foreach (var item in Inventory)
            {
                if (item is RightHand)
                {
                    var rightHand = (item as RightHand);
                    RightHands.Add(new RightHandsSave
                    {
                        Id = rightHand.Id
                    });
                }
            }
            return (RightHands);
        }

        private List<ChestArmorSave> getChestArmors()
        {
            var ChestArmors = new List<ChestArmorSave>();
            foreach (var item in Inventory)
            {
                if (item is ChestArmor)
                {
                    var chestArmor = (item as ChestArmor);
                    ChestArmors.Add(new ChestArmorSave
                    {
                        Id = chestArmor.Id
                    });
                }
            }
            return (ChestArmors);
        }

        private List<LegsArmorSave> getLegsArmors()
        {
            var LegsArmors = new List<LegsArmorSave>();
            foreach (var item in Inventory)
            {
                if (item is LegsArmor)
                {
                    var legsArmor = (item as LegsArmor);
                    LegsArmors.Add(new LegsArmorSave
                    {
                        Id = legsArmor.Id
                    });
                }
            }
            return (LegsArmors);
        }

        private List<SleevesArmorSave> getSleevesArmors()
        {
            var SleevesArmor = new List<SleevesArmorSave>();
            foreach (var item in Inventory)
            {
                if (item is SleevesArmor)
                {
                    var sleevesArmor = (item as SleevesArmor);
                    SleevesArmor.Add(new SleevesArmorSave
                    {
                        Id = sleevesArmor.Id
                    });
                }
            }
            return (SleevesArmor);
        }

        private List<FeetArmorSave> getFeetArmors()
        {
            var FeetArmors = new List<FeetArmorSave>();
            foreach (var item in Inventory)
            {
                if (item is FeetArmor)
                {
                    var feetArmor = (item as FeetArmor);
                    FeetArmors.Add(new FeetArmorSave
                    {
                        Id = feetArmor.Id
                    });
                }
            }
            return (FeetArmors);
        }

        private List<HandsArmorSave> getHandsArmors()
        {
            var HandsArmors = new List<HandsArmorSave>();
            foreach (var item in Inventory)
            {
                if (item is HandsArmor)
                {
                    var handsArmor = (item as HandsArmor);
                    HandsArmors.Add(new HandsArmorSave
                    {
                        Id = handsArmor.Id
                    });
                }
            }
            return (HandsArmors);
        }

        private List<HeadArmorSave> getHeadArmors()
        {
            var HeadArmors = new List<HeadArmorSave>();
            foreach (var item in Inventory)
            {
                if (item is HeadArmor)
                {
                    var headArmor = (item as HeadArmor);
                    HeadArmors.Add(new HeadArmorSave
                    {
                        Id = headArmor.Id
                    });
                }
            }
            return (HeadArmors);
        }

        public string Save()
        {
            try
            {
                GameSave gameSave = new GameSave();
                gameSave.Save(new PlayerSave
                {
                    Name = Name,
                    Level = Level,
                    CurrentExp = _currentExp,
                    Money = Money,
                    CurrentHP = CurrentHP,
                    RightHand = ((RightHand != null) ? (RightHand.Id) : (0)),
                    ChestArmor = ((ChestArmor != null) ? (ChestArmor.Id) : (0)),
                    LegsArmor = ((LegsArmor != null) ? (LegsArmor.Id) : (0)),
                    SleevesArmor = ((SleevesArmor != null) ? (SleevesArmor.Id) : (0)),
                    FeetArmor = ((FeetArmor != null) ? (FeetArmor.Id) : (0)),
                    HandsArmor = ((HandsArmor != null) ? (HandsArmor.Id) : (0)),
                    HeadArmor = ((HeadArmor != null) ? (HeadArmor.Id) : (0)),
                    Inventory = new InventorySave
                    {
                        HPPotions = getHPPotions(),
                        RightHands = getRightHands(),
                        ChestArmors = getChestArmors(),
                        SleevesArmors = getSleevesArmors(),
                        LegsArmors = getLegsArmors(),
                        FeetArmors = getFeetArmors(),
                        HandsArmors = getHandsArmors(),
                        HeadArmors = getHeadArmors()
                    }
                });
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            return ("");
        }
    }
}
