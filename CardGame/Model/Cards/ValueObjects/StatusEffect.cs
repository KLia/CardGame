using System;

namespace CardGame.Model.Cards.ValueObjects
{
    [Flags]
    public enum StatusEffect
    {
        Exhausted = 1,
        Charge = 2,
        Shield = 4,
        Taunt = 8
    }
}
