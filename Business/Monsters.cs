using System;
using System.Collections.Generic;
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
                        Add(new Monster
                        {
                            Name = dalMonster?.GetType().GetProperty("Name")?.GetValue(dalMonster, null).ToString(),
                            BaseHP = int.Parse(dalMonster?.GetType().GetProperty("HP")?.GetValue(dalMonster, null).ToString()),
                            BaseMinAttack = int.Parse(dalMonster?.GetType().GetProperty("BaseMinAttack")?.GetValue(dalMonster, null).ToString()),
                            BaseMaxAttack = int.Parse(dalMonster?.GetType().GetProperty("BaseMaxAttack")?.GetValue(dalMonster, null).ToString()),
                            BaseCooldown = int.Parse(dalMonster?.GetType().GetProperty("BaseCoolDown")?.GetValue(dalMonster, null).ToString()),
                            GivenExp = int.Parse(dalMonster?.GetType().GetProperty("GivenExp")?.GetValue(dalMonster, null).ToString()),
                            BaseExp = int.Parse(dalMonster?.GetType().GetProperty("BaseExp")?.GetValue(dalMonster, null).ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
