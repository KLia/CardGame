namespace CardGameConsoleTestApp.Model.Cards
{
    public enum TriggerType
    {
        OnTurnStart = 1,
        OnTurnEnd = 2,
        OnCardDrawn = 3,
        OnAttack = 4,
        OnOtherAttack = 5,
        OnHealed = 6,
        OnOtherHealed = 7,
        OnGetHit = 8,
        OnOtherGetHit = 9,
        OnDeath = 10,
        OnOtherDeath = 11,
        OnCardPlayed = 12,
        OnOtherCardPlayed = 13,
        OnSpellCast = 14,
        OnSpellTarget = 15
    }
}
