using System;
using System.Collections.Generic;
using CardGameConsoleTestApp.Controller;
using CardGameConsoleTestApp.Model.Cards.Interfaces;

namespace CardGameConsoleTestApp.Model.Cards
{
    public abstract class Card : ICard
    {
        private static int _id = 0;
        public static int _playOrder = 0;

        protected Card() : this("", 0)
        {
        }

        protected Card(string name, int cost)
        {
            _id++;
            Name = name;
            Cost = cost;
            Id = _id;
        }

        public int Id { get; }
        public int PlayOrder { get; set; }

        public string Name { get; set; }
        public int Cost { get; set; }

        public List<Tuple<Delegate, object>> Triggers;
    }
}