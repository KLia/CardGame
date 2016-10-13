using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Decks;
using CardGame.Model.Decks.Interfaces;
using CardGame.Model.Engine;

namespace CardGame.Model.Players.Interfaces
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Name { get; set; }
        int Mana { get; set; }
        IDeck Deck { get; set; }
        List<ICard> CardsInHand { get; set; }
        List<ICard> CardsInPlay { get; set; }
        List<ICard> CardsInGraveyard { get; set; }
        ICard DrawCard(bool isMulligan = false);
        List<ICard> DrawCards(int count, bool isMulligan = false);
        void PlayCard(ICard card, int boardPos, IDamageable target = null);
    }
}
