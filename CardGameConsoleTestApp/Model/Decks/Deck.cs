using System.Collections.Generic;
using CardGameConsoleTestApp.Model.Cards;

namespace CardGameConsoleTestApp.Model.Decks
{
    public class Deck
    {
        private readonly IList<Card> _cards;

        public string Name;
        public IList<Card> Cards => _cards;

        public Deck()
        {
            _cards = new List<Card>();
        }

        public Deck(IList<Card> cards)
        {
            _cards = cards;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
        }
    }
}
