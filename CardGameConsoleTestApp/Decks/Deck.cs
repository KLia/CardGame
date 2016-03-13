using System.Collections.Generic;
using CardGameConsoleTestApp.Cards;

namespace CardGameConsoleTestApp.Decks
{
    public class Deck
    {
        private IList<Card> Cards;
        public string Name;

        public Deck()
        {
            Cards = new List<Card>();
        }

        public Deck(IList<Card> cards)
        {
            Cards = cards;
        }

    }
}
