using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Decks;
using CardGame.Model.Engine;
using CardGame.Model.Players;
using CardGameConsoleApp.Delegates;

namespace CardGameConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //===Initialize============================//
                //CardController.GetInstance().GetAllMinionsList(1);
            var deck1 = new Deck();
            var deck2 = new Deck();
            var p1 = new Player(1, "P1", /*GameConstants.STARTING_MANA*/10, deck1);
            var p2 = new Player(2, "P2", /*GameConstants.STARTING_MANA*/10, deck2);
            
            GameEngine.Initialize(p1, p2, new GameState(p1, p2, p1, new GameBoard()));
            var minions = new List<Minion> { new SampleMinion(), new SampleMinion() };

            deck1.AddCards(new List<ICard>(minions));
            deck2.AddCards(new List<ICard>(minions));
            minions[0].PlayerOwner = p1;
            minions[1].PlayerOwner = p2;
            p1.CardsInHand.Add(minions[0]);
            p2.CardsInHand.Add(minions[1]);

            //invoke script loaderz
            var filenames =
                Directory.GetFiles("C:\\Users\\keith\\Documents\\GitHub\\CardGame\\CardGameConsoleApp\\Cards\\Minions");
            p1.Deck.AddCards(CardScriptLoader.CardScriptLoader.GetCards(filenames));

            Console.WriteLine($"Cards in P1's deck: {p1.Deck.Cards.Count}");
            Console.WriteLine($"Cards in P2's deck: {p2.Deck.Cards.Count}");


            //=========================================//

            foreach (var m in minions)
            {
                Console.WriteLine(
                    $"Name: {m.Name}; BaseManaCost: {m.BaseManaCost}; BaseAttack: {m.BaseAttack}; BaseHealth: {m.BaseHealth}");
            }

            //===Game Events===========================//
            var fullyQualifiedName = ConfigurationManager.AppSettings["TriggerNamespace"];
            const string className = "TriggersController";

            GameEventManager.RegisterForEventTurnStart(minions[0],
                (player) =>
                    DelegateFactory.RunMethod(fullyQualifiedName, className, "Heal",
                        new object[] {minions[0], 2}));

            GameEventManager.RegisterForEventDeath(minions[0],
                (target) => { Console.WriteLine($"------------- {((ICard) target).Id} is DEAD"); });

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

            minions[0].PlayCard(0);
            minions[1].PlayCard(0);
            Console.WriteLine($"Minion {minions[0].Id} BaseHealth: {minions[0].CurrentHealth}");
            Console.WriteLine($"Minion {minions[1].Id} BaseHealth: {minions[1].CurrentHealth}");
            Console.WriteLine($"Minion {minions[0].Id} taking 2 damage");
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