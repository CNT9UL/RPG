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
                Console.WriteLine("Press any key (except the power button) to know how many damage did your Hero taken and dealt!");
                Console.ReadLine();

                Combatant hero = new Combatant(3000, 0);
                Combatant monster = new Combatant(2000, 0);
                int roundCount = 0;

                using (StreamReader sr = new StreamReader("attack_log.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(' ');
                        if (parts.Length == 2)
                        {
                            if (parts[0] == "monster")
                            {
                                monster.Attack(hero,Int32.Parse(parts[1]));
                                Console.WriteLine($"Attacker: {parts[0]} // Damage: {parts[1]}");

                            }
                            else if (parts[0] == "hero")
                            {
                                hero.Attack(monster, Int32.Parse(parts[1]));
                                Console.WriteLine($"Defender: {parts[0]} // Damage: {parts[1]}");
                            }
                            roundCount++;
                        }
                    }
                }

                Console.WriteLine("Total damage received: " + hero.damageReceived);
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

            public Combatant(int health, int armour)
            {
                this.damageReceived = 0;
                this.damageDealt = 0;
                this.health = health;
                this.armour = armour;
            }

            public void Attack(Combatant attackedCombatant, int damage)
            {
               damageDealt += attackedCombatant.Defend(damage);
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
    }
}
