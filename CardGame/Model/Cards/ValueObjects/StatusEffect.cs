using System;

namespace CardGame.Model.Cards.ValueObjects
{
    [Flags]
    public enum StatusEffect
    {
        Exhausted = 1,
        CantAttack = 2,
        Charge = 4,
        Shield = 8,
        Taunt = 16
    }

    public static class StatusEffectExtensions
    {
        /// <summary>
        /// Resets all Status Effects except for EXHAUSTED - that cannot be removed
        /// </summary>
        /// <param name="flags">The StatusEffect variable we want to remove all effects from</param>
        /// <returns></returns>
        public static StatusEffect ResetAll(this StatusEffect flags)
        {
            return flags & (~StatusEffect.CantAttack & ~StatusEffect.Charge & StatusEffect.Shield & ~StatusEffect.Taunt);
        }
    }
}
