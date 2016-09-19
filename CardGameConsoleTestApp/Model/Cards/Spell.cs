using System.Collections.Generic;
using CardGameConsoleTestApp.Model.Cards.ValueObjects;

namespace CardGameConsoleTestApp.Model.Cards
{
    public class Spell : Card
    {
        public Spell() : this("", 0, CardSubType.None, null)
        {
        }

        public Spell(string name, int cost, CardSubType subType, List<CardTrigger> triggers) : base(name, cost, CardType.Spell, subType, triggers)
        {
        }
    }
}
