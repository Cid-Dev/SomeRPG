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
            int TargetHP = Target.Defend(damage);
            report = Name + " attacked " + Target.Name + " and dealt " + damage + " damage.\n";
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
                        Name = hppotion.Name,
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
                        Name = rightHand.Name
                    });
                }
            }
            return (RightHands);
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
                    RightHand = ((RightHand != null) ? (RightHand.Name) : ("")),
                    Inventory = new InventorySave
                    {
                        HPPotions = getHPPotions(),
                        RightHands = getRightHands()
                    }
                });
                /*
                var data = new
                {
                    Name,
                    Level,
                    CurrentExp = _currentExp,
                    Money,
                    RightHand = RightHand.Name,
                    Inventory = new
                    {
                        HPPotions = getAnonHPPotions(),
                        RightHands = getAnonRightHands()
                    }
                };
                */
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            return ("");
        }
    }
}
