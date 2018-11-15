namespace DataAccess
{
    public class BuffSave : StatusSave
    {
        public int Id { get; set; }
        public int RemainingDuration { get; set; }

        public BuffSave()
        {
            StatusType = "Buff";
        }
    }
}
