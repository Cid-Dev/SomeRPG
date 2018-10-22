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

        public void BuildLootTable(object lootTable)
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
    }
}
