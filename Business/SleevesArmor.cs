using System;

namespace Business
{
    public class SleevesArmor : Armor
    {
        public SleevesArmor(int id)
        {
            try
            {
                DataAccess.SleevesArmor DalSleevesArmor = new DataAccess.SleevesArmor();
                BuildArmor(DalSleevesArmor.GetSleevesArmor(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SleevesArmor(string name)
        {
            try
            {
                DataAccess.SleevesArmor DalSleevesArmor = new DataAccess.SleevesArmor();
                BuildArmor(DalSleevesArmor.GetSleevesArmor(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void TakeOff(Character target)
        {
            if (target.SleevesArmor != null)
            {
                if (target is Player)
                {
                    var player = target as Player;
                    player.Inventory.Add(player.SleevesArmor);
                }
                target.SleevesArmor = null;
            }
        }

        public override void TakeOn(Character target)
        {
            TakeOff(target);
            if (target is Player)
            {
                var player = target as Player;
                player.Inventory.Remove(this);
            }
            target.SleevesArmor = this;
        }
    }
}
