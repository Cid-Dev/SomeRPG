using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    [JsonConverter(typeof(StatusConverter))]
    public abstract class StatusSave
    {
        public string StatusType { get; set; }
    }
}
