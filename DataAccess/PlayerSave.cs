using System.Collections.Generic;

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
        public int LeftHand { get; set; }
        public int ChestArmor { get; set; }
        public int LegsArmor { get; set; }
        public int SleevesArmor { get; set; }
        public int FeetArmor { get; set; }
        public int HandsArmor { get; set; }
        public int HeadArmor { get; set; }
        public List<StatusSave> Buffs = new List<StatusSave>();
        public List<StatusSave> DeBuffs = new List<StatusSave>();
        public InventorySave Inventory = new InventorySave();
    }
}
