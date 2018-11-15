using System.Collections.Generic;
using System.Data.SQLite;

namespace DataAccess
{
    public class Weapon : DB
    {
        private List<object> Weapons;

        private string BuildQuery(string fieldName, string parameterName)
        {
            string query = "SELECT i.name AS name, "
                               + "i.description AS description,"
                               + " w.id AS id, w.mindamagebonus AS mindamagebonus, "
                               + "w.maxdamagebonus AS maxdamagebonus, "
                               + "w.isTwoHand as isTwoHand, "
                               + "t.name AS TypeName, "
                               + "w.range AS Range, "
                               + "c.Name AS WeaponClass "
                          + "FROM " + WeaponTable + " w "
                          + "INNER JOIN " + ItemTable + " i "
                          + "ON w.item_id=i.id "
                          + "INNER JOIN " + WeaponTypeTable + " t "
                          + "ON w.type_id=t.id "
                          + "INNER JOIN " + WeaponClassTable + " c "
                          + "ON w.class_id=c.id "
                          + "WHERE " + fieldName + " = " + parameterName + " "
                          + "LIMIT 1";
            return (query);
        }

        private void BuildWeapon(SQLiteDataReader weapon)
        {
            Weapons.Add(new
            {
                Id = weapon["id"],
                Name = weapon["name"],
                Description = weapon["description"],
                MinDamageBonus = weapon["mindamagebonus"],
                MaxDamageBonus = weapon["maxdamagebonus"],
                isTwoHand = weapon["isTwoHand"],
                TypeName = weapon["TypeName"],
                WeaponClass = weapon["WeaponClass"],
                Range = weapon["Range"]
            });
        }
        public List<object> GetWeapon(int id)
        {
            Weapons = new List<object>();
            string query = BuildQuery("w.id", "@id");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };

            GetDatas(query, parameters, BuildWeapon);

            return (Weapons);
        }

        public List<object> GetWeapon(string name)
        {
            Weapons = new List<object>();
            string query = BuildQuery("i.name", "@name");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };

            GetDatas(query, parameters, BuildWeapon);

            return (Weapons);
        }
    }
}
