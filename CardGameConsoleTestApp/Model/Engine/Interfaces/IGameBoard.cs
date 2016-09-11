using System.Collections.Generic;
using CardGameConsoleTestApp.Model.Cards.Interfaces;

namespace CardGameConsoleTestApp.Model.Engine.Interfaces
{
    public interface IGameBoard
    {
        List<ICard> Player1PlayZone { get; set; }
        List<ICard> Player2PlayZone { get; set; }
    }
}
