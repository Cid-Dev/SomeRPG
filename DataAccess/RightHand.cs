﻿using System;
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
        public List<object> GetRightHandById(int id)
        {
            List<object> RightHand = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS name, i.description AS description, r.id AS id, r.mindamagebonus AS mindamagebonus, r.maxdamagebonus AS maxdamagebonus FROM " + RightHandTable + " r "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON r.item_id=i.id "
                                        + "WHERE r.id = @id "
                                        + "LIMIT 1";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        RightHand.Add(new
                        {
                            Id = reader["id"],
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

        public List<object> GetRightHandByName(string name)
        {
            List<object> RightHand = new List<object>();
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS name, i.description AS description, r.id AS id, r.mindamagebonus AS mindamagebonus, r.maxdamagebonus AS maxdamagebonus FROM " + RightHandTable + " r "
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
                            Id = reader["id"],
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
