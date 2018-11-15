using System.Collections.Generic;
using System.Data.SQLite;

namespace DataAccess
{
    public abstract class Armor : DB
    {
        protected List<object> Armors;

        private string BuildQuery(string fieldName, string parameterName, string TableName)
        {
            string query = "SELECT a.id AS id, "
                             + "a.defense AS defense, "
                             + "i.name AS name, "
                             + "i.description AS description, "
                             + "t.name AS type_name, "
                             + "t.absorbency as absorbency "
                         + "FROM " + TableName + " a "
                         + "INNER JOIN " + ItemTable + " i "
                         + "ON a.item_id=i.id "
                         + "INNER JOIN " + ArmorTypeTable + " t "
                         + "ON a.armortype_id=t.id "
                         + "WHERE " + fieldName + " = " + parameterName + " "
                         + "LIMIT 1";
            return (query);
        }

        private void BuildArmor(SQLiteDataReader armor)
        {
            Armors.Add(new
            {
                Id = armor["id"],
                Name = armor["name"],
                Description = armor["description"],
                ArmorTypeName = armor["type_name"],
                Absorbency = armor["absorbency"],
                Defense = armor["defense"]
            });
        }

        protected void GetArmors(string fieldName, string parameterName, string TableName, Dictionary<string, object> parameters)
        {
            Armors = new List<object>();
            string query = BuildQuery(fieldName, parameterName, TableName);
            GetDatas(query, parameters, BuildArmor);
        }
    }
}
