using System;

namespace Business
{
    public class HandsArmor : Armor
    {
        public HandsArmor(int id)
        {
            try
            {
                DataAccess.HandsArmor DalHandsArmor = new DataAccess.HandsArmor();
                BuildArmor(DalHandsArmor.GetHandsArmor(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HandsArmor(string name)
        {
            try
            {
                DataAccess.HandsArmor DalHandsArmor = new DataAccess.HandsArmor();
                BuildArmor(DalHandsArmor.GetHandsArmor(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void TakeOff(Character target)
        {
            if (target.HandsArmor != null)
            {
                if (target is Player)
                {
                    var player = target as Player;
                    player.Inventory.Add(player.HandsArmor);
                }
                target.HandsArmor = null;
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
            target.HandsArmor = this;
        }
    }
}
