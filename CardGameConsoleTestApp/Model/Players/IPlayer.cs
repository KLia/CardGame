using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameConsoleTestApp.Model.Cards.Interfaces;
using CardGameConsoleTestApp.Model.Decks;

namespace CardGameConsoleTestApp.Model.Players
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Name { get; set; }
        IDeck Deck { get; set; }
        List<ICard> CardsInHand { get; set; }
        List<ICard> CardsInGraveyard { get; set; }
        ICard DrawCard(bool isMulligan = false);
        List<ICard> DrawCards(int count, bool isMulligan = false);
    }
}
