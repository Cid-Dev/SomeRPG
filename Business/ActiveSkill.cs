using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ActiveSkill : Skill, ICastable
    {
        public int Cost { get; set; }
        public double? Damage { get; set; }
        public List<Status> Effects { get; set; }
        public Opening Opening { get; set; }
        public SkillRequirement Required { get; set; }

        public bool Cast(Character From, Character To)
        {

            return (true);
        }
    }
}
