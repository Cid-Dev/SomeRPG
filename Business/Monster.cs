using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Business
{
    public class Monster : Character
    {
        public List<Loot> LootTable = new List<Loot>();
        public int MinMoney { get; set; }
        public int MaxMoney { get; set; }
        public float MoneyMultiplier { get; set; }

        public int LootMoney()
        {
            int loot = seed.Next((int)Math.Round(MinMoney * MoneyMultiplier * Level), (int)Math.Round(MaxMoney * MoneyMultiplier * Level) + 1);

            return (loot);
        }

        public List<Item> GetLoots()
        {
            List<Item> Loots = new List<Item>();

            foreach (var loot in LootTable)
            {
                var result = seed.NextDouble() * 100;
                
                if (result <= loot.Probability)
                {
                    Item NewItem = loot.item.Clone() as Item;
                    if (NewItem is IStackable)
                    {
                        var amount = seed.Next(loot.MinAmount, loot.MaxAmount + 1);
                        (NewItem as IStackable).Quantity = amount;
                    }
                    Loots.Add(NewItem);
                }
            }

            return (Loots);
        }

        private void GetLoot<T>(object lootTable, string propertyName) where T : class
        {
            var ItemListAnon = lootTable?.GetType().GetProperty(propertyName)?.GetValue(lootTable, null);
            if (ItemListAnon is IEnumerable<object>)
            {
                var ItemList = ItemListAnon as IEnumerable<object>;

                foreach (var tempItem in ItemList)
                {
                    try
                    {
                        Item item = null;
                        if (typeof(T) == typeof(Weapon))
                            item = new Weapon(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        else if (typeof(T) == typeof(ChestArmor))
                            item = new ChestArmor(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        else if (typeof(T) == typeof(LegsArmor))
                            item = new LegsArmor(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        else if (typeof(T) == typeof(FeetArmor))
                            item = new FeetArmor(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        else if (typeof(T) == typeof(SleevesArmor))
                            item = new SleevesArmor(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        else if (typeof(T) == typeof(HandsArmor))
                            item = new HandsArmor(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        else if (typeof(T) == typeof(HeadArmor))
                            item = new HeadArmor(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        else if (typeof(T) == typeof(HPPotion))
                            item = new HPPotion(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        else if (typeof(T) == typeof(StatusEffectPotion))
                            item = new StatusEffectPotion(tempItem?.GetType().GetProperty("Name")?.GetValue(tempItem, null).ToString());
                        var MinAmount = int.Parse(tempItem?.GetType().GetProperty("MinAmount")?.GetValue(tempItem, null).ToString());
                        var MaxAmount = int.Parse(tempItem?.GetType().GetProperty("MaxAmount")?.GetValue(tempItem, null).ToString());
                        var Probability = double.Parse(tempItem?.GetType().GetProperty("Probability")?.GetValue(tempItem, null).ToString());
                        LootTable.Add(new Loot
                        {
                            item = item,
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
            GetLoot<Weapon>(lootTable, "Weapon");
            GetLoot<ChestArmor>(lootTable, "ArmorChest");
            GetLoot<LegsArmor>(lootTable, "ArmorLegs");
            GetLoot<FeetArmor>(lootTable, "ArmorFeet");
            GetLoot<SleevesArmor>(lootTable, "ArmorSleeves");
            GetLoot<HandsArmor>(lootTable, "ArmorHands");
            GetLoot<HeadArmor>(lootTable, "ArmorHead");
            GetLoot<HPPotion>(lootTable, "HPPotion");
            GetLoot<StatusEffectPotion>(lootTable, "StatusEffectPotion");
        }
    }
}
