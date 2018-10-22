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
        public uint Money { get; set; }

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
                Inventory.Add(NewItem);
        }

        public void AddItem(Item item)
        {
            if (item is IStackable)
                StackItem(item);
            else
                Inventory.Add(item);
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

        public override string Stats()
        {
            string result = "=== Name : " + Name + " === HP : " + CurrentHP + "/" + BaseHP + " === Damages : " + CurrentMinAttack + " - " + CurrentMaxAttack + " === Level : " + Level + " === Exp : " + _currentExp + "/" + getRequiredExp + " ===\n";

            return (result);
        }
    }
}
