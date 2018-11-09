using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public abstract class Status : IApplicable, ICloneable
    {
        public abstract void Apply(Character target);
        public abstract void RemoveEffect(Character target);
        public object Clone()
        {
            return (MemberwiseClone());
        }
    }
}
