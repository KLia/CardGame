#r "..\\CardGame\\bin\\Debug\\CardGame.dll"
using CardGame.Model.Cards;

/// <summary>
/// Implements CARD_NAME Minion
/// 
/// TODO : Implement
/// </summary>
public class TemplateMinion : Minion
{
    private const int MANA_COST = 1;
    private const int ATTACK = 1;
    private const int HEALTH = 1;

    public TemplateMinion()
    {
        BaseAttack = ATTACK;
        BaseHealth = HEALTH;
        BaseCost = MANA_COST;
    }
}