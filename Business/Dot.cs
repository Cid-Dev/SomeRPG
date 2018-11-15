namespace Business
{
    public class Dot : OverTime
    {
        public string Type { get; set; } //Enum? Bleed, Poison, Burning, Acid ...
        public int Damage { get; set; }

        public override void Apply(Character target)
        {
            ApplyTo<Dot>(target.DeBuffs);
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
