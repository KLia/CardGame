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

        public abstract event EventHandler CardDrawn;
        public abstract event EventHandler CardPlayed;
        public abstract event EventHandler RoundStart;
        public abstract event EventHandler RoundEnd;

        public abstract void OnCardDrawn(object sender, EventArgs e);
        public abstract void OnCardPlayed(object sender, EventArgs e);
        public abstract void OnRoundStart(object sender, EventArgs e);
        public abstract void OnRoundEnd(object sender, EventArgs e);
    }
}
