using System.Collections.Generic;

namespace DataAccess
{
    public class InventorySave
    {
        public List<StackableSave> HPPotions = new List<StackableSave>();
        public List<ItemSave> Weapons = new List<ItemSave>();
        public List<ItemSave> ChestArmors = new List<ItemSave>();
        public List<ItemSave> LegsArmors = new List<ItemSave>();
        public List<ItemSave> SleevesArmors = new List<ItemSave>();
        public List<ItemSave> FeetArmors = new List<ItemSave>();
        public List<ItemSave> HandsArmors = new List<ItemSave>();
        public List<ItemSave> HeadArmors = new List<ItemSave>();
        public List<StackableSave> StatusEffectPotions = new List<StackableSave>();
    }
}
