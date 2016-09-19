using System;
using CardGameBackend.Model.Engine.Interfaces;
using CardGameBackend.Model.Players;

namespace CardGameBackend.Model.Engine
{
    public class GameEngine : IGameEngine
    {
        public static Random RngRandom { get; private set; }
        public static IGameState GameState { get; private set; }

        public GameEngine(IGameBoard board, IPlayer player1, IPlayer player2, IPlayer currentPlayer, int seed = 1)
        {
            RngRandom = new Random(seed);
            GameEventManager.Initialize();
            GameState = new GameState(board, player1, player2, currentPlayer);
        }

        public void StartGame()
        {
            //shuffle players' decks
            GameState.Player1.Deck.Shuffle();
            GameState.Player2.Deck.Shuffle();

            //draw cards
            GameState.Player1.DrawCards(GameConstants.DRAW_CARDS_AT_GAME_START, true);
            GameState.Player2.DrawCards(GameConstants.DRAW_CARDS_AT_GAME_START, true);
        }

        public void StartTurn()
        {
            GameEventManager.OnTurnStart();
        }

        public void EndTurn()
        {
            //trigger end turn events first
            GameEventManager.OnTurnEnd();

            //finally swap CurrentPlayer and increment turn number
            GameState.CurrentPlayer = GameState.CurrentPlayer == GameState.Player1 ? GameState.Player2 : GameState.Player1;
            GameState.Turn++;
        }
    }
}
