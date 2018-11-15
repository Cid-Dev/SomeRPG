using System;
using System.Collections.Generic;

namespace Business
{
    public abstract class Status : IApplicable, ICloneable
    {
        public string Name { get; set; }
        public abstract void Apply(Character target);
        public abstract void RemoveEffect(Character target);

        public object Clone()
        {
            return (MemberwiseClone());
        }

        private int IsInList<T>(List<Status> buffs)
        {
            for (int i = 0; i < buffs.Count; ++i)
                if ((buffs[i] is T)
                    && Name == (buffs[i]).Name)
                    return (i);
            return (-1);
        }

        protected void ApplyTo<T>(List<Status> statuses)
        {
            int buffIndex = IsInList<T>(statuses);
            if (buffIndex >= 0)
                statuses.RemoveAt(buffIndex);
            statuses.Add(this);
        }
    }
}
