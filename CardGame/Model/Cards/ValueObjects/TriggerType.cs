using System;

namespace CardGame.Model.Cards.ValueObjects
{
    [Flags]
    public enum TriggerType
    {
        None = 0,
        OnTurnStart = 1,
        OnTurnEnd = 2,
        OnCardDrawn = 4,
        OnAttack = 8,
        OnHealed = 16,
        OnGetHit = 32,
        OnDeath = 64,
        OnCardPlayed = 128,
        OnMinionSummoned = 512,
        OnSpellCast = 1024
    }
}
