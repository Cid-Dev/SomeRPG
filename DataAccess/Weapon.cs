using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace DataAccess
{
    public class Weapon : DB
    {
        public List<object> GetWeaponById(int id)
        {
            List<object> Weapon = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS name, i.description AS description, w.id AS id, w.mindamagebonus AS mindamagebonus, w.maxdamagebonus AS maxdamagebonus, w.isTwoHand as isTwoHand, t.name AS TypeName FROM " + WeaponTable + " w "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON w.item_id=i.id "
                                        + "INNER JOIN " + WeaponTypeTable + " t "
                                        + "ON w.type_id=t.id "
                                        + "WHERE w.id = @id "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Weapon.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["name"],
                            Description = reader["description"],
                            MinDamageBonus = reader["mindamagebonus"],
                            MaxDamageBonus = reader["maxdamagebonus"],
                            isTwoHand = reader["isTwoHand"],
                            TypeName = reader["TypeName"]
                        });
                    }
                }
            }
            return (Weapon);
        }

        public List<object> GetWeaponByName(string name)
        {
            List<object> Weapon = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS name, i.description AS description, w.id AS id, w.mindamagebonus AS mindamagebonus, w.maxdamagebonus AS maxdamagebonus, w.isTwoHand as isTwoHand, t.name AS TypeName FROM " + WeaponTable + " w "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON w.item_id=i.id "
                                        + "INNER JOIN " + WeaponTypeTable + " t "
                                        + "ON w.type_id=t.id "
                                        + "WHERE i.name = @name "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Weapon.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["name"],
                            Description = reader["description"],
                            MinDamageBonus = reader["mindamagebonus"],
                            MaxDamageBonus = reader["maxdamagebonus"],
                            isTwoHand = reader["isTwoHand"],
                            TypeName = reader["TypeName"]
                        });
                    }
                }
            }
            return (Weapon);
        }
    }
}
