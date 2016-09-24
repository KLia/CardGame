using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Engine.Interfaces;

namespace CardGame.Model.Engine
{
    public class GameBoard : IGameBoard
    {
        public GameBoard()
        {
            Player1PlayZone = new List<ICard>(GameConstants.MAX_CARDS_ON_BOARD);

            Player2PlayZone = new List<ICard>(GameConstants.MAX_CARDS_ON_BOARD);
        }

        public List<ICard> Player1PlayZone { get; set; }
        public List<ICard> Player2PlayZone { get; set; }

        public List<ICard> Player1Graveyard { get; set; }
        public List<ICard> Player2Graveyard { get; set; }
    }
}
