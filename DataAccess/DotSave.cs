using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DotSave : OverTimeSave
    {
        public string Type { get; set; }
        public int Damage { get; set; }

        public DotSave()
        {
            StatusType = "Dot";
        }
    }
}
