using System;
using CardGameConsoleTestApp.DTO.Interfaces;

namespace CardGameConsoleTestApp.DTO
{
    public abstract class Card : ICard
    {
        protected Card() : this("", 0)
        {
        }

        protected Card(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public string Name { get; set; }
        public int Cost { get; set; }

        public event EventHandler CardDrawn;
        public event EventHandler CardPlayed;

        public abstract void OnCardDrawn(EventArgs e);
        public abstract void OnCardPlayed(EventArgs e);
    }
}
