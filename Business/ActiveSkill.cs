using System;
using System.Collections.Generic;

namespace Business
{
    public class ActiveSkill : Skill, ICastable
    {
        public int Cost { get; set; }
        public int? Range { get; set; }
        public double? Damage { get; set; }
        public List<Status> Effects { get; set; }
        public Opening Opening { get; set; }
        public SkillRequirement Required { get; set; }

        private Random seed = new Random();

        public AttackReport Cast(Character From, Character To)
        {
            AttackReport attackReport = new AttackReport
            {
                SkillName = Name,
                AttackerName = From.Name,
                DefenderName = To.Name
            };

            if (To.IsAttackEvaded())
                attackReport.AttackResult = AttackResult.Evaded;
            if (To.IsAttackParried())
                attackReport.AttackResult = AttackResult.Parried;
            attackReport.AttackResult = AttackResult.Hit;

            attackReport.Damage = (int)Math.Round((double)seed.Next(From.CurrentRightMinAttack, From.CurrentRightMaxAttack + 1) * (Damage ?? 1));

            To.Defend(From.RightHand, attackReport);

            if (From.RightHand != null)
                attackReport.WeaponName = From.RightHand.Name;

            if (Effects != null)
                foreach (var effect in Effects)
                    (effect.Clone() as Status).Apply(To);

            From.LastOpening.Skill = this;

            return (attackReport);
        }
    }
}
