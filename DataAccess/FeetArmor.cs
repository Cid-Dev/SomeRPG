using System.Collections.Generic;

namespace DataAccess
{
    public class FeetArmor : Armor
    {
        public List<object> GetFeetArmor(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            GetArmors("a.id", "@id", FeetArmorTable, parameters);

            return (Armors);
        }

        public List<object> GetFeetArmor(string name)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };
            GetArmors("i.name", "@name", FeetArmorTable, parameters);

            return (Armors);
        }
    }
}
