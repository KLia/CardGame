namespace CardGame.Model.Cards.ValueObjects
{
    public enum TriggerType
    {
        OnTurnStart = 1,
        OnTurnEnd = 2,
        OnCardDrawn = 3,
        OnAttack = 4,
        OnHealed = 5,
        OnGetHit = 6,
        OnDeath = 7,
        OnCardPlayed = 8,
        OnSpellCast = 9,
        OnSpellTarget = 10
    }
}
