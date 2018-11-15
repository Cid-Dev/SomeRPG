using System.Collections.Generic;

namespace DataAccess
{
    public class SkillFamily
    {
        public ActiveSkill Active { get; set; }
        public List<Skill> Passive { get; set; }
    }
}
