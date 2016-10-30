
/// <summary>
/// Implements MinionWithAura Minion
/// 
/// All friendly minions get +1 Attack
/// 
/// TODO : Implement
/// </summary>
public class MinionWithAura : Minion
{
    private const int MANA_COST = 2;
    private const int ATTACK = 1;
    private const int HEALTH = 1;

    public MinionWithAura()
    {
        BaseAttack = ATTACK;
        BaseHealth = HEALTH;
        BaseCost = MANA_COST;

        GameEventManager.RegisterForEventCardPlayed(this, OnSummon);
        GameEventManager.RegisterForEventDeath(this, OnDeath);
    }

    public void OnSummon(ICard card)
    {
        if (card == this)
        {
            card.PlayerOwner.AreaBuffs.AreaAttackBuff += 1;
        }
    }

    public void OnDeath(ICard card)
    {
        if (card == this)
        {
            card.PlayerOwner.AreaBuffs.AreaAttackBuff -= 1;
        }
    }

    public new void Silence()
    {
        base.Silence();
        OnDeath(this);
    }
}