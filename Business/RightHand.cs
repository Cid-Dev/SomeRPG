using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class RightHand : Item, IEquipable
    {
        public int Id { get; set; }
        public int MinDamageBonus { get; set; }
        public int MaxDamageBonus { get; set; }

        public RightHand(int id)
        {
            try
            {
                DataAccess.RightHand DalRightHand = new DataAccess.RightHand();
                var temp = DalRightHand.GetRightHandById(id);
                if (temp != null)
                {
                    foreach (var dalRightHand in temp)
                    {
                        Id = int.Parse(dalRightHand?.GetType().GetProperty("Id")?.GetValue(dalRightHand, null).ToString());
                        Name = dalRightHand?.GetType().GetProperty("Name")?.GetValue(dalRightHand, null).ToString();
                        Description = dalRightHand?.GetType().GetProperty("Description")?.GetValue(dalRightHand, null).ToString();
                        MinDamageBonus = int.Parse(dalRightHand?.GetType().GetProperty("MinDamageBonus")?.GetValue(dalRightHand, null).ToString());
                        MaxDamageBonus = int.Parse(dalRightHand?.GetType().GetProperty("MaxDamageBonus")?.GetValue(dalRightHand, null).ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public RightHand(string name)
        {
            try
            {
                DataAccess.RightHand DalRightHand = new DataAccess.RightHand();
                var temp = DalRightHand.GetRightHandByName(name);
                if (temp != null)
                {
                    foreach (var dalRightHand in temp)
                    {
                        Id = int.Parse(dalRightHand?.GetType().GetProperty("Id")?.GetValue(dalRightHand, null).ToString());
                        Name = dalRightHand?.GetType().GetProperty("Name")?.GetValue(dalRightHand, null).ToString();
                        Description = dalRightHand?.GetType().GetProperty("Description")?.GetValue(dalRightHand, null).ToString();
                        MinDamageBonus = int.Parse(dalRightHand?.GetType().GetProperty("MinDamageBonus")?.GetValue(dalRightHand, null).ToString());
                        MaxDamageBonus = int.Parse(dalRightHand?.GetType().GetProperty("MaxDamageBonus")?.GetValue(dalRightHand, null).ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void TakeOff(Character target)
        {
            if (target.RightHand != null)
            {
                if (target is Player)
                {
                    var player = target as Player;
                        player.Inventory.Add(player.RightHand);
                }
                target.RightHand = null;
            }
        }

        public void TakeOn(Character target)
        {
            TakeOff(target);
            if (target is Player)
            {
                var player = target as Player;
                    player.Inventory.Remove(this);
            }
            target.RightHand = this;
        }
    }
}
