namespace DataAccess
{
    public class DotSave : OverTimeSave
    {
        public string Type { get; set; }
        public int Damage { get; set; }

        public DotSave()
        {
            StatusType = "Dot";
        }
    }
}
