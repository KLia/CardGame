using CardGameConsoleTestApp.Model.Cards.ValueObjects;

namespace CardGameConsoleTestApp.Model.Cards
{
    public class Spell : Card
    {
        public Spell() : this("", 0, CardSubType.None)
        {
        }

        public Spell(string name, int cost, CardSubType subType) : base(name, cost, CardType.Spell, subType)
        {
        }
    }
}
