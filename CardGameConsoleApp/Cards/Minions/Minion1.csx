/// <summary>
/// Implements Minion1
/// 
/// TODO : Implement
/// </summary>
public class Minion1 : Minion
{
    private const int MANA_COST = 1;
    private const int ATTACK = 1;
    private const int HEALTH = 1;

    public Minion1()
    {
        BaseAttack = ATTACK;
        BaseHealth = HEALTH;
        BaseCost = MANA_COST;
    }
}