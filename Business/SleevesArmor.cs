using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class SleevesArmor : Armor
    {
        public int Id { get; set; }

        public SleevesArmor(int id)
        {
            try
            {
                DataAccess.SleevesArmor DalSleevesArmor = new DataAccess.SleevesArmor();
                var temp = DalSleevesArmor.GetSleevesArmorById(id);
                if (temp != null)
                {
                    foreach (var dalSleevesArmor in temp)
                    {
                        Id = int.Parse(dalSleevesArmor?.GetType().GetProperty("Id")?.GetValue(dalSleevesArmor, null).ToString());
                        Name = dalSleevesArmor?.GetType().GetProperty("Name")?.GetValue(dalSleevesArmor, null).ToString();
                        Description = dalSleevesArmor?.GetType().GetProperty("Description")?.GetValue(dalSleevesArmor, null).ToString();
                        Defense = int.Parse(dalSleevesArmor?.GetType().GetProperty("Defense")?.GetValue(dalSleevesArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalSleevesArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalSleevesArmor, null).ToString(),
                            Absorbency = double.Parse(dalSleevesArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalSleevesArmor, null).ToString())
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

        public SleevesArmor(string name)
        {
            try
            {
                DataAccess.SleevesArmor DalSleevesArmor = new DataAccess.SleevesArmor();
                var temp = DalSleevesArmor.GetSleevesArmorByName(name);
                if (temp != null)
                {
                    foreach (var dalSleevesArmor in temp)
                    {
                        Id = int.Parse(dalSleevesArmor?.GetType().GetProperty("Id")?.GetValue(dalSleevesArmor, null).ToString());
                        Name = dalSleevesArmor?.GetType().GetProperty("Name")?.GetValue(dalSleevesArmor, null).ToString();
                        Description = dalSleevesArmor?.GetType().GetProperty("Description")?.GetValue(dalSleevesArmor, null).ToString();
                        Defense = int.Parse(dalSleevesArmor?.GetType().GetProperty("Defense")?.GetValue(dalSleevesArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalSleevesArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalSleevesArmor, null).ToString(),
                            Absorbency = double.Parse(dalSleevesArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalSleevesArmor, null).ToString())
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
