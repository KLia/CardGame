using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public abstract class Card : ICard
    {
        private static int _id = 0;

        protected Card(string name, int baseCost, IPlayer player, CardType type, CardSubType subType, List<CardTrigger> triggers)
        {
            _id++;
            Id = _id;
            Player = player;
            Name = name;
            BaseCost = baseCost;
            TemporaryCostBuff = 0;
            PermanentCostBuff = 0;
            Type = type;
            SubType = subType;

            Triggers = triggers;
        }

        public int Id { get; }

        public int PlayOrder { get; set; }
        public IPlayer Player { get; set; }

        public string Name { get; set; }
        public int BaseCost { get; set; }
        public int TemporaryCostBuff { get; set; }
        public int PermanentCostBuff { get; set; }
        public int CurrentCost => BaseCost + TemporaryCostBuff + PermanentCostBuff;

        public CardType Type { get; set; }
        public CardSubType SubType { get; set; }
        public List<CardTrigger> Triggers { get; set; }
    }
}