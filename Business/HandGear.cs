using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public abstract class HandGear : Item, IEquipable
    {
        public int Id { get; set; }
        public abstract void TakeOn(Character target);
        public abstract void TakeOff(Character target);
    }
}
