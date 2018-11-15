using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DataAccess
{
    public class SkillLoader : JsonLoader
    {
        public Skills Skills;

        public SkillLoader() : base(new List<string>
        {
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "Resources",
            "Datas"
        }, "skills.json")
        { }

        public void Load()
        {
            Skills = Load<Skills>();
        }
    }
}
