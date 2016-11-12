using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;

namespace CardGame.Model.Cards
{
    public abstract class Spell : Card, ISpell
    {
        protected Spell()
        {
            Type = CardType.Spell;
        }

        public abstract void Cast(IDamageable target = null);

        public override void PlayCard(int boardPos, IDamageable target = null)
        {
            bool abort;

            PlayerOwner.MoveCard(this, GameBoardZone.Hand, GameBoardZone.Graveyard);
            GameEventManager.SpellCast(this, target, out abort);

            if (!abort)
            {
                Cast(target);
            }
        }
    }
}