using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ChestArmor : Armor
    {
        public int Id { get; set; }

        public ChestArmor(int id)
        {
            try
            {
                DataAccess.ChestArmor DalChestArmor = new DataAccess.ChestArmor();
                var temp = DalChestArmor.GetChestArmorById(id);
                if (temp != null)
                {
                    foreach (var dalChestArmor in temp)
                    {
                        Id = int.Parse(dalChestArmor?.GetType().GetProperty("Id")?.GetValue(dalChestArmor, null).ToString());
                        Name = dalChestArmor?.GetType().GetProperty("Name")?.GetValue(dalChestArmor, null).ToString();
                        Description = dalChestArmor?.GetType().GetProperty("Description")?.GetValue(dalChestArmor, null).ToString();
                        Defense = int.Parse(dalChestArmor?.GetType().GetProperty("Defense")?.GetValue(dalChestArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalChestArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalChestArmor, null).ToString(),
                            Absorbency = double.Parse(dalChestArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalChestArmor, null).ToString())
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public ChestArmor(string name)
        {
            try
            {
                DataAccess.ChestArmor DalChestArmor = new DataAccess.ChestArmor();
                var temp = DalChestArmor.GetChestArmorByName(name);
                if (temp != null)
                {
                    foreach (var dalChestArmor in temp)
                    {
                        Id = int.Parse(dalChestArmor?.GetType().GetProperty("Id")?.GetValue(dalChestArmor, null).ToString());
                        Name = dalChestArmor?.GetType().GetProperty("Name")?.GetValue(dalChestArmor, null).ToString();
                        Description = dalChestArmor?.GetType().GetProperty("Description")?.GetValue(dalChestArmor, null).ToString();
                        Defense = int.Parse(dalChestArmor?.GetType().GetProperty("Defense")?.GetValue(dalChestArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalChestArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalChestArmor, null).ToString(),
                            Absorbency = double.Parse(dalChestArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalChestArmor, null).ToString())
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
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
                    player.Inventory.Add(player.ChestArmor);
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
