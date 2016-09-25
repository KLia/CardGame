using CardGame.Model.Engine.Interfaces;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Engine
{
    public class GameState : IGameState
    {
        public GameState(IGameBoard board, IPlayer player1, IPlayer player2, IPlayer current)
        {
            this.Board = board ?? new GameBoard();
            this.Player = player1;
            this.Opponent = player2;
            this.CurrentPlayer = current;
            this.Turn = 0;
        }


        public IGameBoard Board { get; set; }
        public IPlayer Player { get; set; }
        public IPlayer Opponent { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public int Turn { get; set; }
    }
}
