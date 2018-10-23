using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class Monsters : List<Monster>
    {
        public Monsters()
        {
            try
            {
                DataAccess.Monster DalMonsters = new DataAccess.Monster();

                var temp = DalMonsters.GetAll();
                if (temp.Count() > 0)
                {
                    foreach (var dalMonster in temp)
                    {
                        var monster = new Monster
                        {
                            Name = dalMonster?.GetType().GetProperty("Name")?.GetValue(dalMonster, null).ToString(),
                            BaseHP = int.Parse(dalMonster?.GetType().GetProperty("HP")?.GetValue(dalMonster, null).ToString()),
                            BaseMinAttack = int.Parse(dalMonster?.GetType().GetProperty("BaseMinAttack")?.GetValue(dalMonster, null).ToString()),
                            BaseMaxAttack = int.Parse(dalMonster?.GetType().GetProperty("BaseMaxAttack")?.GetValue(dalMonster, null).ToString()),
                            BaseCooldown = int.Parse(dalMonster?.GetType().GetProperty("BaseCoolDown")?.GetValue(dalMonster, null).ToString()),
                            GivenExp = int.Parse(dalMonster?.GetType().GetProperty("GivenExp")?.GetValue(dalMonster, null).ToString()),
                            BaseExp = int.Parse(dalMonster?.GetType().GetProperty("BaseExp")?.GetValue(dalMonster, null).ToString()),
                            MinMoney = int.Parse(dalMonster?.GetType().GetProperty("MinMoney")?.GetValue(dalMonster, null).ToString()),
                            MaxMoney = int.Parse(dalMonster?.GetType().GetProperty("MaxMoney")?.GetValue(dalMonster, null).ToString()),
                            MoneyMultiplier = float.Parse(dalMonster?.GetType().GetProperty("MoneyMultiplier")?.GetValue(dalMonster, null).ToString())
                        };

                        monster.BuildLootTable(dalMonster?.GetType().GetProperty("LootTable")?.GetValue(dalMonster, null));

                        Add(monster);

                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
