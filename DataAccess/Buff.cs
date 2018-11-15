using System.Collections.Generic;
using System.Data.SQLite;

namespace DataAccess
{
    public class Buff : DB
    {
        private List<object> Buffs;

        private string BuildQuery(string fieldName, string parameterName)
        {
            string query = "SELECT * "
                         + "FROM " + BuffTable + " b "
                         + "WHERE b." + fieldName + " = " + parameterName + " "
                         + "LIMIT 1";
            return (query);
        }

        private void BuildBuff(SQLiteDataReader buff)
        {
            Buffs.Add(new
            {
                Id = buff["id"],
                Name = buff["Name"],
                Duration = buff["Duration"],
                IsGood = buff["IsGood"],
                HPModifier = buff["HPModifier"],
                StrenghModifier = buff["StrenghModifier"],
                DexterityModifier = buff["DexterityModifier"],
                VitalityModifier = buff["VitalityModifier"],
                AgilityModifier = buff["AgilityModifier"],
                PrecisionModifier = buff["PrecisionModifier"]
            });
        }

        public List<object> GetBuff(int id)
        {
            Buffs = new List<object>();
            string query = BuildQuery("id", "@id");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };

            GetDatas(query, parameters, BuildBuff);

            return (Buffs);
        }

        public List<object> GetBuff(string name)
        {
            Buffs = new List<object>();
            string query = BuildQuery("name", "@name");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };

            GetDatas(query, parameters, BuildBuff);

            return (Buffs);
        }
    }
}
