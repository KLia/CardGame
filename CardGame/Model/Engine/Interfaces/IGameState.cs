using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Engine.Interfaces
{
    public interface IGameState
    {
        IGameBoard Board { get; set; }
        IPlayer Player { get; set; }
        IPlayer Opponent { get; set; }
        IPlayer CurrentPlayer { get; set; }
        int Turn { get; set; }
    }
}
