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
        //private string TableName = "hppotion";

        public List<object> GetHPPotionByName(string name)
        {
            /*
            string sql = "SELECT * FROM " + HPPotionTable + " h "
                       + "INNER JOIN " + ItemTable + " i "
                       + "ON h.item_id=i.id "
                       + "WHERE i.name = '" + name + "' "
                       + "LIMIT 1";*/
            List<object> HPPotion = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {

                    //command.CommandText = "SELECT * FROM " + HPPotionTable;
                    command.CommandText = "SELECT * FROM " + HPPotionTable + " h "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON h.item_id=i.id "
                                        + "WHERE i.name = @name "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    //Open();
                    //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    //command.Parameters.Add("@name", System.Data.DbType.String);
                    //command.Parameters["@name"].Value = name;
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        HPPotion.Add(new
                        {
                            Name = reader["name"],
                            Description = reader["description"],
                            Amount = reader["amount"],
                            MaxAmount = reader["max_amount"]
                        });
                    }
                    //Close();
                }
            }
            return (HPPotion);
        }
    }
}
