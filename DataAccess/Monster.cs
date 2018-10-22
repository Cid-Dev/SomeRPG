using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Monster : DB
    {
        //private string TableName = "monster";
        //private string TableName = "sqlite_master";

        public List<object> GetAll()
        {
            string sql = "select * from " + MonsterTable;
            Open();
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<object> Monsters = new List<object>();
            while (reader.Read())
            {
                //Debug.WriteLine("Name: " + reader["name"] + "\tHP: " + reader["HP"] + "\tBaseMinAttack: " + reader["BaseMinAttack"] + "\tBaseMaxAttack: " + reader["BaseMaxAttack"] + "\tCoolDown: " + reader["BaseCoolDown"]);
                Monsters.Add(new
                {
                    Name = reader["name"],
                    HP = reader["HP"],
                    BaseMinAttack = reader["BaseMinAttack"],
                    BaseMaxAttack = reader["BaseMaxAttack"],
                    BaseCoolDown = reader["BaseCoolDown"],
                    GivenExp = reader["GivenExp"],
                    BaseExp = reader["BaseExp"]
                });
            }
            Close();
            return (Monsters);
        }
    }
}
