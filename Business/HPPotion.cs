using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HPPotion : Item, IUsable, IStackable
    {
        /// <summary>
        /// Amount of HP Restored
        /// </summary>
        public int Amount { get; set; }
        public int Quantity { get; set; }
        public int MaxAmount { get; set; }

        public void Use(Character target)
        {
            target.Heal(Amount);
            --Quantity;
        }
    }
}
