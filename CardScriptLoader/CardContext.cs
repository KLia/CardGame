using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;

namespace CardScriptLoader
{
    public class CardContext
    {
        public IList<ICard> Cards { get; private set; }

        public CardContext()
        {
            Cards = new List<ICard>();
        }

        public void AddCard(ICard card)
        {
            Cards.Add(card);
        }
    }
}
