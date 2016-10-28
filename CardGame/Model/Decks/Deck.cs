using System;
using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Decks.Interfaces;
using CardGame.Model.Engine;

namespace CardGame.Model.Decks
{
    public class Deck : IDeck
    {
        public string Name;
        public List<ICard> Cards { get; }
        private int _topDeckIndex;
        
        public Deck()
        {
            Cards = new List<ICard>();
        }

        public Deck(List<ICard> cards)
        {
            Cards = cards;
            _topDeckIndex = cards.Count;
        }

        public void AddCard(ICard card)
        {
            Cards.Add(card);
        }
        public void AddCards(IList<ICard> cards)
        {
            Cards.AddRange(cards);
        }

        public void RemoveCard(ICard card)
        {
            Cards.Remove(card);
        }
        public void RemoveCard(int index)
        {
            Cards.RemoveAt(index);
        }

        public void Shuffle()
        {
            var n = Cards.Count;
            while (n > 1)
            {
                n--;
                var r = GameEngine.RngRandom.Next(n + 1);
                var temp = Cards[n];
                Cards[n] = Cards[r];
                Cards[r] = temp;
            }
        }

        public ICard DrawCard()
        {
            if (_topDeckIndex <= 0)
            {
                throw new Exception("Cannot draw anymore cards. Deck is empty");
            }

            _topDeckIndex--;
            var card = Cards[_topDeckIndex];
            RemoveCard(_topDeckIndex);
            return card;
        }

        public List<ICard> DrawCards(int count)
        {
            var cards = new List<ICard>();
            for (var i = 0; i < count; ++i)
            {
                cards.Add(DrawCard());
            }

            return cards;
        }
    }
}
