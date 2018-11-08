using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ActiveSkill
    {
        public List<Skill> Skills { get; set; }
        public SkillRequirement Required { get; set; }
    }
}
