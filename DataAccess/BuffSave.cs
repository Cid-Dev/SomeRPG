using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BuffSave : StatusSave
    {
        public int Id { get; set; }
        public int RemainingDuration { get; set; }

        public BuffSave()
        {
            StatusType = "Buff";
        }
    }
}
