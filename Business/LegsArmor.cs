using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class LegsArmor : Armor
    {
        public int Id { get; set; }

        public LegsArmor(int id)
        {
            try
            {
                DataAccess.LegsArmor DalLegsArmor = new DataAccess.LegsArmor();
                var temp = DalLegsArmor.GetLegsArmorById(id);
                if (temp != null)
                {
                    foreach (var dalLegsArmor in temp)
                    {
                        Id = int.Parse(dalLegsArmor?.GetType().GetProperty("Id")?.GetValue(dalLegsArmor, null).ToString());
                        Name = dalLegsArmor?.GetType().GetProperty("Name")?.GetValue(dalLegsArmor, null).ToString();
                        Description = dalLegsArmor?.GetType().GetProperty("Description")?.GetValue(dalLegsArmor, null).ToString();
                        Defense = int.Parse(dalLegsArmor?.GetType().GetProperty("Defense")?.GetValue(dalLegsArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalLegsArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalLegsArmor, null).ToString(),
                            Absorbency = double.Parse(dalLegsArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalLegsArmor, null).ToString())
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

        public LegsArmor(string name)
        {
            try
            {
                DataAccess.LegsArmor DalLegsArmor = new DataAccess.LegsArmor();
                var temp = DalLegsArmor.GetLegsArmorByName(name);
                if (temp != null)
                {
                    foreach (var dalLegsArmor in temp)
                    {
                        Id = int.Parse(dalLegsArmor?.GetType().GetProperty("Id")?.GetValue(dalLegsArmor, null).ToString());
                        Name = dalLegsArmor?.GetType().GetProperty("Name")?.GetValue(dalLegsArmor, null).ToString();
                        Description = dalLegsArmor?.GetType().GetProperty("Description")?.GetValue(dalLegsArmor, null).ToString();
                        Defense = int.Parse(dalLegsArmor?.GetType().GetProperty("Defense")?.GetValue(dalLegsArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalLegsArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalLegsArmor, null).ToString(),
                            Absorbency = double.Parse(dalLegsArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalLegsArmor, null).ToString())
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
