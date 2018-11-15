using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;

namespace DataAccess
{
    public abstract class DB
    {
        protected string ItemTable = "item";
        protected string MonsterTable = "monster";
        protected string HPPotionTable = "hppotion";
        protected string WeaponTable = "weapon";
        protected string WeaponTypeTable = "weapon_type";
        protected string ChestArmorTable = "chestarmor";
        protected string LegsArmorTable = "legsarmor";
        protected string SleevesArmorTable = "sleevesarmor";
        protected string FeetArmorTable = "feetarmor";
        protected string HandsArmorTable = "handsarmor";
        protected string HeadArmorTable = "headarmor";
        protected string ArmorTypeTable = "armortype";
        protected string BuffTable = "buff";
        protected string WeaponClassTable = "WeaponClass";
        protected string StatusEffectPotionTable = "statuseffectpotion";
        protected string loot_table_monster_hppotion = "loot_table_monster_hppotion";
        protected string loot_table_monster_weapon = "loot_table_monster_weapon";
        protected string loot_table_monster_chestarmor = "loot_table_monster_chestarmor";
        protected string loot_table_monster_legsarmor = "loot_table_monster_legsarmor";
        protected string loot_table_monster_sleevesarmor = "loot_table_monster_sleevesarmor";
        protected string loot_table_monster_feetarmor = "loot_table_monster_feetarmor";
        protected string loot_table_monster_handsarmor = "loot_table_monster_handsarmor";
        protected string loot_table_monster_headarmor = "loot_table_monster_headarmor";
        protected string loot_table_monster_statuseffectpotions = "loot_table_monster_statuseffectpotions";

        protected string ConnectionString = null;
        protected SQLiteConnection m_dbConnection = null;

        public DB()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
            }
            catch { }
        }

        protected void GetDatas(string query, Dictionary<string, object> parameters, Action<SQLiteDataReader> callback)
        {
            using (m_dbConnection = new SQLiteConnection(ConnectionString))
            {
                m_dbConnection.Open();
                using (SQLiteCommand command = m_dbConnection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                        foreach (var parameter in parameters)
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                        callback(reader);
                }
            }
        }
    }
}
