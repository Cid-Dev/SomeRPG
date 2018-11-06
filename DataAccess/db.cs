using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DataAccess
{
    public class DB
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

        protected void Open()
        {
            m_dbConnection = new SQLiteConnection(ConnectionString);
            m_dbConnection.Open();
        }

        protected void Close()
        {
            m_dbConnection.Close();

            m_dbConnection = null;
        }
    }
}
