using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Engine.Interfaces;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Engine
{
    public class GameBoard : IGameBoard
    {
        public GameBoard()
        {
            PlayerBoardCards = new List<ICard>(GameConstants.MAX_CARDS_IN_PLAY);

            OpponentBoardCards = new List<ICard>(GameConstants.MAX_CARDS_IN_PLAY);
        }

        public List<ICard> PlayerBoardCards { get; set; }
        public List<ICard> OpponentBoardCards { get; set; }

        public List<ICard> GetPlayerBoardCards(IPlayer player)
        {
            return GameBoardZone.Board.GetPlayerBoardZone(player);
        }
    }
}
