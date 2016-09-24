using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Decks;

namespace CardGame.Model.Players.Interfaces
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Name { get; set; }
        int Mana { get; set; }
        IDeck Deck { get; set; }
        List<ICard> CardsInHand { get; set; }
        ICard DrawCard(bool isMulligan = false);
        List<ICard> DrawCards(int count, bool isMulligan = false);
    }
}
