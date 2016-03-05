using System;

namespace CardGameConsoleTestApp.DTO
{
    public class Spell : Card
    {
        public Spell() : this("", 0)
        {
        }

        public Spell(string name, int cost) : base(name, cost)
        {
        }

        public override void OnCardDrawn(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnCardPlayed(EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
