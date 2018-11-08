﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Business;

namespace SomeRPG
{
    class Program
    {
        static Player player;

        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        public static string ShowEffects(List<Status> effects)
        {
            string result = "";

            foreach (var effect in effects)
            {
                switch (effect)
                {
                    case Dot dot:
                        result += "\t\t\tDoT type : " + dot.Type + " - " + dot.Quantity + " x Damage " + dot.Damage + " every " + dot.Frequency + "\n";
                        break;

                    case Buff buff:
                        result += buff.Description(3);
                        break;

                    default:
                        break;
                }
            }
            return (result);
        }

        public static string ShowOpening(Opening opening)
        {
            string result = "";

            if (opening.Skill != null)
                result += opening.Skill.Name;

            return (result);
        }

        public static string SkillDetail(ActiveSkill skill)
        {
            string result = "\t- ";

            result = skill.Name + "(Level " + skill.Level + ") : " + skill.Description + "\n";

            if (skill.Required != null)
            {
                if (skill.Required.RequiredWeapon != null)
                    result += "\t\t- Required weapon : " + skill.Required.RequiredWeapon.ToString() + "\n";
            }

            if (skill.Damage != null)
                result += "\t\t- Damage : " + skill.Damage + "\n";

            if (skill.Effects != null
                && skill.Effects.Count > 0)
                result += "\t\t- Effects :\n" + ShowEffects(skill.Effects);

            if (skill.Opening != null)
                result += "\t\t- Opening : " + ShowOpening(skill.Opening) + "\n";

            return (result);
        }

        public static void Stats()
        {
            Console.Write("=== Name : " + player.Name + " === HP : ");
            if ((player.CurrentHP * 100) / player.HP <= 25)
                Console.ForegroundColor = ConsoleColor.Red;
            else if ((player.CurrentHP * 100) / player.HP <= 50)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(player.CurrentHP);
            Console.ResetColor();
            Console.Write("/" + player.HP + " === Damages : Right hand [" + player.CurrentRightMinAttack + " - " + player.CurrentRightMaxAttack + "]");
            if (player.CurrentLeftMinAttack > 0
                || player.CurrentLeftMaxAttack > 0)
                Console.Write(" Left hand [" + player.CurrentLeftMinAttack + " - " + player.CurrentLeftMaxAttack + "]");
            Console.WriteLine(" === Level : " + player.Level + " === Exp : " + player._currentExp + "/" + player.getRequiredExp + " ===\n");
            if (player.Money > 0)
                Console.WriteLine("=== Money : " + player.ConvertMoney(player.Money) + " ===\n");
            if (player.Buffs.Count > 0)
            {
                Console.WriteLine("=== Buff" + ((player.Buffs.Count > 1) ? ("s") : ("")) + ":\n");
                foreach (Buff buff in player.Buffs)
                    Console.WriteLine(buff.Description());
            }
            if (player.DeBuffs.Count > 0)
            {
                Console.WriteLine("=== Debuff" + ((player.DeBuffs.Count > 1) ? ("s") : ("")) + ":\n");
                foreach (Buff deBuff in player.DeBuffs)
                    Console.WriteLine(deBuff.Description());
            }
        }

        public static string WeaponDetail(Weapon weapon)
        {
            string result = "";
            if (weapon != null)
                result += "Min attack : " + weapon.MinDamageBonus + " - Max attack : " + weapon.MaxDamageBonus + " - Type : " + weapon.TypeName.ToString() + ((weapon.isTwoHand) ? (" Two-Handed") : (""));
            return (result);
        }

        public static string ArmorDetail(Armor armor)
        {
            string result = "";
            if (armor != null)
                result += "Defense : " + armor.Defense + " - Type : " + armor.ArmorType.Name + " - Absorbency : " + armor.ArmorType.Absorbency + "%";
            return (result);
        }

        public static void DetailedStats()
        {
            Console.WriteLine("=== Stats ===\n");
            Console.WriteLine("\tStrengh : " + player.Strengh);
            Console.WriteLine("\tVitality : " + player.Vitality);
            Console.WriteLine("\tAgility : " + player.Agility);
            Console.WriteLine("\tPrecision : " + player.Precision);
            Console.WriteLine("\tDexterity : " + player.Dexterity);
            Console.WriteLine();
        }

        public static void MonsterStats(Character monster)
        {
            Console.Write("=== Name : " + monster.Name + " === HP : ");
            if ((monster.CurrentHP * 100) / monster.HP <= 25)
                Console.ForegroundColor = ConsoleColor.Red;
            else if ((monster.CurrentHP * 100) / monster.HP <= 50)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(monster.CurrentHP);
            Console.ResetColor();
            Console.Write("/" + monster.HP + " === Damages : Right hand [" + monster.CurrentRightMinAttack + " - " + monster.CurrentRightMaxAttack + "]");
            if (monster.CurrentLeftMinAttack > 0
                || monster.CurrentLeftMaxAttack > 0)
                Console.Write(" Left hand [" + monster.CurrentLeftMinAttack + " - " + monster.CurrentLeftMaxAttack + "]");
            Console.WriteLine(" === Level : " + monster.Level + "\n");
            if (monster.Buffs.Count > 0)
            {
                Console.WriteLine("=== Buff" + ((monster.Buffs.Count > 1) ? ("s") : ("")) + ":\n");
                foreach (Buff buff in monster.Buffs)
                    Console.WriteLine(buff.Description());
            }
            if (monster.DeBuffs.Count > 0)
            {
                Console.WriteLine("=== Debuff" + ((monster.DeBuffs.Count > 1) ? ("s") : ("")) + ":\n");
                foreach (Buff deBuff in monster.DeBuffs)
                    Console.WriteLine(deBuff.Description());
            }
        }

        static void DisplayFightInfos(Character monster)
        {
            Console.Clear();
            Stats();
            Console.WriteLine("You are fighting a monster !");
            MonsterStats(monster);
            Console.WriteLine("=== " + player.Name + " : " + player.CurrentCooldown + " === " + monster.Name + " : " + monster.CurrentCooldown + " ===");
        }

        static bool CheckUsableSkill(ActiveSkill skill)
        {
            if (skill.Required != null)
            {
                if (skill.Required.RequiredWeapon != null)
                {
                    if (!(player.RightHand != null
                            && player.RightHand is Weapon
                            && (player.RightHand as Weapon).TypeName == skill.Required.RequiredWeapon))
                        return (false);
                }
            }

            if (skill.Opening != null)
            {
                if (player.LastOpening != null)
                {
                    if (skill.Opening.Skill != null)
                    {
                        if (!(player.LastOpening.Skill != null
                            && skill.Opening.Skill == player.LastOpening.Skill))
                            return (false);
                    }
                }
                else
                    return (false);
            }
            return (true);
        }

        static bool SelectSkill(SkillFamily selected)
        {
            string error = "";
            string input;
            bool back = false;
            bool IsSkillUsed = false;
            do
            {
                do
                {
                    if (selected.Actives.Count > 0)
                    {

                        Console.Clear();
                        Stats();

                        Console.WriteLine("Skills availables for " + selected.Name + " skill family :\n");
                        int i = 0;
                        foreach (var skill in selected.Actives)
                        {
                            if (!CheckUsableSkill(skill))
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("[" + ++i + "] - " + SkillDetail(skill));
                            Console.ResetColor();
                        }
                        if (error != "")
                            Console.WriteLine(error);
                        Console.WriteLine("Please select a skill or go [[B]ack]");
                    }
                    else
                        Console.WriteLine("No skill. Please go [B]ack]");
                    input = Console.ReadLine();
                    error = "";
                } while (!Regex.IsMatch(input, "^(([0-9]+)|(b(ack)?))$", RegexOptions.IgnoreCase));

                if (int.TryParse(input, out int result))
                {
                    if (result > 0 && result <= selected.Actives.Count)
                    {
                        var skill = selected.Actives[--result];
                        if (CheckUsableSkill(skill))
                        {
                            //IsSkillUsed = skill.Use(player, monster);
                            back = IsSkillUsed;
                        }
                        else
                            error = "Skill " + skill.Name + " actually unavailable";
                    }
                    else
                        error = "Invalid number.";
                }
                else
                    back = true;

            } while (!back);
            return (IsSkillUsed);
        }

        static void ShowSkillFamily(SkillFamily selected)
        {
            Console.Clear();
            Stats();
            Console.WriteLine("Skills availables for " + selected.Name + " skill family :\n");
            foreach (var skill in selected.Actives)
                Console.WriteLine(SkillDetail(skill));
            Console.WriteLine("Press any key to go back.");
            ClearKeyBuffer();
            Console.ReadKey(true);
        }

        static bool SkillsMenu(bool isInBattle = false)
        {
            string error = "";
            string input;
            bool back = false;
            bool IsSkillUsed = false;
            do
            {
                do
                {
                    string options = "";
                    if (player.Skills.Count > 0)
                    {
                        int i = 0;
                        foreach (var skill in player.Skills)
                            options += "\t[" + ++i + "] " + skill.Name + "\n";

                        Console.Clear();
                        Stats();
                        Console.WriteLine(options);
                        if (error != "")
                            Console.WriteLine(error);
                        Console.WriteLine("Please select a skill family or go [[B]ack]");
                    }
                    else
                        Console.WriteLine("No skill. Please go [B]ack]");
                    input = Console.ReadLine();
                    error = "";
                } while (!Regex.IsMatch(input, "^(([0-9]+)|(b(ack)?))$", RegexOptions.IgnoreCase));

                if (int.TryParse(input, out int result))
                {
                    if (result > 0 && result <= player.Skills.Count)
                    {
                        var selected = player.Skills[--result];
                        if (isInBattle)
                        {
                            IsSkillUsed = SelectSkill(selected);
                            back = IsSkillUsed;
                        }
                        else
                            ShowSkillFamily(selected);
                    }
                    else
                        error = "Invalid number.";
                }
                else
                    back = true;

            } while (!back);
            return (IsSkillUsed);
        }

        static void OpenUsableInventory(Character monster)
        {
            bool back = false;
            while (!back)
            {
                string input = "";
                int i = 1;
                List<IUsable> usables;
                do
                {
                    i = 1;
                    usables = null;
                    usables = new List<IUsable>();
                    string options = "Inventory :\n";
                    foreach (Item item in player.Inventory)
                    {
                        if (item is IUsable)
                        {
                            string stack = "";
                            if (item is IStackable)
                            {
                                var stackable = item as IStackable;
                                stack = "(" + stackable.Quantity + "/" + stackable.MaxAmount + ") ";
                            }
                            options += "[" + i++ + "] " + item.Name + " " + stack + ": " + item.Description + "\n";
                            usables.Add(item as IUsable);
                        }
                    }

                    DisplayFightInfos(monster);
                    if (usables.Count > 0)
                    {
                        Console.WriteLine(options);
                        Console.WriteLine("Please select a number or go [[B]ack]");
                    }
                    else
                        Console.WriteLine("No usables items in inventory. Please go [[B]ack]");
                    input = Console.ReadLine();
                } while (!Regex.IsMatch(input, "^([0-9]+)|(b(ack)?)$", RegexOptions.IgnoreCase));


                if (int.TryParse(input, out int result))
                {
                    if (result > 0 && result <= usables.Count)
                    {
                        --result;
                        ConsoleKeyInfo menu;
                        do
                        {
                            DisplayFightInfos(monster);
                            Console.WriteLine("Please select a target for " + (usables[result] as Item).Name);
                            Console.WriteLine("[Y]ourself. [M]onster. [B]ack");
                            ClearKeyBuffer();
                            menu = Console.ReadKey(true);
                        } while (menu.Key != ConsoleKey.Y
                                 && menu.Key != ConsoleKey.M
                                 && menu.Key != ConsoleKey.B);

                        Character target = null;

                        switch (menu.Key)
                        {
                            case (ConsoleKey.Y):
                                target = player;
                                break;

                            case (ConsoleKey.M):
                                target = monster;
                                break;

                            default:
                                break;
                        }

                        if (target != null)
                        {
                            usables[result].Use(target);
                            if (usables[result] is IStackable)
                            {
                                var stackable = usables[result] as IStackable;
                                if (stackable.Quantity <= 0)
                                    player.Inventory.Remove(stackable as Item);
                            }
                            else
                                player.Inventory.Remove(usables[result] as Item);
                        }
                    }
                }
                else
                    back = true;
            }
        }

        static bool selectAction(Character monster)
        {
            ConsoleKeyInfo menu;
            do
            {
                DisplayFightInfos(monster);
                Console.WriteLine("Select your action");
                Console.WriteLine("[A]ttack. [S]kills. [I]nventory. [F]lee like a coward");
                ClearKeyBuffer();
                menu = Console.ReadKey(true);
            } while (menu.Key != ConsoleKey.A
                     && menu.Key != ConsoleKey.S
                     && menu.Key != ConsoleKey.I
                     && menu.Key != ConsoleKey.F);

            switch (menu.Key)
            {
                case (ConsoleKey.A):
                    Console.WriteLine(player.Attack());
                    return (false);

                case (ConsoleKey.S):
                    SkillsMenu(true);
                    player.CurrentCooldown = player.BaseCooldown;
                    return (false);

                case (ConsoleKey.I):
                    OpenUsableInventory(monster);
                    player.CurrentCooldown = player.BaseCooldown;
                    return (false);

                default:
                    return (true);
            }
        }

        static string ConvertMoney(int money)
        {
            string result = "";

            int cents;

            cents = money % 100;
            if (cents > 0)
                result = " " + cents + " copper";
            money /= 100;
            if (money > 0)
            {
                cents = money % 100;
                if (cents > 0)
                    result = " " + cents + " silver" + result;
                money /= 100;
                if (money > 0)
                {
                    cents = money % 1000;
                    if (cents > 0)
                        result = " " + cents + " gold" + result;
                    money /= 1000;
                    if (money > 0)
                        result = " " + money + " platinium" + result;
                }
            }
            return (result);
        }

        static void ManageEffectStatus(Character target)
        {
            for (int i = 0; i < target.Buffs.Count; ++i)
            {
                --target.Buffs[i].RemainingDuration;
                if (target.Buffs[i].RemainingDuration <= 0)
                    target.Buffs[i].RemoveEffect(target);
            }
            for (int i = 0; i < target.DeBuffs.Count; ++i)
            {
                --target.DeBuffs[i].RemainingDuration;
                if (target.DeBuffs[i].RemainingDuration <= 0)
                    target.DeBuffs[i].RemoveEffect(target);
            }
        }

        static void Fight(Monster monster)
        {
            player.Target = monster;
            monster.Target = player;
            bool hasFlee = false;
            while (player.CurrentHP > 0 && monster.CurrentHP > 0 && !hasFlee)
            {
                --player.CurrentCooldown;
                --monster.CurrentCooldown;
                ManageEffectStatus(player);
                ManageEffectStatus(monster);
                DisplayFightInfos(monster);
                if (player.CurrentCooldown <= 0)
                {
                    hasFlee = selectAction(monster);
                    if (!hasFlee)
                    {
                        Console.WriteLine("Press any key");
                        ClearKeyBuffer();
                        Console.ReadKey(true);
                    }
                }
                if (monster.CurrentCooldown <= 0)
                {
                    Console.WriteLine(monster.Attack());
                    Console.WriteLine("Press any key");
                    ClearKeyBuffer();
                    Console.ReadKey(true);
                }
                Thread.Sleep(100);
            }
            if (player.CurrentHP > 0 && monster.CurrentHP <= 0)
            {
                var loots = monster.GetLoots();
                var money = monster.LootMoney();
                string loot = "";
                foreach (var item in loots)
                {
                    var StackInfo = ((item is IStackable) ? ((item as IStackable).Quantity + " ") : (""));
                    loot += "\t" + StackInfo + item.Name + " : " + item.Description + "\n";
                    player.AddItem(item);
                }
                if (money > 0)
                {
                    player.Money += money;
                    loot += "Money looted :" + ConvertMoney(money) + "\n";
                }
                Console.WriteLine("Well done " + player.Name + ", you raped " + monster.Name + ".\nYou have " + player.CurrentHP + "HP remaining.");
                if (loot != "")
                    Console.WriteLine("You've earned : \n" + loot);
            }
            else if (monster.CurrentHP > 0 && player.CurrentHP <= 0)
                Console.WriteLine("You really sux " + player.Name + ", you've been raped by " + monster.Name + ".\nIt has " + monster.CurrentHP + "HP remaining.");
            else if (monster.CurrentHP <= 0 && player.CurrentHP <= 0)
                Console.WriteLine("HAHA " + player.Name + " and " + monster.Name + " killed each other");
            else if (hasFlee)
                Console.WriteLine("You flee like a coward. You are a chicken.");
            else
                Console.WriteLine("If you see this message, both player and monster are alive and the program is buggy");
            Console.WriteLine("Press enter");
            Console.ReadLine();
        }

        static int Difficulty(Monster monster)
        {
            int difficulty = player.Level;
            ConsoleKeyInfo menu;
            do
            {
                Console.Clear();
                Stats();
                Console.WriteLine("Select your difficulty");
                Console.WriteLine("[E]asy. [N]normal [H]hard [B]ack");
                ClearKeyBuffer();
                menu = Console.ReadKey(true);
            } while (menu.Key != ConsoleKey.E
                     && menu.Key != ConsoleKey.N
                     && menu.Key != ConsoleKey.H
                     && menu.Key != ConsoleKey.B);

            switch (menu.Key)
            {
                case ConsoleKey.E:
                    monster.SetLevel((difficulty - 5) > 0 ? (difficulty - 5) : (1));
                    --monster.GivenExp;
                    Fight(monster);
                    ++monster.GivenExp;
                    break;

                case ConsoleKey.N:
                    monster.SetLevel(difficulty);
                    Fight(monster);
                    break;

                case ConsoleKey.H:
                    monster.SetLevel(difficulty + 5);
                    ++monster.GivenExp;
                    Fight(monster);
                    --monster.GivenExp;
                    break;

                default:
                    break;
            }

            return (difficulty);
        }

        static void selectMonster()
        {
            Monsters monsters;
            try
            {
                bool back = false;
                do
                {
                    string error = "";
                    monsters = new Monsters();
                    string input;
                    do
                    {
                        int i = 1;
                        string options = "";
                        foreach (var monster in monsters)
                            options += "[" + i++ + "] " + monster.Name + "\n";
                        if (i > 1)
                        {
                            Console.Clear();
                            Stats();
                            Console.WriteLine(options);
                            if (error != "")
                                Console.WriteLine(error);
                            Console.WriteLine("Please select a monster or go [[B]ack]");
                        }
                        else
                            Console.WriteLine("No monster. Please go [[B]ack]");
                        input = Console.ReadLine();
                    } while (!Regex.IsMatch(input, "^(([0-9]+)|(b(ack)?))$", RegexOptions.IgnoreCase));

                    if (int.TryParse(input, out int result))
                    {
                        if (result > 0 && result <= monsters.Count)
                        {
                            Difficulty(monsters[--result]);
                        }
                        else
                            error = "Invalid number.";
                    }
                    else
                        back = true;

                } while (!back && player.CurrentHP > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting the monster list : " + ex.Message);
            }
        }

        static volatile bool WakeUp = false;
        static void Rest()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            Task.Factory.StartNew(() =>
            {
                while (!ct.IsCancellationRequested && Console.ReadKey(true).Key != ConsoleKey.W) ;
                WakeUp = true;
            }, tokenSource2.Token);

            while (!WakeUp && player.HP > player.CurrentHP)
            {
                ManageEffectStatus(player);
                Console.Clear();
                Stats();
                Console.WriteLine("You are resting. " + (player.HP - player.CurrentHP) + " seconds left before being fully rested. [W]ake up?");
                Thread.Sleep(1000);
                ++player.CurrentHP;
            }
            WakeUp = false;
            tokenSource2.Cancel();
            tokenSource2.Dispose();
        }

        static void GetInventoryGears<T>(bool IsLeftHand)
        {
            string input;
            List<IEquipable> stuff;
            string error;
            bool back = false;
            error = "";
            do
            {
                do
                {
                    
                    int i = 1;
                    string options = "";
                    stuff = null;
                    stuff = new List<IEquipable>();

                    foreach (Item gearT in player.Inventory)
                        if (gearT.GetType() == typeof(T)
                            && gearT is IEquipable)
                        {
                            options += "[" + i++ + "] " + gearT.Name + " : " + gearT.Description + ((gearT is Armor) ? (" - " + ArmorDetail(gearT as Armor)) : ((gearT is Weapon) ? (WeaponDetail(gearT as Weapon)) : (""))) + "\n";
                            stuff.Add(gearT as IEquipable);
                        }
                    if (i > 1)
                    {
                        Console.Clear();
                        Stats();
                        Console.WriteLine(options);
                        if (error != "")
                            Console.WriteLine(error);
                        Console.WriteLine("Please select an item or go [[B]ack]");
                    }
                    else
                        Console.WriteLine("No item found for that slot. Please go [[B]ack]");
                    input = Console.ReadLine();
                    error = "";
                } while (!Regex.IsMatch(input, "^(([0-9]+)|(b(ack)?))$", RegexOptions.IgnoreCase));

                if (int.TryParse(input, out int result))
                {
                    if (result > 0 && result <= stuff.Count)
                    {
                        if (stuff[--result] is Weapon)
                            (stuff[result] as Weapon).IsTargetingRightHand = !IsLeftHand;
                        stuff[result].TakeOn(player);
                        back = true;
                    }
                    else
                        error = "Invalid number.";
                }
                else
                    back = true;
            } while (!back);
        }

        static void SeeSlot<T>(string slotName, IEquipable item)
        {
            ConsoleKeyInfo menu;
            do
            {
                Console.Clear();
                Stats();
                Console.WriteLine(slotName + " : " + ((item != null) ? ((item as Item).Name + " : " + (item as Item).Description + ((item is Armor) ? (" - " + ArmorDetail(item as Armor)) : ((item is Weapon) ? (WeaponDetail(item as Weapon)) : ("")))) : ("Nothing equiped")) + "\n");
                Console.WriteLine(((item != null) ? ("[R]emove. R[e]place") : ("[E]quip")) + ". [B]ack");
                ClearKeyBuffer();
                menu = Console.ReadKey(true);
            } while (menu.Key != ConsoleKey.R
                        && menu.Key != ConsoleKey.E
                        && menu.Key != ConsoleKey.B);

            switch (menu.Key)
            {
                case ConsoleKey.R:
                    if (item != null)
                        item.TakeOff(player);
                    break;

                case ConsoleKey.E:
                    bool IsLeftHand = (slotName == "Left hand");
                    GetInventoryGears<T>(IsLeftHand);
                    break;

                default:
                    break;
            }
        }

        static void EquipementMenu()
        {
            ConsoleKeyInfo menu;
            do
            {
                do
                {
                    Console.Clear();
                    Stats();
                    Console.WriteLine("[R]ight hand : " + ((player.RightHand != null) ? (player.RightHand.Name + ((player.RightHand is Weapon) ? (" : " + WeaponDetail(player.RightHand as Weapon)) : (""))) : ("Nothing equiped")) + "\n");
                    Console.WriteLine("[L]eft hand : " + ((player.LeftHand != null) ? (player.LeftHand.Name + ((player.LeftHand is Weapon) ? (" : " + WeaponDetail(player.LeftHand as Weapon)) : (""))) : ("Nothing equiped")) + "\n");
                    Console.WriteLine("[C]hest : " + ((player.ChestArmor != null) ? (player.ChestArmor.Name + " : " + player.ChestArmor.Description + " - " + ArmorDetail(player.ChestArmor)) : ("Nothing equiped")) + "\n");
                    Console.WriteLine("l[e]gs : " + ((player.LegsArmor != null) ? (player.LegsArmor.Name + " : " + player.LegsArmor.Description + " - " + ArmorDetail(player.LegsArmor)) : ("Nothing equiped")) + "\n");
                    Console.WriteLine("[A]rms : " + ((player.SleevesArmor != null) ? (player.SleevesArmor.Name + " : " + player.SleevesArmor.Description + " - " + ArmorDetail(player.SleevesArmor)) : ("Nothing equiped")) + "\n");
                    Console.WriteLine("[F]eet : " + ((player.FeetArmor != null) ? (player.FeetArmor.Name + " : " + player.FeetArmor.Description + " - " + ArmorDetail(player.FeetArmor)) : ("Nothing equiped")) + "\n");
                    Console.WriteLine("[H]and : " + ((player.HandsArmor != null) ? (player.HandsArmor.Name + " : " + player.HandsArmor.Description + " - " + ArmorDetail(player.HandsArmor)) : ("Nothing equiped")) + "\n");
                    Console.WriteLine("Hea[d] : " + ((player.HeadArmor != null) ? (player.HeadArmor.Name + " : " + player.HeadArmor.Description + " - " + ArmorDetail(player.HeadArmor)) : ("Nothing equiped")) + "\n");
                    Console.WriteLine("Select an equipement slot or go [B]ack");
                    ClearKeyBuffer();
                    menu = Console.ReadKey(true);
                } while (menu.Key != ConsoleKey.R
                         && menu.Key != ConsoleKey.C
                         && menu.Key != ConsoleKey.L
                         && menu.Key != ConsoleKey.A
                         && menu.Key != ConsoleKey.F
                         && menu.Key != ConsoleKey.H
                         && menu.Key != ConsoleKey.E
                         && menu.Key != ConsoleKey.D
                         && menu.Key != ConsoleKey.B);

                switch (menu.Key)
                {
                    case ConsoleKey.R:
                        SeeSlot<Weapon>("Right hand", player.RightHand);
                        break;

                    case ConsoleKey.L:
                        SeeSlot<Weapon>("Left hand", player.LeftHand);
                        break;

                    case ConsoleKey.C:
                        SeeSlot<ChestArmor>("Chest armor", player.ChestArmor);
                        break;

                    case ConsoleKey.E:
                        SeeSlot<LegsArmor>("Legs armor", player.LegsArmor);
                        break;

                    case ConsoleKey.A:
                        SeeSlot<SleevesArmor>("Arms armor", player.SleevesArmor);
                        break;

                    case ConsoleKey.F:
                        SeeSlot<FeetArmor>("Feet armor", player.FeetArmor);
                        break;

                    case ConsoleKey.H:
                        SeeSlot<HandsArmor>("Hands armor", player.HandsArmor);
                        break;

                    case ConsoleKey.D:
                        SeeSlot<HeadArmor>("Head armor", player.HeadArmor);
                        break;

                    default:
                        break;
                }

            } while (menu.Key != ConsoleKey.B);
        }

        static void SpecificInventoryItems<T>(string type) where T : class
        {
            
            string error = "";
            string input;
            List<T> items;
            bool back = false;

            do
            {
                do
                {
                    items = null;
                    items = new List<T>();
                    string options = type + " :\n";
                    int i = 1;
                    if (player.Inventory.Count > 0)
                    {
                        foreach (Item item in player.Inventory)
                        {
                            if (item is T)
                            {
                                string stack = "";
                                if (item is IStackable)
                                {
                                    var stackable = item as IStackable;
                                    stack = "(" + stackable.Quantity + "/" + stackable.MaxAmount + ") ";
                                }
                                options += "[" + i++ + "] - " + item.Name + " " + stack + ": " + item.Description;
                                if (item is Armor)
                                    options += " " + ArmorDetail(item as Armor);
                                else if (item is Weapon)
                                    options += " " + WeaponDetail(item as Weapon);
                                options += "\n";
                                items.Add(item as T);
                            }
                        }

                        Console.Clear();
                        Stats();

                        if (i > 1)
                        {
                            Console.WriteLine(options);
                            if (error != "")
                                Console.WriteLine(error);
                            Console.WriteLine("Please select an item or go [[B]ack]");
                        }
                        else
                            Console.WriteLine("No " + type.ToLower() + " found. Please go [[B]ack]");
                    }
                    else
                        Console.WriteLine("No item in inventory. Please go [[B]ack]");
                    input = Console.ReadLine();
                    error = "";

                } while (!Regex.IsMatch(input, "^(([0-9]+)|(b(ack)?))$", RegexOptions.IgnoreCase));

                if (int.TryParse(input, out int result))
                {
                    if (result > 0 && result <= items.Count)
                    {
                        var selected = items[--result];
                        switch (selected)
                        {
                            case IEquipable eq:
                                eq.TakeOn(player);
                                back = true;
                                break;

                            case IUsable us:
                                us.Use(player);
                                if (us is IStackable)
                                {
                                    var stackable = us as IStackable;
                                    if (stackable.Quantity <= 0)
                                        player.Inventory.Remove(stackable as Item);
                                }
                                else
                                    player.Inventory.Remove(us as Item);
                                back = true;
                                break;

                            default:
                                break;
                        }
                    }
                    else
                        error = "Invalid number.";
                }
                else
                    back = true;
            } while (!back);
        }

        static void InventoryMenu()
        {
            ConsoleKeyInfo menu;
            do
            {
                do
                {
                    string options = "Inventory :\n";
                    if (player.Inventory.Count > 0)
                    {
                        foreach (Item item in player.Inventory)
                        {
                            string stack = "";
                            if (item is IStackable)
                            {
                                var stackable = item as IStackable;
                                stack = "(" + stackable.Quantity + "/" + stackable.MaxAmount + ") ";
                            }
                            options += "\t - " + item.Name + " " + stack + ": " + item.Description + "\n";
                        }

                        Console.Clear();
                        Stats();
                        Console.WriteLine(options);
                        Console.WriteLine("[U]sables. [E]quipement. [B]ack");
                    }
                    else
                        Console.WriteLine("No item in inventory. Please go [B]ack]");
                    ClearKeyBuffer();
                    menu = Console.ReadKey(true);
                } while (menu.Key != ConsoleKey.U
                         && menu.Key != ConsoleKey.E
                         && menu.Key != ConsoleKey.B);

                if (player.Inventory.Count > 0)
                {
                    switch (menu.Key)
                    {
                        case ConsoleKey.U:
                            SpecificInventoryItems<IUsable>("Usable");
                            break;

                        case ConsoleKey.E:
                            SpecificInventoryItems<IEquipable>("Equipable");
                            break;

                        default:
                            break;
                    }
                }
            } while (menu.Key != ConsoleKey.B);
        }

        static void CharacterMenu()
        {
            ConsoleKeyInfo menu;
            do
            {
                do
                {
                    Console.Clear();
                    Stats();
                    DetailedStats();
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("[S]kills. [E]quipement. [I]nventory. [R]est. [B]ack");
                    ClearKeyBuffer();
                    menu = Console.ReadKey(true);
                } while (menu.Key != ConsoleKey.E
                         && menu.Key != ConsoleKey.S
                         && menu.Key != ConsoleKey.I
                         && menu.Key != ConsoleKey.R
                         && menu.Key != ConsoleKey.B);

                switch (menu.Key)
                {
                    case ConsoleKey.S:
                        SkillsMenu();
                        break;

                    case ConsoleKey.E:
                        EquipementMenu();
                        break;

                    case ConsoleKey.I:
                        InventoryMenu();
                        break;

                    case ConsoleKey.R:
                        if (player.HP <= player.CurrentHP)
                        {
                            Console.WriteLine("You can't rest, you are already full HP");
                            Console.WriteLine("Press enter");
                            Console.ReadLine();
                        }
                        else
                            Rest();
                        break;

                    default:
                        break;
                }
            } while (menu.Key != ConsoleKey.B);
        }

        static void Menu()
        {
            ConsoleKeyInfo menu;
            do
            {
                do
                {
                    Console.Clear();
                    Stats();
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("[A]ttack a monster. [C]haracter menu. [R]est. [S]ave game. [E]xit");
                    ClearKeyBuffer();
                    menu = Console.ReadKey(true);
                } while (menu.Key != ConsoleKey.A
                         && menu.Key != ConsoleKey.R
                         && menu.Key != ConsoleKey.C
                         && menu.Key != ConsoleKey.S
                         && menu.Key != ConsoleKey.E);

                switch (menu.Key)
                {
                    case ConsoleKey.A:
                        if (player.CurrentHP <= 0)
                        {
                            Console.WriteLine("You can't fight, you are K.O. Consider taking a rest");
                            Console.WriteLine("Press enter");
                            Console.ReadLine();
                        }
                        else
                            selectMonster();
                        break;

                    case ConsoleKey.R:
                        if (player.HP <= player.CurrentHP)
                        {
                            Console.WriteLine("You can't rest, you are already full HP");
                            Console.WriteLine("Press enter");
                            Console.ReadLine();
                        }
                        else
                            Rest();
                        break;

                    case ConsoleKey.C:
                        CharacterMenu();
                        break;

                    case ConsoleKey.S:
                        string result = player.Save();
                        if (result == "")
                        {
                            Console.WriteLine("Game Saved.");
                            Console.WriteLine("Press enter");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Error saving game : " + result);
                            Console.WriteLine("Press enter");
                            Console.ReadLine();
                        }
                        break;

                    default:
                        break;
                }
            } while (menu.Key != ConsoleKey.E);
        }

        static string getName()
        {
            string name = "";
            string pattern = "^[a-zA-Z]{3,}$";
            LoadManager loadManager = new LoadManager();

            while (!Regex.IsMatch(name, pattern))
            {
                Console.Clear();
                Console.WriteLine("Enter your name");
                name = Console.ReadLine();
                if (!loadManager.CheckName(name))
                {
                    name = "";
                    Console.WriteLine("Name already in saved games");
                    Console.WriteLine("Press enter");
                    Console.ReadLine();
                }
            }

            return (name);
        }

        static void Load()
        {
            string input;
            bool back = false;
            LoadManager loadManager;
            do
            {
                do
                {
                    loadManager = new LoadManager();
                    string gameList = loadManager.ShowSavedGames();
                    Console.Clear();
                    if (gameList != "")
                    {
                        Console.WriteLine(gameList);
                        Console.WriteLine("Select your save or go [[B]ack]");
                    }
                    else
                        Console.WriteLine("No saved game. Please go [[B]ack]");
                    input = Console.ReadLine();
                } while (!Regex.IsMatch(input, "^(([0-9]+)|(b(ack)?))$", RegexOptions.IgnoreCase));

                if (int.TryParse(input, out int result))
                {
                    player = loadManager.GetPlayer(result);
                    back = player != null;
                }
                else
                    back = true;
            } while (!back);
        }

        static bool welcome()
        {
            ConsoleKeyInfo menu;
            bool isExiting = false;
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to SomeRPG :)");
                    Console.WriteLine("[L]oad game. [N]ew game. [E]xit");
                    ClearKeyBuffer();
                    menu = Console.ReadKey(true);
                } while (menu.Key != ConsoleKey.L
                         && menu.Key != ConsoleKey.N
                         && menu.Key != ConsoleKey.E);

                switch (menu.Key)
                {
                    case ConsoleKey.L:
                        Load();
                        player.BuildSkillTree();
                        break;

                    case ConsoleKey.N:
                        player = new Player
                        {
                            Name = getName(),
                            BaseHP = 42,
                            BaseCooldown = 10,
                            BaseRightMinAttack = 5,
                            BaseRightMaxAttack = 10,
                            BaseLeftMinAttack = 0,
                            BaseLeftMaxAttack = 0,
                            BaseStrengh = 10,
                            BaseVitality = 10,
                            BaseAgility = 10,
                            BasePrecision = 10,
                            BaseDexterity = 10
                        };

                        player.BuildSkillTree();

                        try
                        {
                            player.Inventory.Add(new HPPotion("Lesser Healing Potion")
                            {
                                Quantity = 20
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    default:
                        isExiting = true;
                        break;
                }
            } while (player == null && menu.Key != ConsoleKey.E);
            return (isExiting);
        }

        static void Main(string[] args)
        {
            ConsoleKeyInfo startOVer;
            bool isExiting = false;
            do
            {
                if (!welcome())
                {
                    Menu();
                    do
                    {
                        Console.WriteLine("Game over. Exit? y/n");
                        ClearKeyBuffer();
                        startOVer = Console.ReadKey(true);
                    } while (startOVer.Key != ConsoleKey.Y
                             && startOVer.Key != ConsoleKey.N);
                    if (startOVer.Key == ConsoleKey.Y)
                        isExiting = true;
                }
                else
                    isExiting = true;
            } while (!isExiting);
        }
    }
}
