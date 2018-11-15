using System;

namespace Business
{
    public class HeadArmor : Armor
    {
        public HeadArmor(int id)
        {
            try
            {
                DataAccess.HeadArmor DalHeadArmor = new DataAccess.HeadArmor();
                BuildArmor(DalHeadArmor.GetHeadArmor(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HeadArmor(string name)
        {
            try
            {
                DataAccess.HeadArmor DalHeadArmor = new DataAccess.HeadArmor();
                BuildArmor(DalHeadArmor.GetHeadArmor(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void TakeOff(Character target)
        {
            if (target.HeadArmor != null)
            {
                if (target is Player)
                {
                    var player = target as Player;
                    player.Inventory.Add(player.HeadArmor);
                }
                target.HeadArmor = null;
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
            target.HeadArmor = this;
        }
    }
}
