using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Dot : OverTime
    {
        public string Type { get; set; } //Enum? Bleed, Poison, Burning, Acid ...
        public int Damage { get; set; }
    }
}
