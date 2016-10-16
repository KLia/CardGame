#r "..\\CardGame\\bin\\Debug\\CardGame.dll"
using CardGame.Model.Cards;

/// <summary>
/// Implements CARD_NAME Minion
/// 
/// TODO : Implement
/// </summary>
public class MinionWithBattleCry : Minion, ITriggerable
{
    private const int MANA_COST = 2;
    private const int ATTACK = 1;
    private const int HEALTH = 1;

    public MinionWithBattleCry()
    {
        BaseAttack = ATTACK;
        BaseHealth = HEALTH;
        BaseCost = MANA_COST;
    }

    public TriggerType TriggerTypes => TriggerType.OnCardPlayed;

    public void AttachEvent()
    {
        GameEventManager.RegisterForEventCardPlayed(this, OnSummon);
    }

    public void OnSummon(ICard card)
    {
        if (card == this)
        {
            if (((Minion)card).PlayerOwner.CurrentMana < GameConstants.TOTAL_MANA)
            {
                ((Minion)card).PlayerOwner.CurrentMana++;
            }
        }
    }
}