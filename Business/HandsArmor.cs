using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HandsArmor : Armor
    {
        public int Id { get; set; }

        public HandsArmor(int id)
        {
            try
            {
                DataAccess.HandsArmor DalHandsArmor = new DataAccess.HandsArmor();
                var temp = DalHandsArmor.GetHandsArmorById(id);
                if (temp != null)
                {
                    foreach (var dalHandsArmor in temp)
                    {
                        Id = int.Parse(dalHandsArmor?.GetType().GetProperty("Id")?.GetValue(dalHandsArmor, null).ToString());
                        Name = dalHandsArmor?.GetType().GetProperty("Name")?.GetValue(dalHandsArmor, null).ToString();
                        Description = dalHandsArmor?.GetType().GetProperty("Description")?.GetValue(dalHandsArmor, null).ToString();
                        Defense = int.Parse(dalHandsArmor?.GetType().GetProperty("Defense")?.GetValue(dalHandsArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalHandsArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalHandsArmor, null).ToString(),
                            Absorbency = double.Parse(dalHandsArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalHandsArmor, null).ToString())
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

        public HandsArmor(string name)
        {
            try
            {
                DataAccess.HandsArmor DalHandsArmor = new DataAccess.HandsArmor();
                var temp = DalHandsArmor.GetHandsArmorByName(name);
                if (temp != null)
                {
                    foreach (var dalHandsArmor in temp)
                    {
                        Id = int.Parse(dalHandsArmor?.GetType().GetProperty("Id")?.GetValue(dalHandsArmor, null).ToString());
                        Name = dalHandsArmor?.GetType().GetProperty("Name")?.GetValue(dalHandsArmor, null).ToString();
                        Description = dalHandsArmor?.GetType().GetProperty("Description")?.GetValue(dalHandsArmor, null).ToString();
                        Defense = int.Parse(dalHandsArmor?.GetType().GetProperty("Defense")?.GetValue(dalHandsArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalHandsArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalHandsArmor, null).ToString(),
                            Absorbency = double.Parse(dalHandsArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalHandsArmor, null).ToString())
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
