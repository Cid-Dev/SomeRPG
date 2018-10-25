using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PlayerSave
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int CurrentExp { get; set; }
        public int Money { get; set; }
        public int CurrentHP { get; set; }
        public int RightHand { get; set; }
        public int ChestArmor { get; set; }
        public InventorySave Inventory = new InventorySave();
    }
}
