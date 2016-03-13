using System;
using CardGameConsoleTestApp.Cards.Interfaces;

namespace CardGameConsoleTestApp.Cards
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
        public event EventHandler RoundStart;
        public event EventHandler RoundEnd;

        public void OnCardDrawn(object sender, EventArgs e)
        {
            CardDrawn?.Invoke(sender, e);
        }

        public void OnCardPlayed(object sender, EventArgs e)
        {
            CardPlayed?.Invoke(sender, e);
        }

        public void OnRoundStart(object sender, EventArgs e)
        {
            RoundStart?.Invoke(sender, e);
        }

        public void OnRoundEnd(object sender, EventArgs e)
        {
            RoundEnd?.Invoke(sender, e);
        }
    }
}
