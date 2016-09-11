using CardGameConsoleTestApp.Model.Players;

namespace CardGameConsoleTestApp.Model.Engine.Interfaces
{
    public interface IGameState
    {
        IGameBoard Board { get; set; }
        IPlayer Player1 { get; set; }
        IPlayer Player2 { get; set; }
        IPlayer CurrentPlayer { get; set; }
        int Turn { get; set; }
    }
}
