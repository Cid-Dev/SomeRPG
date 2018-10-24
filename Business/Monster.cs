using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Monster : Character
    {
        public List<Loot> LootTable = new List<Loot>();
        public int MinMoney { get; set; }
        public int MaxMoney { get; set; }
        public float MoneyMultiplier { get; set; }


        public void SetLevel(int lvl)
        {
            float hp = BaseHP;
            while (lvl > 1)
            {
                hp *= hpMultiplier;
                --lvl;
            }
            BaseHP = (int)Math.Round(hp);
        }

        public int LootMoney()
        {
            Random seed = new Random();

            int loot = seed.Next((int)Math.Round(MinMoney * MoneyMultiplier * Level), (int)Math.Round(MaxMoney * MoneyMultiplier * Level) + 1);

            return (loot);
        }

        public List<Item> GetLoots()
        {
            List<Item> Loots = new List<Item>();
            Random randomdbl = new Random();
            Random randomint = new Random();

            foreach (var loot in LootTable)
            {
                var result = randomdbl.NextDouble() * 100;
                
                if (result <= loot.Probability)
                {
                    Item NewItem = loot.item.Clone() as Item;
                    if (NewItem is IStackable)
                    {
                        var amount = randomint.Next(loot.MinAmount, loot.MaxAmount + 1);
                        (NewItem as IStackable).Quantity = amount;
                    }
                    Loots.Add(NewItem);
                    //var NewItem = loot.item.Clone();
                    
                }
            }

            return (Loots);
        }

        private void getRightHands(object lootTable)
        {
            var RightHandListAnon = lootTable?.GetType().GetProperty("RightHand")?.GetValue(lootTable, null);
            if (RightHandListAnon is IEnumerable<object>)
            {
                var RightHandList = RightHandListAnon as IEnumerable<object>;

                foreach (var tempRightHand in RightHandList)
                {
                    try
                    {
                        var rightHand = new RightHand(tempRightHand?.GetType().GetProperty("Name")?.GetValue(tempRightHand, null).ToString());
                        var MinAmount = int.Parse(tempRightHand?.GetType().GetProperty("MinAmount")?.GetValue(tempRightHand, null).ToString());
                        var MaxAmount = int.Parse(tempRightHand?.GetType().GetProperty("MaxAmount")?.GetValue(tempRightHand, null).ToString());
                        var Probability = double.Parse(tempRightHand?.GetType().GetProperty("Probability")?.GetValue(tempRightHand, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = rightHand,
                            MaxAmount = MaxAmount,
                            MinAmount = MinAmount,
                            Probability = Probability
                        });
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void getPotions(object lootTable)
        {
            var HPPotionListAnon = lootTable?.GetType().GetProperty("HPPotion")?.GetValue(lootTable, null);
            if (HPPotionListAnon is IEnumerable<object>)
            {
                var HPPotionList = HPPotionListAnon as IEnumerable<object>;

                foreach (var tempHPPotion in HPPotionList)
                {
                    try
                    {
                        var hPPotion = new HPPotion(tempHPPotion?.GetType().GetProperty("Name")?.GetValue(tempHPPotion, null).ToString());
                        var MinAmount = int.Parse(tempHPPotion?.GetType().GetProperty("MinAmount")?.GetValue(tempHPPotion, null).ToString());
                        var MaxAmount = int.Parse(tempHPPotion?.GetType().GetProperty("MaxAmount")?.GetValue(tempHPPotion, null).ToString());
                        var Probability = double.Parse(tempHPPotion?.GetType().GetProperty("Probability")?.GetValue(tempHPPotion, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = hPPotion,
                            MaxAmount = MaxAmount,
                            MinAmount = MinAmount,
                            Probability = Probability
                        });
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void BuildLootTable(object lootTable)
        {
            getPotions(lootTable);
            getRightHands(lootTable);
        }
    }
}
