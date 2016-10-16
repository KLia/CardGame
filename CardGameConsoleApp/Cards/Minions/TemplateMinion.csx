#r "..\\CardGame\\bin\\Release\\CardGame.dll"
using CardGame.Model.Cards;

/// <summary>
/// Implements CARD_NAME Minion
/// 
/// TODO : Implement
/// </summary>
public class CARD_NAME : Minion
{
    private const int MANA_COST = 1;
    private const int ATTACK = 1;
    private const int HEALTH = 1;

    public CARD_NAME()
    {
        BaseAttack = ATTACK;
        BaseHealth = HEALTH;
        BaseCost = MANA_COST;
    }
}