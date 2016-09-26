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
        private static int _playOrder = 0;
        public static Random RngRandom { get; private set; }
        public IGameState GameState { get; }

        public GameEngine(IPlayer player, IPlayer opponent, IPlayer currentPlayer, int seed = 1)
        {
            RngRandom = new Random(seed);
            GameEventManager.Initialize();
            GameState = new GameState(player, opponent, currentPlayer);
        }

        public void StartGame(IPlayer player1, IPlayer player2)
        {
            //shuffle players' decks
            player1.Deck.Shuffle();
            player2.Deck.Shuffle();

            //draw cards
            player1.DrawCards(GameConstants.DRAW_CARDS_AT_GAME_START, true);
            player2.DrawCards(GameConstants.DRAW_CARDS_AT_GAME_START, true);
        }

        public void StartTurn()
        {
            GameEventManager.TurnStart();
        }

        public void EndTurn()
        {
            //trigger end turn events first
            GameEventManager.TurnEnd();

            //swap CurrentPlayer and increment turn number
            GameState.CurrentPlayer = GameState.CurrentPlayer == GameState.Player ? GameState.Opponent : GameState.Player;
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

            if (player.Mana < card.CurrentCost)
            {
                throw new InvalidOperationException("Not enough Mana");
            }

            if (player.CardsInPlay.Count == GameConstants.MAX_CARDS_IN_PLAY)
            {
                throw new InvalidOperationException(
                    $"Cannot have more than {GameConstants.MAX_CARDS_IN_PLAY} cards in play");
            }

            //move from hand to board, assign PlayOrder and decrease mana
            card.PlayOrder = _playOrder++;
            player.Mana -= card.CurrentCost;

            //play events
            switch (card.Type)
            {
                case CardType.Minion:
                    MoveCard(card, player, GameBoardZone.Hand, player, GameBoardZone.Board, boardPos);
                    GameEventManager.CardPlayed(card);
                    break;

                case CardType.Spell:
                    bool abort;

                    MoveCard(card, player, GameBoardZone.Hand, player, GameBoardZone.Graveyard);
                    GameEventManager.SpellCast((Spell) card, target, out abort);

                    if (!abort)
                    {
                        ((Spell) card).Cast(target);

                        if (target != null)
                        {
                            GameEventManager.SpellTarget(target);
                        }
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void MoveCard(ICard card, IPlayer sourcePlayer, GameBoardZone sourceZone, IPlayer destPlayer, GameBoardZone destZone, int boardPos = -1, bool isCopy = false)
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
