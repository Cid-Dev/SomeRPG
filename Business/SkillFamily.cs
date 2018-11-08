using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class SkillFamily
    {
        public string Name { get; set; }
        public List<ActiveSkill> Actives { get; set; }
    }
}
