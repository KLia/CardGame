using System;
using System.Collections.Generic;
using System.Configuration;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Decks;
using CardGame.Model.Engine;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players;
using CardGameConsoleApp.Controller;
using CardGameConsoleApp.Delegates;

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
            var p1 = new Player(1, "P1", /*GameConstants.STARTING_MANA*/10, deck1);
            var p2 = new Player(2, "P2", /*GameConstants.STARTING_MANA*/10, deck2);

            GameEngine.Initialize(p1, p2, p1);
            p1.CardsInHand.Add(minions[0]);
            p2.CardsInHand.Add(minions[1]);
            //=========================================//

            foreach (var m in minions)
            {
                Console.WriteLine($"Name: {m.Name}; BaseCost: {m.BaseCost}; BaseAttack: {m.BaseAttack}; BaseHealth: {m.BaseHealth}");
            }

            //===Game Events===========================//
            var fullyQualifiedName = ConfigurationManager.AppSettings["TriggerNamespace"];
            const string className = "TriggersController";

            GameEventManager.RegisterForEventTurnStart(minions[0],
                (player) =>
                    DelegateFactory.RunMethod(fullyQualifiedName, className, "Heal",
                        new object[] {minions[0], 2}));

            GameEventManager.RegisterForEventDeath(minions[0], (target) => { Console.WriteLine($"------------- {((ICard)target).Id} is DEAD"); });

            GameEventManager.RegisterForEventTurnStart(minions[1],
                (player) =>
                    DelegateFactory.RunMethod(fullyQualifiedName, className, "Heal",
                        new object[] {minions[1], 5}));

            GameEventManager.RegisterForEventTurnEnd(minions[0],
                (player) =>
                    DelegateFactory.RunMethod(fullyQualifiedName, className, "DealDamage",
                        new object[] {minions[0], 12}));
            GameEventManager.RegisterForEventCardDrawn(minions[1],
                (card) =>
                    DelegateFactory.RunMethod(fullyQualifiedName, className, "Heal",
                        new object[] {card, 1}));
            //=========================================//

            p1.PlayCard(minions[0], 0);
            p2.PlayCard(minions[1], 0);
            Console.WriteLine($"Minion {minions[0].Id} BaseHealth: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion {minions[1].Id} BaseHealth: {minions[1].CurrentHealth}");
            minions[0].TakeDamage(2);
            Console.WriteLine($"Minion {minions[0].Id} BaseHealth: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion {minions[1].Id} BaseHealth: {minions[1].CurrentHealth}");
            minions[0].CurrentHealth += 2;
            Console.WriteLine($"Minion {minions[0].Id} BaseHealth: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion {minions[1].Id} BaseHealth: {minions[1].CurrentHealth}");
            GameEngine.StartTurn();
            Console.WriteLine($"Minion {minions[0].Id} BaseHealth: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion {minions[1].Id} BaseHealth: {minions[1].CurrentHealth}");
            GameEngine.EndTurn();
            Console.WriteLine($"Minion {minions[0].Id} BaseHealth: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion {minions[1].Id} BaseHealth: {minions[1].CurrentHealth}");
            try
            {
                p1.DrawCards(2);
            }
            catch
            {
                
            }
            Console.WriteLine($"Minion {minions[0].Id} BaseHealth: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion {minions[1].Id} BaseHealth: {minions[1].CurrentHealth}");
            
            Console.ReadKey();
        }
    }
}