using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace DataAccess
{
    public class RightHand : DB
    {
        public List<object> GetRightHandByName(string name)
        {
            List<object> RightHand = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + RightHandTable + " r "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON r.item_id=i.id "
                                        + "WHERE i.name = @name "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        RightHand.Add(new
                        {
                            Name = reader["name"],
                            Description = reader["description"],
                            MinDamageBonus = reader["mindamagebonus"],
                            MaxDamageBonus = reader["maxdamagebonus"]
                        });
                    }
                }
            }
            return (RightHand);
        }
    }
}
