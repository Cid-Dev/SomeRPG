using System;

namespace Business
{
    public class LegsArmor : Armor
    {
        public LegsArmor(int id)
        {
            try
            {
                DataAccess.LegsArmor DalLegsArmor = new DataAccess.LegsArmor();
                BuildArmor(DalLegsArmor.GetLegsArmor(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LegsArmor(string name)
        {
            try
            {
                DataAccess.LegsArmor DalLegsArmor = new DataAccess.LegsArmor();
                BuildArmor(DalLegsArmor.GetLegsArmor(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void TakeOff(Character target)
        {
            if (target.LegsArmor != null)
            {
                if (target is Player)
                {
                    var player = target as Player;
                    player.Inventory.Add(player.LegsArmor);
                }
                target.LegsArmor = null;
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
            target.LegsArmor = this;
        }
    }
}
