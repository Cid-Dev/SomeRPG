using System.Collections.Generic;

namespace Business
{
    public abstract class Armor : Item, IEquipable
    {
        public ArmorType ArmorType { get; set; }
        public int Defense { get; set; }
        public abstract void TakeOn(Character target);
        public abstract void TakeOff(Character target);

        protected void BuildArmor(List<object> armors)
        {
            if (armors != null)
            {
                foreach (var armor in armors)
                {
                    Id = int.Parse(armor?.GetType().GetProperty("Id")?.GetValue(armor, null).ToString());
                    Name = armor?.GetType().GetProperty("Name")?.GetValue(armor, null).ToString();
                    Description = armor?.GetType().GetProperty("Description")?.GetValue(armor, null).ToString();
                    Defense = int.Parse(armor?.GetType().GetProperty("Defense")?.GetValue(armor, null).ToString());
                    ArmorType = new ArmorType
                    {
                        Name = armor?.GetType().GetProperty("ArmorTypeName")?.GetValue(armor, null).ToString(),
                        Absorbency = double.Parse(armor?.GetType().GetProperty("Absorbency")?.GetValue(armor, null).ToString())
                    };
                }
            }
        }
    }
}
