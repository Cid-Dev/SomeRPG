using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class Skills
    {
        DataAccess.Skills skills;

        public Skills()
        {
            var skillLoader = new SkillLoader();
            skillLoader.Load();
            skills = skillLoader.Skills;
        }
    }
}
