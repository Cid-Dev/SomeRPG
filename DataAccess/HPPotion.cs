using System.Collections.Generic;
using System.Data.SQLite;

namespace DataAccess
{
    public class HPPotion : DB
    {
        protected List<object> HPPotions;

        private string BuildQuery(string fieldName, string parameterName)
        {
            string query = "SELECT i.name AS name, "
                             + "i.description AS description, "
                             + "h.id AS id, "
                             + "h.amount AS amount, "
                             + "h.max_amount AS max_amount "
                         + "FROM " + HPPotionTable + " h "
                         + "INNER JOIN " + ItemTable + " i "
                         + "ON h.item_id=i.id "
                         + "WHERE " + fieldName + " = " + parameterName + " "
                         + "LIMIT 1";
            return (query);
        }

        private void BuildHPPotion(SQLiteDataReader hppotion)
        {
            HPPotions.Add(new
            {
                Id = hppotion["id"],
                Name = hppotion["name"],
                Description = hppotion["description"],
                Amount = hppotion["amount"],
                MaxAmount = hppotion["max_amount"]
            });
        }

        public List<object> GetHPPotion(int id)
        {
            HPPotions = new List<object>();
            string query = BuildQuery("h.id", "@id");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };

            GetDatas(query, parameters, BuildHPPotion);

            return (HPPotions);
        }

        public List<object> GetHPPotion(string name)
        {
            HPPotions = new List<object>();
            string query = BuildQuery("i.name", "@name");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };

            GetDatas(query, parameters, BuildHPPotion);

            return (HPPotions);
        }
    }
}
