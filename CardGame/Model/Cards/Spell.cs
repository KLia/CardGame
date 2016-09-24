using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public abstract class Spell : Card
    {
        protected Spell() : this("", 0, null, CardSubType.None, null)
        {
        }

        protected Spell(string name, int cost, IPlayer player, CardSubType subType, List<CardTrigger> triggers)
            : base(name, cost, player, CardType.Spell, subType, triggers)
        {
        }

        public abstract void Cast(IDamageable target = null);
    }
}