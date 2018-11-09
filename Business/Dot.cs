using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Dot : OverTime
    {
        public string Type { get; set; } //Enum? Bleed, Poison, Burning, Acid ...
        public int Damage { get; set; }

        private int IsInList(List<Status> buffs)
        {
            for (int i = 0; i < buffs.Count; ++i)
                if ((buffs[i] is Dot)
                    && Type == (buffs[i] as Dot).Type)
                    return (i);
            return (-1);
        }

        public override void Apply(Character target)
        {
            int buffIndex = IsInList(target.DeBuffs);
            if (buffIndex >= 0)
                target.DeBuffs.RemoveAt(buffIndex);
            target.DeBuffs.Add(this);
        }

        public override void RemoveEffect(Character target)
        {
            target.DeBuffs.Remove(this);
        }

        public override void ApplyTick(Character target)
        {
            target.CurrentHP -= Damage;
            --RemainingQuantity;
            if (RemainingQuantity <= 0)
                RemoveEffect(target);
            else
                TimeBeforeNextTick = Frequency;
        }
    }
}
