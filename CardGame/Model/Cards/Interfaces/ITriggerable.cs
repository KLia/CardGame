using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards.Interfaces
{
    public interface ITriggerable
    {
        TriggerType TriggerTypes { get; }

        //Trigger Events
        void OnTurnStart(IPlayer player);
        void OnTurnEnd(IPlayer player);
        void OnCardDrawn(ICard card);
        void OnAttack(IAttacker attacker, IDamageable target, out bool abort);
        void OnHealed(IDamageable target, int heal);
        void OnGetHit(IDamageable target, int damage);
        void OnDeath(IDamageable target);
        void OnCardPlayed(ICard card);
        void OnMinionSummoned(IMinion card, IDamageable target);
        void OnSpellCast(ISpell spell, IDamageable target, out bool abort);
    }
}