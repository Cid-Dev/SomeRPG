using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;

namespace DataAccess
{
    public class StatusEffectPotion : DB
    {
        public List<object> GetStatusEffectPotionById(int id)
        {
            List<object> StatusEffectPotions = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT s.id AS id, "
                                            + "i.name AS Name, "
                                            + "i.description AS Description, "
                                            + "s.max_amount AS MaxAmount, "
                                            + "b.id AS BuffId, "
                                            + "b.Name AS BuffName, "
                                            + "b.Duration AS Duration, "
                                            + "b.IsGood AS IsGood, "
                                            + "b.HPModifier AS HPModifier, "
                                            + "b.StrenghModifier AS StrenghModifier, "
                                            + "b.DexterityModifier AS DexterityModifier, "
                                            + "b.VitalityModifier AS VitalityModifier, "
                                            + "b.AgilityModifier AS AgilityModifier, "
                                            + "b.PrecisionModifier AS PrecisionModifier "
                                        + "FROM " + StatusEffectPotionTable + " s "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON s.item_id=i.id "
                                        + "INNER JOIN " + BuffTable + " b "
                                        + "ON s.buff_id=b.id "
                                        + "WHERE s.id = @id "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        StatusEffectPotions.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["Name"],
                            Description = reader["Description"],
                            MaxAmount = reader["MaxAmount"],
                            Buff = new
                            {
                                Id = reader["BuffId"],
                                Name = reader["BuffName"],
                                Duration = reader["Duration"],
                                IsGood = reader["IsGood"],
                                HPModifier = reader["HPModifier"],
                                StrenghModifier = reader["StrenghModifier"],
                                DexterityModifier = reader["DexterityModifier"],
                                VitalityModifier = reader["VitalityModifier"],
                                AgilityModifier = reader["AgilityModifier"],
                                PrecisionModifier = reader["PrecisionModifier"]
                            }
                        });
                    }
                }
            }
            return (StatusEffectPotions);
        }

        public List<object> GetStatusEffectPotionByName(string name)
        {
            List<object> StatusEffectPotions = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT s.id AS id, "
                                            + "i.name AS Name, "
                                            + "i.description AS Description, "
                                            + "s.max_amount AS MaxAmount, "
                                            + "b.id AS BuffId, "
                                            + "b.Name AS BuffName, "
                                            + "b.Duration AS Duration, "
                                            + "b.IsGood AS IsGood, "
                                            + "b.HPModifier AS HPModifier, "
                                            + "b.StrenghModifier AS StrenghModifier, "
                                            + "b.DexterityModifier AS DexterityModifier, "
                                            + "b.VitalityModifier AS VitalityModifier, "
                                            + "b.AgilityModifier AS AgilityModifier, "
                                            + "b.PrecisionModifier AS PrecisionModifier "
                                        + "FROM " + StatusEffectPotionTable + " s "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON s.item_id=i.id "
                                        + "INNER JOIN " + BuffTable + " b "
                                        + "ON s.buff_id=b.id "
                                        + "WHERE i.name = @name "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        StatusEffectPotions.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["Name"],
                            Description = reader["Description"],
                            MaxAmount = reader["MaxAmount"],
                            Buff = new
                            {
                                Id = reader["BuffId"],
                                Name = reader["BuffName"],
                                Duration = reader["Duration"],
                                IsGood = reader["IsGood"],
                                HPModifier = reader["HPModifier"],
                                StrenghModifier = reader["StrenghModifier"],
                                DexterityModifier = reader["DexterityModifier"],
                                VitalityModifier = reader["VitalityModifier"],
                                AgilityModifier = reader["AgilityModifier"],
                                PrecisionModifier = reader["PrecisionModifier"]
                            }
                        });
                    }
                }
            }
            return (StatusEffectPotions);
        }
    }
}
