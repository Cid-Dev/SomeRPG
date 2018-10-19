using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace SomeRPG
{
    class Program
    {
        static Character player;

        static void Fight(Character monster)
        {
            Console.Clear();
            Console.WriteLine(player.Stats());
            Console.WriteLine("You are fighting a monster !");
            Console.WriteLine(monster.Stats());
            player.Target = monster;
            monster.Target = player;
            while (player.CurrentHP > 0 && monster.CurrentHP > 0)
            {
                --player.CurrentCooldown;
                --monster.CurrentCooldown;
                if (player.CurrentCooldown <= 0)
                    Console.WriteLine(player.Attack());
                if (monster.CurrentCooldown <= 0)
                    Console.WriteLine(monster.Attack());
            }
            if (player.CurrentHP > 0 && monster.CurrentHP <= 0)
                Console.WriteLine("Well done " + player.Name + ", you raped " + monster.Name + ".\nYou have " + player.CurrentHP + "HP remaining.");
            else if (monster.CurrentHP > 0 && player.CurrentHP <= 0)
                Console.WriteLine("You really sux " + player.Name + ", you've been raped by " + monster.Name + ".\nIt has " + monster.CurrentHP + "HP remaining.");
            else if (monster.CurrentHP <= 0 && player.CurrentHP <= 0)
                Console.WriteLine("HAHA " + player.Name + " and " + monster.Name + " killed each other");
            else
                Console.WriteLine("If you see this message, both player and monster are alive and the program is buggy");
            Console.WriteLine("Press enter");
            Console.ReadLine();
        }

        static int Difficulty(Character monster)
        {
            int difficulty = player.Level;
            ConsoleKeyInfo menu;
            do
            {
                Console.Clear();
                Console.WriteLine(player.Stats());
                Console.WriteLine("Select your difficulty");
                Console.WriteLine("[E]asy. [N]normal [H]hard [B]ack");
                menu = Console.ReadKey();
            } while (menu.Key != ConsoleKey.E
                     && menu.Key != ConsoleKey.N
                     && menu.Key != ConsoleKey.H
                     && menu.Key != ConsoleKey.B);

            switch (menu.Key)
            {
                case ConsoleKey.E:
                    monster.Level = ((difficulty - 5) > 0 ? (difficulty - 5) : (1));
                    ++monster.GivenExp;
                    Fight(monster);
                    break;

                case ConsoleKey.N:
                    monster.Level = difficulty;
                    Fight(monster);
                    break;

                case ConsoleKey.H:
                    monster.Level = difficulty + 5;
                    --monster.GivenExp;
                    Fight(monster);
                    break;

                default:
                    break;
            }

            return (difficulty);
        }

        static void selectMonster()
        {
            ConsoleKeyInfo menu;
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(player.Stats());
                    Console.WriteLine("Select your monster");
                    Console.WriteLine("[N]oob (easy). [R]egular (normal) [V]eteran (hard) [B]ack");
                    menu = Console.ReadKey();
                } while (menu.Key != ConsoleKey.N
                         && menu.Key != ConsoleKey.R
                         && menu.Key != ConsoleKey.V
                         && menu.Key != ConsoleKey.B);

                Character noob = new Character
                {
                    Name = "Noob",
                    BaseHP = 20,
                    BaseCooldown = 15,
                    BaseMinAttack = 2,
                    BaseMaxAttack = 5,
                    GivenExp = 5
                };

                Character regular = new Character
                {
                    Name = "Regular",
                    BaseHP = 40,
                    BaseCooldown = 12,
                    BaseMinAttack = 4,
                    BaseMaxAttack = 8
                };

                Character veteran = new Character
                {
                    Name = "Veteran",
                    BaseHP = 50,
                    BaseCooldown = 10,
                    BaseMinAttack = 6,
                    BaseMaxAttack = 11,
                    GivenExp = 3
                };

                switch (menu.Key)
                {
                    case ConsoleKey.N:
                        Difficulty(noob);
                        break;

                    case ConsoleKey.R:
                        Difficulty(regular);
                        break;

                    case ConsoleKey.V:
                        Difficulty(veteran);
                        break;

                    default:
                        break;
                }
            } while (menu.Key != ConsoleKey.B && player.CurrentHP > 0);
        }

        static void Rest()
        {
            while (player.BaseHP > player.CurrentHP)
            {
                Console.Clear();
                Console.WriteLine(player.Stats());
                Console.WriteLine("You are resting. " + (player.BaseHP - player.CurrentHP) + " seconds left before being fully rested.");
                System.Threading.Thread.Sleep(1000);
                ++player.CurrentHP;
            }
        }

        static void Menu()
        {
            ConsoleKeyInfo menu;
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(player.Stats());
                    Console.WriteLine("What do you want to do?");
                    Console.WriteLine("[A]ttack a monster. [R]est. [E]xit");
                    menu = Console.ReadKey();
                } while (menu.Key != ConsoleKey.A
                         && menu.Key != ConsoleKey.R
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
                        if (player.BaseHP <= player.CurrentHP)
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
            } while (menu.Key != ConsoleKey.E);
        }

        static void Main(string[] args)
        {
            ConsoleKeyInfo startOVer;
            do
            {
                player = new Player
                {
                    Name = "Cid",
                    BaseHP = 42,
                    BaseCooldown = 10,
                    BaseMinAttack = 5,
                    BaseMaxAttack = 10
                };

                Menu();

                do
                {
                    Console.WriteLine("Game over. Start over? y/n");
                    startOVer = Console.ReadKey();
                } while (startOVer.Key != ConsoleKey.Y
                         && startOVer.Key != ConsoleKey.N);
            } while (startOVer.Key == ConsoleKey.Y);
        }
    }
}
