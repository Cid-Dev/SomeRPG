using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SkillLoader
    {
        public Skills Skills;// = new Skills();
        private readonly string FilePath;
        private readonly string SkillFile;

        public SkillLoader()
        {
            FilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");
            FilePath = Path.Combine(FilePath, "Datas");
            SkillFile = Path.Combine(FilePath, "skills.json");
        }

        public void Load()
        {

            if (Directory.Exists(FilePath)
                && File.Exists(SkillFile))
            {
                Skills = JsonConvert.DeserializeObject<Skills>(File.ReadAllText(SkillFile));
            }
        }
    }
}
