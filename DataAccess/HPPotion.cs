using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Diagnostics;

namespace DataAccess
{
    public class HPPotion : DB
    {
        public List<object> GetHPPotionById(int id)
        {
            List<object> HPPotion = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS name, i.description AS description, h.id AS id, h.amount AS amount, h.max_amount AS max_amount FROM " + HPPotionTable + " h "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON h.item_id=i.id "
                                        + "WHERE h.id = @id "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        HPPotion.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["name"],
                            Description = reader["description"],
                            Amount = reader["amount"],
                            MaxAmount = reader["max_amount"]
                        });
                    }
                }
            }
            return (HPPotion);
        }

        public List<object> GetHPPotionByName(string name)
        {
            List<object> HPPotion = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS name, i.description AS description, h.id AS id, h.amount AS amount, h.max_amount AS max_amount FROM " + HPPotionTable + " h "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON h.item_id=i.id "
                                        + "WHERE i.name = @name "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        HPPotion.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["name"],
                            Description = reader["description"],
                            Amount = reader["amount"],
                            MaxAmount = reader["max_amount"]
                        });
                    }
                }
            }
            return (HPPotion);
        }
    }
}
