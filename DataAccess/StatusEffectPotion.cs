using System.Collections.Generic;
using System.Data.SQLite;

namespace DataAccess
{
    public class StatusEffectPotion : DB
    {
        List<object> StatusEffectPotions;

        private string BuildQuery(string fieldName, string parameterName)
        {
            string query = "SELECT s.id AS id, "
                            + "i.name AS Name, "
                            + "i.description AS Description, "
                            + "s.max_amount AS MaxAmount, "
                            + "b.id AS BuffId "
                         + "FROM " + StatusEffectPotionTable + " s "
                         + "INNER JOIN " + ItemTable + " i "
                         + "ON s.item_id=i.id "
                         + "INNER JOIN " + BuffTable + " b "
                         + "ON s.buff_id=b.id "
                         + "WHERE " + fieldName + " = " + parameterName + " "
                         + "LIMIT 1";
            return (query);
        }

        private void BuildStatusEffectPotions(SQLiteDataReader StatusEffectPotion)
        {
            object buff = null;
            if (StatusEffectPotion["BuffId"] != null)
            {
                var BuffId = int.Parse(StatusEffectPotion["BuffId"].ToString());
                var dalbuff = new Buff();
                var buffs = dalbuff.GetBuff(BuffId);
                if (buffs != null
                    && buffs.Count > 0)
                    buff = buffs[0];
            }
            StatusEffectPotions.Add(new
            {
                Id = StatusEffectPotion["id"],
                Name = StatusEffectPotion["Name"],
                Description = StatusEffectPotion["Description"],
                MaxAmount = StatusEffectPotion["MaxAmount"],
                Buff = buff
            });
        }

        public List<object> GetStatusEffectPotion(int id)
        {
            StatusEffectPotions = new List<object>();
            string query = BuildQuery("s.id", "@id");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            GetDatas(query, parameters, BuildStatusEffectPotions);

            return (StatusEffectPotions);
        }

        public List<object> GetStatusEffectPotion(string name)
        {
            StatusEffectPotions = new List<object>();
            string query = BuildQuery("i.name", "@name");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@name", name }
            };

            GetDatas(query, parameters, BuildStatusEffectPotions);

            return (StatusEffectPotions);
        }
    }
}
