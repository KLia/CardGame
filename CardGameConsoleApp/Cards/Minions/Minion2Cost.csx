#r "C:\Users\keith\Documents\GitHub\CardGame\CardGame\bin\Debug\CardGame.dll"
using CardGame.Model.Cards;

/// <summary>
/// Implements CARD_NAME Minion
/// 
/// TODO : Implement
/// </summary>
public class Minion2Cost : Minion
{
    private const int MANA_COST = 2;
    private const int ATTACK = 2;
    private const int HEALTH = 2;
    
    public Minion2Cost()
    {
        BaseAttack = ATTACK;
        BaseHealth = HEALTH;
        BaseCost = MANA_COST;
    }
}