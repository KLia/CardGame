﻿using System;
using System.Collections.Generic;
using CardGame.Controller;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Decks;
using CardGame.Model.Engine;
using CardGame.Model.Players;

namespace CardGameConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            //===Initialize============================//
            var minions = CardController.GetInstance().GetAllMinionsList(1);
            var deck1 = new Deck(new List<ICard>(minions));
            var deck2 = new Deck(new List<ICard>(minions));
            var p1 = new Player(1, "P1", GameConstants.STARTING_MANA, deck1);
            var p2 = new Player(2, "P2", GameConstants.STARTING_MANA, deck2);

            var gameEngine = new GameEngine(null, p1, p2, p1);
            //=========================================//

            foreach (var m in minions)
            {
                Console.WriteLine($"Name: {m.Name}; Cost: {m.Cost}; Attack: {m.Attack}; Health: {m.Health}");
            }

            GameEventManager.RegisterForEventTurnStart(minions[0], () => DelegateFactory.RunMethod("TriggersController", "Heal", new object[] { minions[0], 2 }));
            GameEventManager.RegisterForEventTurnStart(minions[1], () => DelegateFactory.RunMethod("TriggersController", "Heal", new object[] { minions[1], 5 }));
            GameEventManager.RegisterForEventTurnEnd(minions[0], () => DelegateFactory.RunMethod("TriggersController", "DealDamage", new object[] { minions[0], 12 }));
            GameEventManager.RegisterForEventCardDrawn(minions[1], (card) => DelegateFactory.RunMethod("TriggersController", "Heal", new object[] { card, 1 }));

            //var result = DelegateFactory.GetDelegate("TriggersController", "Heal");
            //minions[0].GetHit += (s,e) => DelegateFactory.RunMethod("TriggersController", "Heal", new object[] {minions[1], 2});

            Console.WriteLine($"Minion 0 Health: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion 1 Health: {minions[1].CurrentHealth}");
            minions[0].CurrentHealth -= 2;
            Console.WriteLine($"Minion 0 Health: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion 1 Health: {minions[1].CurrentHealth}");
            minions[0].CurrentHealth += 2;
            Console.WriteLine($"Minion 0 Health: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion 1 Health: {minions[1].CurrentHealth}");
            gameEngine.StartTurn();
            Console.WriteLine($"Minion 0 Health: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion 1 Health: {minions[1].CurrentHealth}");
            gameEngine.EndTurn();
            Console.WriteLine($"Minion 0 Health: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion 1 Health: {minions[1].CurrentHealth}");
            p1.DrawCards(2);
            Console.WriteLine($"Minion 0 Health: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion 1 Health: {minions[1].CurrentHealth}");


            //minions[1].Healed += (s, e) => DelegateFactory.RunMethod("TriggersController", "Heal", new object[] {minions[0], 1});

            Console.ReadKey();
        }
    }
}