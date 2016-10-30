using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Engine.Interfaces
{
    public interface IGameState
    {IPlayer Player { get; set; }
        IPlayer Opponent { get; set; }
        IPlayer CurrentPlayer { get; set; }
        int Turn { get; }

        void IncrementTurn();
        void SwapCurrentPlayer();
    }
}
