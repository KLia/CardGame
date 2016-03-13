using System;
using System.Net.Http.Headers;
using CardGameConsoleTestApp.Controller;
using CardGameConsoleTestApp.Model;
using CardGameConsoleTestApp.Triggers;

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


            var result = DelegateFactory.RunMethod("Triggers", "Heal");
            minions[0].GetHit += (s,e) => DelegateFactory.RunMethod("Triggers", "Heal").DynamicInvoke(minions[0], 28);

            minions[0].CurrentHealth -= 2;
            Console.WriteLine(minions[0].CurrentHealth);
            minions[0].CurrentHealth += 2;
            Console.WriteLine(minions[0].CurrentHealth);

            CardController.GetInstance().LoadMinion(1);

            Console.ReadKey();
        }
    }
}
