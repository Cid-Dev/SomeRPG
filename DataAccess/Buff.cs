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
    public class Buff : DB
    {
        public List<object> GetBuffById(int id)
        {
            List<object> Buff = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + BuffTable + " b "
                                        + "WHERE b.id = @id "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Buff.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["Name"],
                            Duration = reader["Duration"],
                            IsGood = reader["IsGood"],
                            HPModifier = reader["HPModifier"],
                            StrenghModifier = reader["StrenghModifier"],
                            DexterityModifier = reader["DexterityModifier"],
                            VitalityModifier = reader["VitalityModifier"],
                            AgilityModifier = reader["AgilityModifier"],
                            PrecisionModifier = reader["PrecisionModifier"]
                        });
                    }
                }
            }
            return (Buff);
        }

        public List<object> GetBuffByName(string name)
        {
            List<object> Buff = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM " + BuffTable + " b "
                                        + "WHERE b.name = @name "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Buff.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["Name"],
                            Duration = reader["Duration"],
                            IsGood = reader["IsGood"],
                            HPModifier = reader["HPModifier"],
                            StrenghModifier = reader["StrenghModifier"],
                            DexterityModifier = reader["DexterityModifier"],
                            VitalityModifier = reader["VitalityModifier"],
                            AgilityModifier = reader["AgilityModifier"],
                            PrecisionModifier = reader["PrecisionModifier"]
                        });
                    }
                }
            }
            return (Buff);
        }
    }
}
