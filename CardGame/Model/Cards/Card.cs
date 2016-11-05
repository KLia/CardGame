using System;
using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public abstract class Card : ICard
    {
        private static int _id = 0;
        private static int _playOrder = 0;
        public static int CurrentPlayOrder => _playOrder++;

        protected Card()
        {
            _id++;
            Id = _id;
            TemporaryCostBuff = 0;
            PermanentCostBuff = 0;
        }

        public int Id { get; }

        public int PlayOrder { get; set; }
        public IPlayer PlayerOwner { get; set; }

        public string Name { get; set; }
        public int BaseCost { get; set; }
        public int TemporaryCostBuff { get; set; }
        public int PermanentCostBuff { get; set; }
        public int CurrentCost => BaseCost + TemporaryCostBuff + PermanentCostBuff;

        public CardType Type { get; set; }
        public CardSubType SubType { get; set; }

        /// <summary>
        /// Called whenever a player plays a card from their hand
        /// </summary
        /// <param name="boardPos">The new position on the board where the card is dropped</param>
        /// <param name="target">The card to attack, if any</param>
        public virtual void PlayCard(int boardPos, IDamageable target = null)
        {
            if (!PlayerOwner.CardsInHand.Contains(this))
            {
                throw new InvalidOperationException("The card you're trying to play is not in your hand");
            }

            if (PlayerOwner.CurrentMana < CurrentCost)
            {
                throw new InvalidOperationException("Not enough Mana");
            }

            //move from hand to board, assign PlayOrder, and decrease mana
            PlayOrder = CurrentPlayOrder;
            PlayerOwner.CurrentMana -= CurrentCost;
        }

        /// <summary>
        /// Moves the card from one board zone to another
        /// </summary>
        /// <param name="sourceZone">From which board zone</param>
        /// <param name="destZone">To which board zone</param>
        /// <param name="boardPos">If it is dropped in a specific position, use it</param>
        /// <param name="isCopy">If it should be copies rather than moved, do not remove it from source zone</param>
        public void MoveCard(GameBoardZone sourceZone, GameBoardZone destZone, int boardPos = -1, bool isCopy = false)
        {
            var source = sourceZone.GetPlayerBoardZone(PlayerOwner);
            var dest = destZone.GetPlayerBoardZone(PlayerOwner);

            if (!source.Contains(this))
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
                source.Remove(this);
            }

            //insert into the correct position
            if (boardPos > -1 && boardPos < dest.Count)
            {
                dest.Insert(boardPos, this);
            }
            else
            {
                dest.Add(this);
            }
        }
    }
}