using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class HPPotion : Item, IUsable, IStackable
    {
        public int Id { get; set; }
        /// <summary>
        /// Amount of HP Restored
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Quantity of current stack
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Max quantity of stack
        /// </summary>
        public int MaxAmount { get; set; }

        public void Use(Character target)
        {
            target.Heal(Amount);
            --Quantity;
        }

        public HPPotion(int id)
        {
            try
            {
                DataAccess.HPPotion DalHPPotion = new DataAccess.HPPotion();
                var temp = DalHPPotion.GetHPPotionById(id);
                if (temp != null)
                {
                    foreach (var dalHPPotion in temp)
                    {
                        Id = int.Parse(dalHPPotion?.GetType().GetProperty("Id")?.GetValue(dalHPPotion, null).ToString());
                        Name = dalHPPotion?.GetType().GetProperty("Name")?.GetValue(dalHPPotion, null).ToString();
                        Description = dalHPPotion?.GetType().GetProperty("Description")?.GetValue(dalHPPotion, null).ToString();
                        Amount = int.Parse(dalHPPotion?.GetType().GetProperty("Amount")?.GetValue(dalHPPotion, null).ToString());
                        MaxAmount = int.Parse(dalHPPotion?.GetType().GetProperty("MaxAmount")?.GetValue(dalHPPotion, null).ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public HPPotion(string name)
        {
            try
            {
                DataAccess.HPPotion DalHPPotion = new DataAccess.HPPotion();
                var temp = DalHPPotion.GetHPPotionByName(name);
                if (temp != null)
                {
                    foreach (var dalHPPotion in temp)
                    {
                        Id = int.Parse(dalHPPotion?.GetType().GetProperty("Id")?.GetValue(dalHPPotion, null).ToString());
                        Name = dalHPPotion?.GetType().GetProperty("Name")?.GetValue(dalHPPotion, null).ToString();
                        Description = dalHPPotion?.GetType().GetProperty("Description")?.GetValue(dalHPPotion, null).ToString();
                        Amount = int.Parse(dalHPPotion?.GetType().GetProperty("Amount")?.GetValue(dalHPPotion, null).ToString());
                        MaxAmount = int.Parse(dalHPPotion?.GetType().GetProperty("MaxAmount")?.GetValue(dalHPPotion, null).ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
