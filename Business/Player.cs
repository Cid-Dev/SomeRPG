using System;
using System.Collections.Generic;
using DataAccess;

namespace Business
{
    public class Player : Character
    {
        public List<Item> Inventory = new List<Item>();
        public int Money { get; set; }

        private void StackItem(Item NewItem)
        {
            foreach (var item in Inventory)
            {
                if (item.Name == NewItem.Name
                    && item is IStackable
                    && (NewItem as IStackable).Quantity > 0)
                {
                    var stackable = item as IStackable;
                    if (stackable.Quantity < stackable.MaxAmount)
                    {
                        if (stackable.Quantity + (NewItem as IStackable).Quantity <= stackable.MaxAmount)
                        {
                            stackable.Quantity += (NewItem as IStackable).Quantity;
                            (NewItem as IStackable).Quantity = 0;
                        }
                        else
                        {
                            (NewItem as IStackable).Quantity -= (stackable.MaxAmount - stackable.Quantity);
                            stackable.Quantity = stackable.MaxAmount;
                        }
                    }
                }
            }
            if ((NewItem as IStackable).Quantity > 0)
                Inventory.Add(NewItem.Clone() as Item);
        }

        public void AddItem(Item item)
        {
            if (item is IStackable)
                StackItem(item);
            else
                Inventory.Add(item.Clone() as Item);
        }

        private List<StackableSave> getStackables<T>() where T : class
        {
            var ItemsToGet = new List<StackableSave>();
            foreach (var item in Inventory)
            {
                if (item is T)
                {
                    var itemToGet = (item as IStackable);
                    ItemsToGet.Add(new StackableSave
                    {
                        Id = (itemToGet as Item).Id,
                        Quantity = itemToGet.Quantity
                    });
                }
            }
            return (ItemsToGet);
        }

        private List<ItemSave> getItems<T>() where T : class
        {
            var ItemsToGet = new List<ItemSave>();
            foreach (var item in Inventory)
            {
                if (item is T)
                {
                    var itemToGet = (item as Item);
                    ItemsToGet.Add(new ItemSave
                    {
                        Id = itemToGet.Id
                    });
                }
            }
            return (ItemsToGet);
        }

        private List<StatusSave> GetActiveBuffs(List<Status> statuss)
        {
            List<StatusSave> statusSaves = new List<StatusSave>();

            foreach (var status in statuss)
            {
                switch (status)
                {
                    case Buff b:
                        statusSaves.Add(new BuffSave()
                        {
                            Id = b.Id,
                            RemainingDuration = b.RemainingDuration
                        });
                        break;

                    case Dot d:
                        statusSaves.Add(new DotSave()
                        {
                            Damage = d.Damage,
                            Frequency = d.Frequency,
                            Quantity = d.Quantity,
                            RemainingQuantity = d.RemainingQuantity,
                            TimeBeforeNextTick = d.TimeBeforeNextTick,
                            Type = d.Type
                        });
                        break;
                }
            }
            return (statusSaves);
        }

        public string Save()
        {
            try
            {
                GameSave gameSave = new GameSave();
                gameSave.Save(new PlayerSave
                {
                    Name = Name,
                    Level = Level,
                    CurrentExp = _currentExp,
                    Money = Money,
                    CurrentHP = CurrentHP,
                    RightHand = ((RightHand != null) ? (RightHand.Id) : (0)),
                    LeftHand = ((LeftHand != null) ? (LeftHand.Id) : (0)),
                    ChestArmor = ((ChestArmor != null) ? (ChestArmor.Id) : (0)),
                    LegsArmor = ((LegsArmor != null) ? (LegsArmor.Id) : (0)),
                    SleevesArmor = ((SleevesArmor != null) ? (SleevesArmor.Id) : (0)),
                    FeetArmor = ((FeetArmor != null) ? (FeetArmor.Id) : (0)),
                    HandsArmor = ((HandsArmor != null) ? (HandsArmor.Id) : (0)),
                    HeadArmor = ((HeadArmor != null) ? (HeadArmor.Id) : (0)),
                    Buffs = GetActiveBuffs(Buffs),
                    DeBuffs = GetActiveBuffs(DeBuffs),
                    Inventory = new InventorySave
                    {
                        HPPotions = getStackables<HPPotion>(),
                        Weapons = getItems<Weapon>(),
                        ChestArmors = getItems<ChestArmor>(),
                        SleevesArmors = getItems<SleevesArmor>(),
                        LegsArmors = getItems<LegsArmor>(),
                        FeetArmors = getItems<FeetArmor>(),
                        HandsArmors = getItems<HandsArmor>(),
                        HeadArmors = getItems<HeadArmor>(),
                        StatusEffectPotions = getStackables<StatusEffectPotion>()
                    }
                });
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
            return ("");
        }
    }
}
