using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class Opening
    {
        public ActiveSkill Skill { get; set; }

        public Opening() { }

        public Opening(DataAccess.Opening opening, List<ActiveSkill> skills)
        {
            Skill = skills.SingleOrDefault(s => s.Id == opening.Skill);
        }
    }
}
