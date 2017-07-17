using System;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
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

            ZonePos = -1;
            Zone = GameBoardZone.Deck;

            TemporaryCostBuff = 0;
            PermanentCostBuff = 0;
        }

        public int Id { get; }

        public int PlayOrder { get; set; }
        public IPlayer PlayerOwner { get; set; }

        public string Name { get; set; }
        public int BaseManaCost { get; set; }
        public int TemporaryCostBuff { get; set; }
        public int PermanentCostBuff { get; set; }
        public int CurrentManaCost => Math.Min(0, BaseManaCost + TemporaryCostBuff + PermanentCostBuff);

        public CardType Type { get; set; }
        public CardSubType SubType { get; set; }
        public GameBoardZone Zone { get; set; }
        public int ZonePos { get; set; }

        /// <summary>
        /// Called whenever a player plays a card from their hand
        /// </summary
        /// <param name="boardPos">The new position on the board where the card is dropped</param>
        /// <param name="target">The card to attack, if any</param>
        public abstract void PlayCard(int boardPos, IDamageable target = null);
    }
}