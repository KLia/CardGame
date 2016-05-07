using System;
using CardGameConsoleTestApp.Controller;
using CardGameConsoleTestApp.Model;

namespace CardGameConsoleTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var minions = CardController.GetInstance().GetAllMinionsList();

            foreach (var m in minions)
            {
                Console.WriteLine($"Name: {m.Name}; Cost: {m.Cost}; Attack: {m.Attack}; Health: {m.Health}");
            }


            //var result = DelegateFactory.GetDelegate("Triggers", "Heal");
            minions[0].GetHit += (s,e) => DelegateFactory.RunMethod("Triggers", "Heal", new object[] {minions[1], 2});

            Console.WriteLine(minions[0].CurrentHealth);
            minions[0].CurrentHealth -= 2;
            Console.WriteLine(minions[0].CurrentHealth);
            minions[0].CurrentHealth += 2;
            Console.WriteLine(minions[0].CurrentHealth);

            minions[1].Healed += (s, e) => DelegateFactory.RunMethod("Triggers", "Heal", new object[] {minions[0], 1});
            minions[1].CurrentHealth += 2;

            Console.ReadKey();
        }
    }
}
