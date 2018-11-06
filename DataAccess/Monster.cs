﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Monster : DB
    {
        public List<object> GetWeaponLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_weapon + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + WeaponTable + " w "
                                        + "ON l.weapon_id=w.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON w.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetHPPotionLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_hppotion + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + HPPotionTable + " h "
                                        + "ON l.hppotion_id=h.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON h.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetArmorChestLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_chestarmor + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + ChestArmorTable + " c "
                                        + "ON l.chestarmor_id=c.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON c.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetArmorLegsLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_legsarmor + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + LegsArmorTable + " c "
                                        + "ON l.legsarmor_id=c.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON c.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetArmorSleevesLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_sleevesarmor + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + SleevesArmorTable + " c "
                                        + "ON l.sleevesarmor_id=c.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON c.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetArmorFeetLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_feetarmor + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + FeetArmorTable + " c "
                                        + "ON l.feetarmor_id=c.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON c.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetArmorHandsLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_handsarmor + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + HandsArmorTable + " c "
                                        + "ON l.handsarmor_id=c.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON c.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetArmorHeadLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_headarmor + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + HeadArmorTable + " c "
                                        + "ON l.headarmor_id=c.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON c.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //Debug.WriteLine("Name: " + reader["name"] + "\tDescription: " + reader["description"] + "\tAmount: " + reader["amount"] + "\tMaxAmount: " + reader["max_amount"]);
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetStatusEffectPotionLootTable(int monster_id)
        {
            List<object> result = new List<object>();

            using (var m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT i.name AS Name, "
                                               + "l.min_amount AS MinAmount, "
                                               + "l.max_amount AS MaxAmount, "
                                               + "l.probability AS Probability "
                                        + "FROM " + MonsterTable + " m "
                                        + "INNER JOIN " + loot_table_monster_statuseffectpotions + " l "
                                        + "ON l.monster_id=m.id "
                                        + "INNER JOIN " + StatusEffectPotionTable + " s "
                                        + "ON l.statuseffectpotions_id=s.id "
                                        + "INNER JOIN " + ItemTable + " i "
                                        + "ON s.item_id=i.id "
                                        + "WHERE m.id = @monster_id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@monster_id", monster_id);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            Name = reader["Name"],
                            MinAmount = reader["MinAmount"],
                            MaxAmount = reader["MaxAmount"],
                            Probability = reader["Probability"]
                        });
                    }
                }
            }
            return (result);
        }

        public List<object> GetAll()
        {
            string sql = "select * from " + MonsterTable;
            Open();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<object> Monsters = new List<object>();
            while (reader.Read())
            {
                Monsters.Add(new
                {
                    Name = reader["name"],
                    HP = reader["HP"],
                    BaseRightMinAttack = reader["BaseRightMinAttack"],
                    BaseRightMaxAttack = reader["BaseRightMaxAttack"],
                    BaseLeftMinAttack = reader["BaseLeftMinAttack"],
                    BaseLeftMaxAttack = reader["BaseLeftMaxAttack"],
                    BaseCoolDown = reader["BaseCoolDown"],
                    GivenExp = reader["GivenExp"],
                    BaseExp = reader["BaseExp"],
                    MinMoney = reader["MinMoney"],
                    MaxMoney = reader["MaxMoney"],
                    MoneyMultiplier = reader["MoneyMultiplier"],
                    BaseStrengh = reader["BaseStrengh"],
                    BaseVitality = reader["BaseVitality"],
                    BaseAgility = reader["BaseAgility"],
                    BasePrecision = reader["BasePrecision"],
                    BaseDexterity = reader["BaseDexterity"],
                    LootTable = new
                    {
                        HPPotion = GetHPPotionLootTable(int.Parse(reader["id"].ToString())),
                        Weapon = GetWeaponLootTable(int.Parse(reader["id"].ToString())),
                        ArmorChest = GetArmorChestLootTable(int.Parse(reader["id"].ToString())),
                        ArmorSleeves = GetArmorSleevesLootTable(int.Parse(reader["id"].ToString())),
                        ArmorLegs = GetArmorLegsLootTable(int.Parse(reader["id"].ToString())),
                        ArmorFeet = GetArmorFeetLootTable(int.Parse(reader["id"].ToString())),
                        ArmorHands = GetArmorHandsLootTable(int.Parse(reader["id"].ToString())),
                        ArmorHead = GetArmorHeadLootTable(int.Parse(reader["id"].ToString())),
                        StatusEffectPotion = GetStatusEffectPotionLootTable(int.Parse(reader["id"].ToString()))
                    }
                });
            }
            Close();
            return (Monsters);
        }
    }
}
