using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Engine.Interfaces
{
    public interface IGameBoard
    {
        List<ICard> PlayerBoardCards { get; set; }
        List<ICard> OpponentBoardCards { get; set; }
        List<ICard> GetPlayerBoardCards(IPlayer player);
    }
}
