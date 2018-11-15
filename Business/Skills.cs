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
