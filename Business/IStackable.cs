using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IStackable
    {
        int Quantity { get; set; }
        int MaxAmount { get; set; }
    }
}
