using System;
using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine.Interfaces;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Engine
{
    public class GameEngine : IGameEngine
    {
        private static int _playOrder = 0;
        public static Random RngRandom { get; private set; }
        public IGameState GameState { get; }

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

        /// <summary>
        /// Called whenever a player plays a card from their hand
        /// </summary>
        /// <param name="player">The player playing the card</param>
        /// <param name="card">The card being played</param>
        /// <param name="boardPos">The new position on the board where the card is dropped</param>
        public void PlayCard(IPlayer player, ICard card, int boardPos, IDamageable target = null)
        {
            if (GameState.CurrentPlayer != player)
            {
                throw new InvalidOperationException("It is not currently your turn");
            }

            if (!player.CardsInHand.Contains(card))
            {
                throw new InvalidOperationException("The card you're trying to play is not in your hand");
            }

            if (player.Mana < card.Cost)
            {
                throw new InvalidOperationException("Not enough Mana");
            }

            //move from hand to board, assign PlayOrder and decrease mana
            card.PlayOrder = _playOrder++;
            player.CardsInHand.Remove(card);
            player.Mana -= card.Cost;

            //play events
            switch (card.Type)
            {
                case CardType.Minion:
                    var playerHand = GameState.CurrentPlayer == GameState.Player1
                        ? GameState.Board.Player1PlayZone
                        : GameState.Board.Player2PlayZone;

                    playerHand.Insert(boardPos, card);
                    GameEventManager.OnCardPlayed(card);
                    GameEventManager.OnOtherCardPlayed(card);
                    break;

                case CardType.Spell:
                    bool abort;
                    GameEventManager.OnSpellCast((Spell) card, target, out abort);

                    if (!abort)
                    {
                        ((Spell) card).Activate(target);

                        if (target != null)
                        {
                            GameEventManager.OnSpellTarget(target);
                        }
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
