using System.Collections.Generic;

namespace DataAccess
{
    public class ActiveSkill
    {
        public List<Skill> Skills { get; set; }
        public SkillRequirement Required { get; set; }
    }
}
