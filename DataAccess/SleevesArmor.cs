using System.Collections.Generic;

namespace DataAccess
{
    public class SleevesArmor : Armor
    {
        public List<object> GetSleevesArmor(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            GetArmors("a.id", "@id", SleevesArmorTable, parameters);

            return (Armors);
        }

        public List<object> GetSleevesArmor(string name)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };
            GetArmors("i.name", "@name", SleevesArmorTable, parameters);

            return (Armors);
        }
    }
}
