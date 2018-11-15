using System.Collections.Generic;

namespace DataAccess
{
    public class ChestArmor : Armor
    {
        public List<object> GetChestArmor(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            GetArmors("a.id", "@id", ChestArmorTable, parameters);

            return (Armors);
        }

        public List<object> GetChestArmor(string name)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };
            GetArmors("i.name", "@name", ChestArmorTable, parameters);

            return (Armors);
        }
    }
}
