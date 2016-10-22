﻿#r "C:\Users\keith\Documents\GitHub\CardGame\CardGame\bin\Debug\CardGame.dll"
using CardGame.Model.Cards;
/// <summary>
/// Implements BasicMinion Minion
/// 
/// TODO : Implement
/// </summary>
public class BasicMinion : Minion
{
    private const int MANA_COST = 1;
    private const int ATTACK = 1;
    private const int HEALTH = 2;

    public BasicMinion()
    {
        BaseAttack = ATTACK;
        BaseHealth = HEALTH;
        BaseCost = MANA_COST;
    }
}