using System.Collections.Generic;
using CardGameConsoleTestApp.Model.Cards.Interfaces;

namespace CardGameConsoleTestApp.Model.Decks
{
    public interface IDeck
    {
        void AddCard (ICard card);
        void RemoveCard(ICard card);
        void RemoveCard(int index);
        void Shuffle();
        ICard DrawCard();
        List<ICard> DrawCards(int count);
    }
}
