using System;

namespace Business
{
    public class FeetArmor : Armor
    {
        public FeetArmor(int id)
        {
            try
            {
                DataAccess.FeetArmor DalFeetArmor = new DataAccess.FeetArmor();
                BuildArmor(DalFeetArmor.GetFeetArmor(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FeetArmor(string name)
        {
            try
            {
                DataAccess.FeetArmor DalFeetArmor = new DataAccess.FeetArmor();
                BuildArmor(DalFeetArmor.GetFeetArmor(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void TakeOff(Character target)
        {
            if (target.FeetArmor != null)
            {
                if (target is Player)
                {
                    var player = target as Player;
                    player.Inventory.Add(player.FeetArmor);
                }
                target.FeetArmor = null;
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
            target.FeetArmor = this;
        }
    }
}
