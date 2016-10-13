using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
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
    }
}