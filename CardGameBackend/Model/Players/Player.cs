using System;
using System.Collections.Generic;
using CardGameBackend.Model.Cards.Interfaces;
using CardGameBackend.Model.Decks;
using CardGameBackend.Model.Engine;

namespace CardGameBackend.Model.Players
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Mana { get; set; }
        public IDeck Deck { get; set; }

        public List<ICard> CardsInHand { get; set; }

        public Player(int id, string name, int mana, IDeck deck)
        {
            Id = id;
            Name = name;
            Mana = mana;
            Deck = deck;
            CardsInHand = new List<ICard>();
        }

        public ICard DrawCard(bool isMulligan = false)
        {
            //todo = handle case of no more cards to draw
            var card = Deck.DrawCard();
            CardsInHand.Add(card);

            if (!isMulligan)
            {
                GameEventManager.OnCardDrawn(card);
            }

            return card;
        }

        public List<ICard> DrawCards(int count, bool isMulligan = false)
        {
            //todo = handle case of no more cards to draw
            var cards = Deck.DrawCards(count);
            CardsInHand.AddRange(cards);

            if (!isMulligan)
            {
                foreach (var card in cards)
                {
                    GameEventManager.CardDrawn(card);
                }
            }

            return cards;
        }
    }
}
