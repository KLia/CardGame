using System;
using System.Collections.Generic;
using CardGameConsoleTestApp.Controller;
using CardGameConsoleTestApp.Model.Cards.Interfaces;
using CardGameConsoleTestApp.Model.Cards.ValueObjects;
using CardGameConsoleTestApp.Model.Engine;

namespace CardGameConsoleTestApp.Model.Cards
{
    public abstract class Card : ICard
    {
        private static int _id = 0;
        public static int _playOrder = 0;

        protected Card(string name, int cost, CardType type, CardSubType subType, List<CardTrigger> triggers)
        {
            _id++;
            Id = _id;
            Name = name;
            Cost = cost;
            Type = type;
            SubType = subType;

            Triggers = triggers;
        }

        public int Id { get; }
        public int PlayOrder { get; set; }

        public string Name { get; set; }
        public int Cost { get; set; }

        public CardType Type { get; set; }
        public CardSubType SubType { get; set; }

        public List<CardTrigger> Triggers { get; set; }
    }
}