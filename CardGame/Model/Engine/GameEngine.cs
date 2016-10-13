using System;
using System.Collections.Generic;
using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine.Interfaces;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Engine
{
    public class GameEngine : IGameEngine
    {
        public static Random RngRandom { get; private set; }
        public static  IGameState GameState { get; private set; }

        public static void Initialize(IPlayer player, IPlayer opponent, IGameState state, int seed = 1)
        {
            RngRandom = new Random(seed);
            GameEventManager.Initialize();
            GameState = state;
        }

        public static void Uninitialize()
        {
            RngRandom = null;
            GameEventManager.Uninitialize();
            GameState = null;
        }

        public static void StartGame(IPlayer player1, IPlayer player2, int cardsToDraw)
        {
            //shuffle players' decks
            player1.Deck.Shuffle();
            player2.Deck.Shuffle();

            //draw cards
            player1.DrawCards(cardsToDraw, true);
            player2.DrawCards(cardsToDraw, true);
        }

        public static void StartTurn()
        {
            var currentPlayer = GameState.CurrentPlayer;
            GameEventManager.TurnStart(currentPlayer);

            currentPlayer.DrawCard();
        }

        public static void EndTurn()
        {
            //trigger end turn events first
            GameEventManager.TurnEnd(GameState.CurrentPlayer);

            //swap CurrentPlayer and increment turn number
            GameState.CurrentPlayer = GameState.CurrentPlayer == GameState.Player ? GameState.Opponent : GameState.Player;
            GameState.IncrementTurn();
        }

        /// <summary>
        /// Called whenever a player plays a card from their hand
        /// </summary>
        /// <param name="player">The player playing the card</param>
        /// <param name="card">The card being played</param>
        /// <param name="boardPos">The new position on the board where the card is dropped</param>
        /// <param name="target">The target card</param>
        public static void PlayCard(IPlayer player, ICard card, int boardPos, IDamageable target = null)
        {
            if (GameState.CurrentPlayer != player)
            {
                throw new InvalidOperationException("It is not currently your turn");
            }

            player.PlayCard(card, boardPos, target);
        }

        public static void MoveCard(ICard card, IPlayer sourcePlayer, GameBoardZone sourceZone, IPlayer destPlayer, GameBoardZone destZone, int boardPos = -1, bool isCopy = false)
        {
            var source  = sourceZone.GetPlayerBoardZone(sourcePlayer);
            var dest = destZone.GetPlayerBoardZone(destPlayer);

            if (!source.Contains(card))
            {
                throw new InvalidOperationException("Card does not exist in source");
            }

            if (destZone == GameBoardZone.Hand && dest.Count >= GameConstants.MAX_CARDS_IN_HAND)
            {
                throw new InvalidOperationException("Hand is full");
            }

            if (destZone == GameBoardZone.Board && dest.Count >= GameConstants.MAX_CARDS_IN_PLAY)
            {
                throw new InvalidOperationException("Board is full");
            }

            //remove the card if we are not copying
            if (!isCopy)
            {
                source.Remove(card);
            }

            //insert into the correct position
            if (boardPos > -1 && boardPos < dest.Count)
            {
                dest.Insert(boardPos, card);
            }
            else
            {
                dest.Add(card);
            }
        }
    }
}
