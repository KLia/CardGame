using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;

namespace CardGame.Model.Engine.Interfaces
{
    public interface IGameBoard
    {
        List<ICard> Player1PlayZone { get; set; }
        List<ICard> Player2PlayZone { get; set; }
        List<ICard> Player1Graveyard { get; set; }
        List<ICard> Player2Graveyard { get; set; }
    }
}
