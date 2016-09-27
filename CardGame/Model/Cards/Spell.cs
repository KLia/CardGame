using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public abstract class Spell : Card
    {
        protected Spell() : this("", 0, null, CardSubType.None)
        {
        }

        protected Spell(string name, int baseCost, IPlayer player, CardSubType subType)
            : base(name, baseCost, player, CardType.Spell, subType)
        {
        }

        public abstract void Cast(IDamageable target = null);
    }
}