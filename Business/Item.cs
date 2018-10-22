using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Item : ICloneable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public object Clone()
        {
            return (this.MemberwiseClone());
        }
    }
}
