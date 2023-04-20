using System;
using System.IO;
using System.Threading;

namespace RPGDemage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Hello World Hero!");
                Console.WriteLine("Press any key (except the power button) to play!");
                Console.ReadLine();
                Console.Clear();

                Weapon stick = new Weapon("Holy Wooden Stick", 100, 120);
                Weapon sword = new Weapon("Despicable Longsword", 80, 90);
                Combatant hero = new Combatant(3000, 30); hero.weapon = stick;
                Combatant monster = new Combatant(2000, 50); monster.weapon = sword;
                
                int roundCount = 0;

                {
                    while (hero.health > 0 && monster.health  > 0)
                    {
                        if (roundCount%2 == 1)
                        {
                            Console.WriteLine("Monster's turn!");
                            Console.WriteLine("Press any key (except the power button) to attack!");
                            Console.ReadLine();
                            Console.Clear();

                            int dmg = monster.Attack(hero);
                            Console.WriteLine($"Attacker: Monster // {monster.weapon.name} made {dmg} damage!");

                        }
                        else
                        {
                            Console.WriteLine("Hero's turn!");
                            Console.WriteLine("Press any key (except the power button) to attack!");
                            Console.ReadLine();
                            Console.Clear();

                            int dmg = hero.Attack(monster);
                            Console.WriteLine($"Attacker: Hero // {hero.weapon.name} made {dmg} damage!");
                        }
                        Console.WriteLine($"\nHero's health: {hero.health}");
                        Console.WriteLine($"Monster's health: {monster.health}\n");
                        roundCount++;
                    }
                    if (hero.health < 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Hero, you are dead! Shame on you!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Oh my God, you killed Monster! You bastard!");
                    }
                }

                Console.WriteLine("Hero:\nTotal damage received: " + hero.damageReceived);
                Console.WriteLine("Total damage dealt: " + hero.damageDealt);
                Console.WriteLine($"In {roundCount} rounds");
                Console.WriteLine("Press any key (except the power button) to continue!");
                Console.ReadLine();
                Console.Clear();
            }
        }


        public class Combatant
        {
            public int damageReceived;
            public int damageDealt;
            public int health;
            public int armour;
            public Weapon weapon;

            public Combatant(int health, int armour)
            {
                this.damageReceived = 0;
                this.damageDealt = 0;
                this.health = health;
                this.armour = armour;
            }

            public int Attack(Combatant attackedCombatant)
            {
                int dmg = attackedCombatant.Defend(weapon.damage);
                damageDealt += dmg;
                return dmg;
            }

            public int Defend(int damage)
            {
                int realDamage = damage - armour;
                if (realDamage > 0)
                {
                    health -= realDamage;
                    damageReceived += realDamage;
                    return realDamage;
                }
                else
                {
                    return 0;
                }

            }

        }

        public class Weapon
        {
            public string name;
            int minDamage;
            int maxDamage;
            public int damage => CalculateDamage();
            public Weapon(string name, int minDamage, int maxDamage)
            {
                this.name = name;
                this.minDamage = minDamage;
                this.maxDamage = maxDamage;
            }

            private int CalculateDamage()
            {
                Random rnd = new Random();
                return rnd.Next(minDamage, maxDamage + 1);
            }
           
        }
    }
}
