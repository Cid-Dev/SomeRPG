using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public abstract class Armor : Item, IEquipable
    {
        public ArmorType ArmorType { get; set; }
        public int Defense { get; set; }
        public abstract void TakeOn(Character target);
        public abstract void TakeOff(Character target);
    }
}
