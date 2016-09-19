using System;
using System.Collections.Generic;
using CardGameConsoleTestApp.Model.Cards.Interfaces;
using CardGameConsoleTestApp.Model.Decks;
using CardGameConsoleTestApp.Model.Engine;

namespace CardGameConsoleTestApp.Model.Players
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IDeck Deck { get; set; }

        public List<ICard> CardsInHand { get; set; }
        public List<ICard> CardsInGraveyard { get; set; }

        public Player(int id, string name, IDeck deck)
        {
            Id = id;
            Name = name;
            Deck = deck;
            CardsInHand = new List<ICard>();
            CardsInGraveyard = new List<ICard>();
        }

        public ICard DrawCard(bool isMulligan = false)
        {
            //todo = handle case of no more cards to draw
            var card = Deck.DrawCard();
            CardsInHand.Add(card);

            if (!isMulligan)
            {
                GameEventManager.OnCardDrawn(this, card);
            }

            return card;
        }

        public List<ICard> DrawCards(int count, bool isMulligan=false)
        {
            //todo = handle case of no more cards to draw
            var cards = Deck.DrawCards(count);
            CardsInHand.AddRange(cards);

            if (!isMulligan)
            {
                foreach (var card in cards)
                {
                    GameEventManager.CardDrawn(this, card);
                }
            }

            return cards;
        }
    }
}
