using System.Collections.Generic;

namespace DataAccess
{
    public class HeadArmor : Armor
    {
        public List<object> GetHeadArmor(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            GetArmors("a.id", "@id", HeadArmorTable, parameters);

            return (Armors);
        }

        public List<object> GetHeadArmor(string name)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };
            GetArmors("i.name", "@name", HeadArmorTable, parameters);

            return (Armors);
        }
    }
}
