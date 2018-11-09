using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class OverTimeSave : StatusSave
    {
        public int Frequency { get; set; }
        public int TimeBeforeNextTick { get; set; }
        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }
    }
}
