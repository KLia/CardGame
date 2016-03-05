using System;
using CardGameConsoleTestApp.Controller;

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

            Console.ReadKey();
        }
    }
}
