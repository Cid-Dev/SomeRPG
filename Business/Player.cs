using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Player : Character
    {
        public List<Item> Inventory = new List<Item>();

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
