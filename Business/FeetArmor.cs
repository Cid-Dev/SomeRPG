using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class FeetArmor : Armor
    {
        public int Id { get; set; }

        public FeetArmor(int id)
        {
            try
            {
                DataAccess.FeetArmor DalFeetArmor = new DataAccess.FeetArmor();
                var temp = DalFeetArmor.GetFeetArmorById(id);
                if (temp != null)
                {
                    foreach (var dalFeetArmor in temp)
                    {
                        Id = int.Parse(dalFeetArmor?.GetType().GetProperty("Id")?.GetValue(dalFeetArmor, null).ToString());
                        Name = dalFeetArmor?.GetType().GetProperty("Name")?.GetValue(dalFeetArmor, null).ToString();
                        Description = dalFeetArmor?.GetType().GetProperty("Description")?.GetValue(dalFeetArmor, null).ToString();
                        Defense = int.Parse(dalFeetArmor?.GetType().GetProperty("Defense")?.GetValue(dalFeetArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalFeetArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalFeetArmor, null).ToString(),
                            Absorbency = double.Parse(dalFeetArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalFeetArmor, null).ToString())
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

        public FeetArmor(string name)
        {
            try
            {
                DataAccess.FeetArmor DalFeetArmor = new DataAccess.FeetArmor();
                var temp = DalFeetArmor.GetFeetArmorByName(name);
                if (temp != null)
                {
                    foreach (var dalFeetArmor in temp)
                    {
                        Id = int.Parse(dalFeetArmor?.GetType().GetProperty("Id")?.GetValue(dalFeetArmor, null).ToString());
                        Name = dalFeetArmor?.GetType().GetProperty("Name")?.GetValue(dalFeetArmor, null).ToString();
                        Description = dalFeetArmor?.GetType().GetProperty("Description")?.GetValue(dalFeetArmor, null).ToString();
                        Defense = int.Parse(dalFeetArmor?.GetType().GetProperty("Defense")?.GetValue(dalFeetArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalFeetArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalFeetArmor, null).ToString(),
                            Absorbency = double.Parse(dalFeetArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalFeetArmor, null).ToString())
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
