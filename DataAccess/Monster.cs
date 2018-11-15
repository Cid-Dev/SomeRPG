using System;
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
        List<object> Loots;
        List<object> Monsters;

        private void BuildLoot(SQLiteDataReader loot)
        {
            Loots.Add(new
            {
                Name = loot["Name"],
                MinAmount = loot["MinAmount"],
                MaxAmount = loot["MaxAmount"],
                Probability = loot["Probability"]
            });
        }

        private string BuildLootQuery(string fieldName, string ReferenceFieldName, string parameterName, string referenceTableName, string TableName)
        {
            string query = "SELECT i.name AS Name, "
                                + "l.min_amount AS MinAmount, "
                                + "l.max_amount AS MaxAmount, "
                                + "l.probability AS Probability "
                         + "FROM " + MonsterTable + " m "
                         + "INNER JOIN " + referenceTableName + " l "
                         + "ON l.monster_id=m.id "
                         + "INNER JOIN " + TableName + " w "
                         + "ON l." + ReferenceFieldName + "=w.id "
                         + "INNER JOIN " + ItemTable + " i "
                         + "ON w.item_id=i.id "
                         + "WHERE m." + fieldName + " = " + parameterName;
            return (query);
        }

        private List<object> GetLootTable(int monster_id, string ReferenceFieldName, string referenceTableName, string TableName)
        {
            Loots = new List<object>();
            string query = BuildLootQuery("id", ReferenceFieldName, "@monster_id", referenceTableName, TableName);
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@monster_id", monster_id }
            };

            GetDatas(query, parameters, BuildLoot);

            return (Loots);
        }

        private void BuildMonster(SQLiteDataReader monster)
        {
            var monster_id = int.Parse(monster["id"].ToString());
            Monsters.Add(new
            {
                Name = monster["name"],
                HP = monster["HP"],
                BaseRightMinAttack = monster["BaseRightMinAttack"],
                BaseRightMaxAttack = monster["BaseRightMaxAttack"],
                BaseLeftMinAttack = monster["BaseLeftMinAttack"],
                BaseLeftMaxAttack = monster["BaseLeftMaxAttack"],
                BaseCoolDown = monster["BaseCoolDown"],
                GivenExp = monster["GivenExp"],
                BaseExp = monster["BaseExp"],
                MinMoney = monster["MinMoney"],
                MaxMoney = monster["MaxMoney"],
                MoneyMultiplier = monster["MoneyMultiplier"],
                BaseStrengh = monster["BaseStrengh"],
                BaseVitality = monster["BaseVitality"],
                BaseAgility = monster["BaseAgility"],
                BasePrecision = monster["BasePrecision"],
                BaseDexterity = monster["BaseDexterity"],
                LootTable = new
                {
                    HPPotion = GetLootTable(monster_id, "hppotion_id", loot_table_monster_hppotion, HPPotionTable),
                    Weapon = GetLootTable(monster_id, "weapon_id", loot_table_monster_weapon, WeaponTable),
                    ArmorChest = GetLootTable(monster_id, "chestarmor_id", loot_table_monster_chestarmor, ChestArmorTable),
                    ArmorSleeves = GetLootTable(monster_id, "sleevesarmor_id", loot_table_monster_sleevesarmor, SleevesArmorTable),
                    ArmorLegs = GetLootTable(monster_id, "legsarmor_id", loot_table_monster_legsarmor, LegsArmorTable),
                    ArmorFeet = GetLootTable(monster_id, "feetarmor_id", loot_table_monster_feetarmor, FeetArmorTable),
                    ArmorHands = GetLootTable(monster_id, "handsarmor_id", loot_table_monster_handsarmor, HandsArmorTable),
                    ArmorHead = GetLootTable(monster_id, "headarmor_id", loot_table_monster_headarmor, HeadArmorTable),
                    StatusEffectPotion = GetLootTable(monster_id, "statuseffectpotions_id", loot_table_monster_statuseffectpotions, StatusEffectPotionTable)
                }
            });
        }

        public List<object> GetAll()
        {
            Monsters = new List<object>();
            string query = "select * from " + MonsterTable;
            GetDatas(query, null, BuildMonster);

            return (Monsters);
        }
    }
}
