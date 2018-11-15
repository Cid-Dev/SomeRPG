using System;

namespace Business
{
    public class ChestArmor : Armor
    {
        public ChestArmor(int id)
        {
            try
            {
                DataAccess.ChestArmor DalChestArmor = new DataAccess.ChestArmor();
                BuildArmor(DalChestArmor.GetChestArmor(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChestArmor(string name)
        {
            try
            {
                DataAccess.ChestArmor DalChestArmor = new DataAccess.ChestArmor();
                BuildArmor(DalChestArmor.GetChestArmor(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void TakeOff(Character target)
        {
            if (target.ChestArmor != null)
            {
                if (target is Player)
                {
                    var player = target as Player;
                    player.Inventory.Add(target.ChestArmor);
                }
                target.ChestArmor = null;
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
            target.ChestArmor = this;
        }
    }
}
