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

        private Random seed = new Random();

        public AttackResult Cast(Character From, Character To, out int damage, out string bodyPart)
        {
            damage = 0;
            bodyPart = "";
            if (To.IsAttackEvaded())
                return (AttackResult.Evaded);
            if (To.IsAttackParried())
                return (AttackResult.Parried);
            damage = (int)Math.Round((double)seed.Next(From.CurrentRightMinAttack, From.CurrentRightMaxAttack + 1) * (Damage ?? 1));
            To.Defend(ref damage, From.RightHand, out bodyPart);
            if (Effects != null)
            {
                foreach (var effect in Effects)
                    (effect.Clone() as Status).Apply(To);
            }
            From.LastOpening.Skill = this;
            return (AttackResult.Hit);
        }
    }
}
