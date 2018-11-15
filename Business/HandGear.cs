namespace Business
{
    public abstract class HandGear : Item, IEquipable
    {
        public abstract void TakeOn(Character target);
        public abstract void TakeOff(Character target);
    }
}
