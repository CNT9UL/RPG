using System;
using System.IO;

namespace RPGDemage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Start:
            Console.WriteLine("Hello World Hero!");
            Console.WriteLine("Press any key (except the power button) to know how many damage did your Hero taken and dealt!");
            Console.ReadLine();

            int damageReceived = 0;
            int damageDealt = 0;
            int roundCount = 0;

            using (StreamReader sr = new StreamReader("attack_log.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length == 2)
                    {
                        if (parts[0] == "get")
                        {
                            damageReceived += Int32.Parse(parts[1]);
                        }
                        else if (parts[0] == "give")
                        {
                            damageDealt += Int32.Parse(parts[1]);
                        }
                        roundCount++;
                    }
                }
            }

            Console.WriteLine("Total damage received: " + damageReceived);
            Console.WriteLine("Total damage dealt: " + damageDealt);
            Console.WriteLine($"In {roundCount} rounds");
            Console.WriteLine("Press any key (except the power button) to continue!");
            Console.ReadLine();
            Console.Clear();
            goto Start;
        }
    }
}
