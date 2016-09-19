using System.Collections.Generic;
using CardGameBackend.Model.Cards.Interfaces;

namespace CardGameBackend.Model.Engine.Interfaces
{
    public interface IGameBoard
    {
        List<ICard> Player1PlayZone { get; set; }
        List<ICard> Player2PlayZone { get; set; }
    }
}
