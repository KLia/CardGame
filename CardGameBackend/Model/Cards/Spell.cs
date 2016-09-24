using System.Collections.Generic;
using CardGameBackend.Model.Cards.ValueObjects;
using CardGameBackend.Model.Cards.Interfaces;
using CardGameBackend.Model.Players;

namespace CardGameBackend.Model.Cards
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

        public abstract void Activate(IDamageable target = null);
    }
}