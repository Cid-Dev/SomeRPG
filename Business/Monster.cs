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
            //float hp = BaseHP;
            Level = lvl;
            BaseStrengh += (lvl - 1);
            BaseHP *= (int)Math.Round(Math.Pow(hpMultiplier, lvl - 1));
            /*
            while (lvl > 1)
            {
                hp *= hpMultiplier;
                --lvl;
            }
            BaseHP = (int)Math.Round(hp);
            */
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

        private void getChestArmor(object lootTable)
        {
            var ArmorChestListAnon = lootTable?.GetType().GetProperty("ArmorChest")?.GetValue(lootTable, null);
            if (ArmorChestListAnon is IEnumerable<object>)
            {
                var ArmorChestList = ArmorChestListAnon as IEnumerable<object>;

                foreach (var tempArmorChest in ArmorChestList)
                {
                    try
                    {
                        var chestArmor = new ChestArmor(tempArmorChest?.GetType().GetProperty("Name")?.GetValue(tempArmorChest, null).ToString());
                        var MinAmount = int.Parse(tempArmorChest?.GetType().GetProperty("MinAmount")?.GetValue(tempArmorChest, null).ToString());
                        var MaxAmount = int.Parse(tempArmorChest?.GetType().GetProperty("MaxAmount")?.GetValue(tempArmorChest, null).ToString());
                        var Probability = double.Parse(tempArmorChest?.GetType().GetProperty("Probability")?.GetValue(tempArmorChest, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = chestArmor,
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

        private void getLegsArmor(object lootTable)
        {
            var ArmorLegsListAnon = lootTable?.GetType().GetProperty("ArmorLegs")?.GetValue(lootTable, null);
            if (ArmorLegsListAnon is IEnumerable<object>)
            {
                var ArmorLegsList = ArmorLegsListAnon as IEnumerable<object>;

                foreach (var tempArmorLegs in ArmorLegsList)
                {
                    try
                    {
                        var legsArmor = new LegsArmor(tempArmorLegs?.GetType().GetProperty("Name")?.GetValue(tempArmorLegs, null).ToString());
                        var MinAmount = int.Parse(tempArmorLegs?.GetType().GetProperty("MinAmount")?.GetValue(tempArmorLegs, null).ToString());
                        var MaxAmount = int.Parse(tempArmorLegs?.GetType().GetProperty("MaxAmount")?.GetValue(tempArmorLegs, null).ToString());
                        var Probability = double.Parse(tempArmorLegs?.GetType().GetProperty("Probability")?.GetValue(tempArmorLegs, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = legsArmor,
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

        private void getFeetArmor(object lootTable)
        {
            var ArmorFeetListAnon = lootTable?.GetType().GetProperty("ArmorFeet")?.GetValue(lootTable, null);
            if (ArmorFeetListAnon is IEnumerable<object>)
            {
                var ArmorFeetList = ArmorFeetListAnon as IEnumerable<object>;

                foreach (var tempArmorFeet in ArmorFeetList)
                {
                    try
                    {
                        var feetArmor = new FeetArmor(tempArmorFeet?.GetType().GetProperty("Name")?.GetValue(tempArmorFeet, null).ToString());
                        var MinAmount = int.Parse(tempArmorFeet?.GetType().GetProperty("MinAmount")?.GetValue(tempArmorFeet, null).ToString());
                        var MaxAmount = int.Parse(tempArmorFeet?.GetType().GetProperty("MaxAmount")?.GetValue(tempArmorFeet, null).ToString());
                        var Probability = double.Parse(tempArmorFeet?.GetType().GetProperty("Probability")?.GetValue(tempArmorFeet, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = feetArmor,
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

        private void getSleevesArmor(object lootTable)
        {
            var ArmorSleevesListAnon = lootTable?.GetType().GetProperty("ArmorSleeves")?.GetValue(lootTable, null);
            if (ArmorSleevesListAnon is IEnumerable<object>)
            {
                var ArmorSleevesList = ArmorSleevesListAnon as IEnumerable<object>;

                foreach (var tempArmorSleeves in ArmorSleevesList)
                {
                    try
                    {
                        var sleevesArmor = new SleevesArmor(tempArmorSleeves?.GetType().GetProperty("Name")?.GetValue(tempArmorSleeves, null).ToString());
                        var MinAmount = int.Parse(tempArmorSleeves?.GetType().GetProperty("MinAmount")?.GetValue(tempArmorSleeves, null).ToString());
                        var MaxAmount = int.Parse(tempArmorSleeves?.GetType().GetProperty("MaxAmount")?.GetValue(tempArmorSleeves, null).ToString());
                        var Probability = double.Parse(tempArmorSleeves?.GetType().GetProperty("Probability")?.GetValue(tempArmorSleeves, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = sleevesArmor,
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

        private void getHandsArmor(object lootTable)
        {
            var ArmorHandsListAnon = lootTable?.GetType().GetProperty("ArmorHands")?.GetValue(lootTable, null);
            if (ArmorHandsListAnon is IEnumerable<object>)
            {
                var ArmorHandsList = ArmorHandsListAnon as IEnumerable<object>;

                foreach (var tempArmorHands in ArmorHandsList)
                {
                    try
                    {
                        var handsArmor = new HandsArmor(tempArmorHands?.GetType().GetProperty("Name")?.GetValue(tempArmorHands, null).ToString());
                        var MinAmount = int.Parse(tempArmorHands?.GetType().GetProperty("MinAmount")?.GetValue(tempArmorHands, null).ToString());
                        var MaxAmount = int.Parse(tempArmorHands?.GetType().GetProperty("MaxAmount")?.GetValue(tempArmorHands, null).ToString());
                        var Probability = double.Parse(tempArmorHands?.GetType().GetProperty("Probability")?.GetValue(tempArmorHands, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = handsArmor,
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

        private void getHeadArmor(object lootTable)
        {
            var ArmorHeadListAnon = lootTable?.GetType().GetProperty("ArmorHead")?.GetValue(lootTable, null);
            if (ArmorHeadListAnon is IEnumerable<object>)
            {
                var ArmorHeadList = ArmorHeadListAnon as IEnumerable<object>;

                foreach (var tempArmorHead in ArmorHeadList)
                {
                    try
                    {
                        var headArmor = new HeadArmor(tempArmorHead?.GetType().GetProperty("Name")?.GetValue(tempArmorHead, null).ToString());
                        var MinAmount = int.Parse(tempArmorHead?.GetType().GetProperty("MinAmount")?.GetValue(tempArmorHead, null).ToString());
                        var MaxAmount = int.Parse(tempArmorHead?.GetType().GetProperty("MaxAmount")?.GetValue(tempArmorHead, null).ToString());
                        var Probability = double.Parse(tempArmorHead?.GetType().GetProperty("Probability")?.GetValue(tempArmorHead, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = headArmor,
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
            getChestArmor(lootTable);
            getSleevesArmor(lootTable);
            getFeetArmor(lootTable);
            getHandsArmor(lootTable);
            getHeadArmor(lootTable);
            getLegsArmor(lootTable);
        }
    }
}
