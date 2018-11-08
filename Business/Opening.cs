using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class Opening
    {
        public ActiveSkill Skill { get; set; }

        public Opening(DataAccess.Opening opening, List<ActiveSkill> skills)
        {
            Skill = skills.SingleOrDefault(s => s.Id == opening.Skill);
        }
    }
}
