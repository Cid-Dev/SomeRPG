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
        static void Main(string[] args)
        {
            ConsoleKeyInfo startOVer;
            do
            {
                Character player = new Character
                {
                    Name = "Cid",
                    BaseHP = 42,
                    BaseCooldown = 10,
                    BaseMinAttack = 5,
                    BaseMaxAttack = 10
                };

                List<Character> monsters = new List<Character>();
                for (int i = 0; i < 10; ++i)
                {
                    monsters.Add(new Character
                    {
                        Name = "Noob" + i,
                        BaseHP = 20,
                        BaseCooldown = 11,
                        BaseMinAttack = 2,
                        BaseMaxAttack = 5
                    });
                }

                while (player.CurrentHP > 0 && monsters.Count > 0)
                {
                    Console.WriteLine(player.Stats());
                    var monster = monsters[0];
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
                    {
                        Console.WriteLine("Well done " + player.Name + ", you raped " + monster.Name + ".\nYou have " + player.CurrentHP + "HP remaining.");
                        monsters.RemoveAt(0);
                    }
                    else if (monster.CurrentHP > 0 && player.CurrentHP <= 0)
                        Console.WriteLine("You really sux " + player.Name + ", you've been raped by " + monster.Name + ".\nIt has " + monster.CurrentHP + "HP remaining.");
                    else if (monster.CurrentHP <= 0 && player.CurrentHP <= 0)
                        Console.WriteLine("HAHA " + player.Name + " and " + monster.Name + " killed each other");
                    else
                        Console.WriteLine("If you see this message, both player and monster are alive and the program is buggy");
                    Console.WriteLine("Press enter");
                    Console.ReadLine();
                }
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
