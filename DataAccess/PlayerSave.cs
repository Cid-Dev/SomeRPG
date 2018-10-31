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
        public int LegsArmor { get; set; }
        public int SleevesArmor { get; set; }
        public int FeetArmor { get; set; }
        public int HandsArmor { get; set; }
        public int HeadArmor { get; set; }
        public List<BuffSave> Buffs = new List<BuffSave>();
        public List<BuffSave> DeBuffs = new List<BuffSave>();
        public InventorySave Inventory = new InventorySave();
    }
}
