namespace Business
{
    public interface IApplicable
    {
        void Apply(Character target);
        void RemoveEffect(Character target);
    }
}
