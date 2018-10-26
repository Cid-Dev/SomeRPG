﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SleevesArmor : DB
    {
        public List<object> GetSleevesArmorById(int id)
        {
            List<object> SleevesArmor = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT a.id AS id, a.defense AS defense, i.name AS name, i.description AS description, t.name AS type_name, t.absorbency as absorbency FROM " + SleevesArmorTable + " a "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON a.item_id=i.id "
                                        + "INNER JOIN " + ArmorTypeTable + " t "
                                        + "ON a.armortype_id=t.id "
                                        + "WHERE a.id = @id "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        SleevesArmor.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["name"],
                            Description = reader["description"],
                            ArmorTypeName = reader["type_name"],
                            Absorbency = reader["absorbency"],
                            Defense = reader["defense"]
                        });
                    }
                }
            }
            return (SleevesArmor);
        }

        public List<object> GetSleevesArmorByName(string name)
        {
            List<object> SleevesArmor = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT a.id AS id, a.defense AS defense, i.name AS name, i.description AS description, t.name AS type_name, t.absorbency as absorbency FROM " + SleevesArmorTable + " a "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON a.item_id=i.id "
                                        + "INNER JOIN " + ArmorTypeTable + " t "
                                        + "ON a.armortype_id=t.id "
                                        + "WHERE i.name = @name "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@name", name);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        SleevesArmor.Add(new
                        {
                            Id = reader["id"],
                            Name = reader["name"],
                            Description = reader["description"],
                            ArmorTypeName = reader["type_name"],
                            Absorbency = reader["absorbency"],
                            Defense = reader["defense"]
                        });
                    }
                }
            }
            return (SleevesArmor);
        }
    }
}
