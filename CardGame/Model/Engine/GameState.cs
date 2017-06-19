using CardGame.Model.Engine.Interfaces;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Engine
{
    public class GameState : IGameState
    {
        public GameState(IPlayer player1, IPlayer player2, IPlayer current, IGameBoard gameBoard)
        {
            this.Player = player1;
            this.Opponent = player2;
            this.CurrentPlayer = current;
            this.Turn = 0;
            this.GameBoard = gameBoard;
        }

       
        public IPlayer Player { get; set; }
        public IPlayer Opponent { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public int Turn { get; private set; }
        public IGameBoard GameBoard { get; private set; }

        public void IncrementTurn()
        {
            Turn++;
        }

        public void SwapCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player ? Opponent : Player;
        }
    }
}
