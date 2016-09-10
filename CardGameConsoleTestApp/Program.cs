using System;
using CardGameConsoleTestApp.Model;
using static CardGameConsoleTestApp.Controller.CardController;

namespace CardGameConsoleTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var minions = GetInstance().GetAllMinionsList(1);

            foreach (var m in minions)
            {
                Console.WriteLine($"Name: {m.Name}; Cost: {m.Cost}; Attack: {m.Attack}; Health: {m.Health}");
            }
   
            //var result = DelegateFactory.GetDelegate("TriggersController", "Heal");
            //minions[0].GetHit += (s,e) => DelegateFactory.RunMethod("TriggersController", "Heal", new object[] {minions[1], 2});

            Console.WriteLine(minions[0].CurrentHealth);
            minions[0].CurrentHealth -= 2;
            Console.WriteLine(minions[0].CurrentHealth);
            minions[0].CurrentHealth += 2;
            Console.WriteLine(minions[0].CurrentHealth);

            //minions[1].Healed += (s, e) => DelegateFactory.RunMethod("TriggersController", "Heal", new object[] {minions[0], 1});
            minions[1].CurrentHealth += 2;

            Console.ReadKey();
        }
    }
}
