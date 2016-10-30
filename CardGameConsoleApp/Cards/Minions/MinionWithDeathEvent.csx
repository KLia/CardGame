/// <summary>
/// Implements MinionWithDeathEvent Minion
/// 
/// TODO : Implement
/// </summary>
public class MinionWithDeathEvent : Minion
{
    private const int MANA_COST = 1;
    private const int ATTACK = 1;
    private const int HEALTH = 1;

    public MinionWithDeathEvent()
    {
        BaseAttack = ATTACK;
        BaseHealth = HEALTH;
        BaseCost = MANA_COST;
    }
}