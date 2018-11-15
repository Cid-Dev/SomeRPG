using System;

namespace Business
{
    public class Item : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public object Clone()
        {
            return (MemberwiseClone());
        }
    }
}
