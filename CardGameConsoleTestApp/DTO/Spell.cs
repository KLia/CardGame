using System;

namespace CardGameConsoleTestApp.DTO
{
    public class Spell : Card
    {
        public override event EventHandler CardDrawn;
        public override event EventHandler CardPlayed;
        public override event EventHandler RoundStart;
        public override event EventHandler RoundEnd;

        public Spell() : this("", 0)
        {
        }

        public Spell(string name, int cost) : base(name, cost)
        {
        }

        public override void OnCardDrawn(object sender, EventArgs e)
        {
            CardDrawn?.Invoke(sender, e);
        }

        public override void OnCardPlayed(object sender, EventArgs e)
        {
            CardPlayed?.Invoke(sender, e);
        }

        public override void OnRoundStart(object sender, EventArgs e)
        {
            RoundStart?.Invoke(sender, e);
        }

        public override void OnRoundEnd(object sender, EventArgs e)
        {
            RoundEnd?.Invoke(sender, e);
        }
    }
}
