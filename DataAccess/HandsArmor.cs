using System.Collections.Generic;

namespace DataAccess
{
    public class HandsArmor : Armor
    {
        public List<object> GetHandsArmor(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            GetArmors("a.id", "@id", HandsArmorTable, parameters);

            return (Armors);
        }

        public List<object> GetHandsArmor(string name)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };
            GetArmors("i.name", "@name", HandsArmorTable, parameters);

            return (Armors);
        }
    }
}
