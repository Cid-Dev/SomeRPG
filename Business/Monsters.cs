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
                            BaseRightMinAttack = int.Parse(dalMonster?.GetType().GetProperty("BaseRightMinAttack")?.GetValue(dalMonster, null).ToString()),
                            BaseRightMaxAttack = int.Parse(dalMonster?.GetType().GetProperty("BaseRightMaxAttack")?.GetValue(dalMonster, null).ToString()),
                            BaseLeftMinAttack = int.Parse(dalMonster?.GetType().GetProperty("BaseLeftMinAttack")?.GetValue(dalMonster, null).ToString()),
                            BaseLeftMaxAttack = int.Parse(dalMonster?.GetType().GetProperty("BaseLeftMaxAttack")?.GetValue(dalMonster, null).ToString()),
                            BaseCooldown = int.Parse(dalMonster?.GetType().GetProperty("BaseCoolDown")?.GetValue(dalMonster, null).ToString()),
                            GivenExp = int.Parse(dalMonster?.GetType().GetProperty("GivenExp")?.GetValue(dalMonster, null).ToString()),
                            BaseExp = int.Parse(dalMonster?.GetType().GetProperty("BaseExp")?.GetValue(dalMonster, null).ToString()),
                            MinMoney = int.Parse(dalMonster?.GetType().GetProperty("MinMoney")?.GetValue(dalMonster, null).ToString()),
                            MaxMoney = int.Parse(dalMonster?.GetType().GetProperty("MaxMoney")?.GetValue(dalMonster, null).ToString()),
                            BaseStrengh = int.Parse(dalMonster?.GetType().GetProperty("BaseStrengh")?.GetValue(dalMonster, null).ToString()),
                            BaseAgility = int.Parse(dalMonster?.GetType().GetProperty("BaseAgility")?.GetValue(dalMonster, null).ToString()),
                            BaseVitality = int.Parse(dalMonster?.GetType().GetProperty("BaseVitality")?.GetValue(dalMonster, null).ToString()),
                            BasePrecision = int.Parse(dalMonster?.GetType().GetProperty("BasePrecision")?.GetValue(dalMonster, null).ToString()),
                            BaseDexterity = int.Parse(dalMonster?.GetType().GetProperty("BaseDexterity")?.GetValue(dalMonster, null).ToString()),
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
