using System;
using CardGameConsoleTestApp.Model.Cards.Interfaces;

namespace CardGameConsoleTestApp.Model.Cards
{
    public abstract class Card : ICard
    {
        private static int _id = 0;

        protected Card() : this("", 0)
        {
        }

        protected Card(string name, int cost)
        {
            _id++;
            Name = name;
            Cost = cost;
        }

        public int Id => _id;

        public string Name { get; set; }
        public int Cost { get; set; }
    }
}