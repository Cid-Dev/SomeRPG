using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LegsArmor : Armor
    {
        public List<object> GetLegsArmor(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            GetArmors("a.id", "@id", LegsArmorTable, parameters);

            return (Armors);
        }

        public List<object> GetLegsArmor(string name)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };
            GetArmors("i.name", "@name", LegsArmorTable, parameters);

            return (Armors);
        }
    }
}
