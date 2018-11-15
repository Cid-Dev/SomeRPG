using System;
using System.Collections.Generic;

namespace Business
{
    public class HPPotion : Item, IUsable, IStackable
    {
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

        private void BuildPotion(List<object> potions)
        {
            if (potions != null)
            {
                foreach (var dalHPPotion in potions)
                {
                    Id = int.Parse(dalHPPotion?.GetType().GetProperty("Id")?.GetValue(dalHPPotion, null).ToString());
                    Name = dalHPPotion?.GetType().GetProperty("Name")?.GetValue(dalHPPotion, null).ToString();
                    Description = dalHPPotion?.GetType().GetProperty("Description")?.GetValue(dalHPPotion, null).ToString();
                    Amount = int.Parse(dalHPPotion?.GetType().GetProperty("Amount")?.GetValue(dalHPPotion, null).ToString());
                    MaxAmount = int.Parse(dalHPPotion?.GetType().GetProperty("MaxAmount")?.GetValue(dalHPPotion, null).ToString());
                }
            }
        }

        public HPPotion(int id)
        {
            try
            {
                DataAccess.HPPotion DalHPPotion = new DataAccess.HPPotion();
                BuildPotion(DalHPPotion.GetHPPotion(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HPPotion(string name)
        {
            try
            {
                DataAccess.HPPotion DalHPPotion = new DataAccess.HPPotion();
                BuildPotion(DalHPPotion.GetHPPotion(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
