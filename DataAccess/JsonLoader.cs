using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public abstract class JsonLoader
    {
        protected string JsonFolder;
        protected string JsonFile;

        public JsonLoader() { }

        public JsonLoader(List<string> pathes, string fileName)
        {
            if (pathes.Count >= 1)
            {
                JsonFolder = pathes[0];
                for (int i = 1; i < pathes.Count; ++i)
                    JsonFolder = Path.Combine(JsonFolder, pathes[i]);
                JsonFile = Path.Combine(JsonFolder, fileName);
            }
        }

        protected T Load<T>() where T: class
        {
            if (Directory.Exists(JsonFolder)
                && File.Exists(JsonFile))
                return (JsonConvert.DeserializeObject<T>(File.ReadAllText(JsonFile)));
            return (null);
        }
    }
}
