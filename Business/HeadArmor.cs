using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HeadArmor : Armor
    {
        public int Id { get; set; }

        public HeadArmor(int id)
        {
            try
            {
                DataAccess.HeadArmor DalHeadArmor = new DataAccess.HeadArmor();
                var temp = DalHeadArmor.GetHeadArmorById(id);
                if (temp != null)
                {
                    foreach (var dalHeadArmor in temp)
                    {
                        Id = int.Parse(dalHeadArmor?.GetType().GetProperty("Id")?.GetValue(dalHeadArmor, null).ToString());
                        Name = dalHeadArmor?.GetType().GetProperty("Name")?.GetValue(dalHeadArmor, null).ToString();
                        Description = dalHeadArmor?.GetType().GetProperty("Description")?.GetValue(dalHeadArmor, null).ToString();
                        Defense = int.Parse(dalHeadArmor?.GetType().GetProperty("Defense")?.GetValue(dalHeadArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalHeadArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalHeadArmor, null).ToString(),
                            Absorbency = double.Parse(dalHeadArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalHeadArmor, null).ToString())
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

        public HeadArmor(string name)
        {
            try
            {
                DataAccess.HeadArmor DalHeadArmor = new DataAccess.HeadArmor();
                var temp = DalHeadArmor.GetHeadArmorByName(name);
                if (temp != null)
                {
                    foreach (var dalHeadArmor in temp)
                    {
                        Id = int.Parse(dalHeadArmor?.GetType().GetProperty("Id")?.GetValue(dalHeadArmor, null).ToString());
                        Name = dalHeadArmor?.GetType().GetProperty("Name")?.GetValue(dalHeadArmor, null).ToString();
                        Description = dalHeadArmor?.GetType().GetProperty("Description")?.GetValue(dalHeadArmor, null).ToString();
                        Defense = int.Parse(dalHeadArmor?.GetType().GetProperty("Defense")?.GetValue(dalHeadArmor, null).ToString());
                        ArmorType = new ArmorType
                        {
                            Name = dalHeadArmor?.GetType().GetProperty("ArmorTypeName")?.GetValue(dalHeadArmor, null).ToString(),
                            Absorbency = double.Parse(dalHeadArmor?.GetType().GetProperty("Absorbency")?.GetValue(dalHeadArmor, null).ToString())
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
