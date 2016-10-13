using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;

namespace CardGame.Model.Cards
{
    public abstract class Spell : Card, ISpell
    {
        protected Spell()
        {
            Type = CardType.Spell;
        }

        public abstract void Cast(IDamageable target = null);
    }
}