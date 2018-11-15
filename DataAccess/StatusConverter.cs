using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DataAccess
{
    /// <summary>
    /// Used to deserialize to the status abstract class
    /// </summary>
    public class StatusConverter : JsonConverter
    {
        private static readonly JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings
        {
            ContractResolver = new StatusSpecifiedConcreteClassConverter()
        };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(StatusSave));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject json = JObject.Load(reader);
            switch (json["StatusType"].Value<string>())
            {
                case "Buff":
                    return JsonConvert.DeserializeObject<BuffSave>(json.ToString(), SpecifiedSubclassConversion);

                case "Dot":
                    return JsonConvert.DeserializeObject<DotSave>(json.ToString(), SpecifiedSubclassConversion);

                default:
                    throw new Exception();
            }

            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
