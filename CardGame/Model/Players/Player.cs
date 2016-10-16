﻿using System;
using System.Collections.Generic;
using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Decks;
using CardGame.Model.Decks.Interfaces;
using CardGame.Model.Engine;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Players
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalMana { get; set; }
        public int CurrentMana { get; set; }
        public IDeck Deck { get; set; }

        public List<ICard> CardsInHand { get; set; }
        public List<ICard> CardsInPlay { get; set; }
        public List<ICard> CardsInGraveyard { get; set; }

        public Player(int id, string name, int mana, IDeck deck)
        {
            Id = id;
            Name = name;
            TotalMana = mana;
            CurrentMana = mana;
            Deck = deck;
            CardsInHand = new List<ICard>(GameConstants.MAX_CARDS_IN_HAND);
            CardsInPlay = new List<ICard>(GameConstants.MAX_CARDS_IN_PLAY);
            CardsInGraveyard = new List<ICard>();
        }

        public ICard DrawCard(bool isMulligan = false)
        {
            var card = Deck.DrawCard();

            //if player already has maximum  number of cards in hand, burn this card
            if (CardsInHand.Count == GameConstants.MAX_CARDS_IN_HAND)
            {
                throw new InvalidOperationException($"Cannot have more than {GameConstants.MAX_CARDS_IN_HAND} cards in hand");
            }

            //add card to hand
            CardsInHand.Add(card);

            //only fire the is card drawn if it's not the mulligan stage
            if (!isMulligan)
            {
                GameEventManager.OnCardDrawn(card);
            }

            return card;
        }

        public List<ICard> DrawCards(int count, bool isMulligan = false)
        {
            //todo = handle case of no more cards to draw
            var cards = Deck.DrawCards(count);
            CardsInHand.AddRange(cards);

            if (!isMulligan)
            {
                foreach (var card in cards)
                {
                    GameEventManager.CardDrawn(card);
                }
            }

            return cards;
        }

        /// <summary>
        /// Called whenever a player plays a card from their hand
        /// </summary>
        /// <param name="card">The card being played</param>
        /// <param name="boardPos">The new position on the board where the card is dropped</param>
        /// <param name="target">The card to attack, if any</param>
        public void PlayCard(ICard card, int boardPos, IDamageable target = null)
        {
            if (!CardsInHand.Contains(card))
            {
                throw new InvalidOperationException("The card you're trying to play is not in your hand");
            }

            if (CurrentMana < card.CurrentCost)
            {
                throw new InvalidOperationException("Not enough Mana");
            }

            if (CardsInPlay.Count == GameConstants.MAX_CARDS_IN_PLAY)
            {
                throw new InvalidOperationException(
                    $"Cannot have more than {GameConstants.MAX_CARDS_IN_PLAY} cards in play");
            }

            //move from hand to board, assign PlayOrder, PlayerOwner Owner and decrease mana
            card.PlayOrder = Cards.Card.CurrentPlayOrder;
            card.PlayerOwner = this;
            CurrentMana -= card.CurrentCost;

            //play events
            switch (card.Type)
            {
                case CardType.Minion:
                    SummonMinion(card, boardPos);
                    break;

                case CardType.Spell:
                    CastSpell(card, target);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SummonMinion(ICard card, int boardPos, bool isForcedSummoned = false)
        {
            MoveCard(card, GameBoardZone.Hand, GameBoardZone.Board, boardPos);

            //Trigger the on CardPlayed Event - if the card is not forced summoned
            if (!isForcedSummoned)
            {
                GameEventManager.CardPlayed(card);
            }
        }

        private void CastSpell(ICard card, IDamageable target)
        {
            bool abort;

            MoveCard(card, GameBoardZone.Hand, GameBoardZone.Graveyard);
            GameEventManager.SpellCast((Spell) card, target, out abort);

            if (!abort)
            {
                ((Spell) card).Cast(target);

                if (target != null)
                {
                    GameEventManager.SpellTarget(target);
                }
            }
        }

        /// <summary>
        /// Moves the card from one board zone to another
        /// </summary>
        /// <param name="card">The card to move</param>
        /// <param name="sourceZone">From which board zone</param>
        /// <param name="destZone">To which board zone</param>
        /// <param name="boardPos">If it is dropped in a specific position, use it</param>
        /// <param name="isCopy">If it should be copies rather than moved, do not remove it from source zone</param>
        public void MoveCard(ICard card, GameBoardZone sourceZone, GameBoardZone destZone, int boardPos = -1, bool isCopy = false)
        {
            var source = sourceZone.GetPlayerBoardZone(this);
            var dest = destZone.GetPlayerBoardZone(this);

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
