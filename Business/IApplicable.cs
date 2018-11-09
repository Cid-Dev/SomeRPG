using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IApplicable
    {
        void Apply(Character target);
        void RemoveEffect(Character target);
    }
}
