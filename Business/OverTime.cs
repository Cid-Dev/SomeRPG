namespace Business
{
    public abstract class OverTime : Status
    {
        public int Frequency { get; set; }
        public int TimeBeforeNextTick { get; set; }
        public int Quantity { get; set; }
        public int RemainingQuantity { get; set; }

        public abstract void ApplyTick(Character target);
    }
}
