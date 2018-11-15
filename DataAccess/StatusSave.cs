using Newtonsoft.Json;

namespace DataAccess
{
    [JsonConverter(typeof(StatusConverter))]
    public abstract class StatusSave
    {
        public string StatusType { get; set; }
    }
}
