using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Create an IEnumerable for all StatusEffect. Used as a helper function
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<StatusEffect> GetAllStatusEffects()
        {
            return (StatusEffect[]) Enum.GetValues(typeof(StatusEffect));
        }

        /// <summary>
        /// Resets all Status Effects except for EXHAUSTED - that cannot be removed
        /// </summary>
        /// <param name="flags">The StatusEffect variable we want to remove all effects from</param>
        /// <returns></returns>
        public static StatusEffect ResetAll(this StatusEffect flags)
        {
            return GetAllStatusEffects()
                .Where(effect => effect != StatusEffect.Exhausted)
                .Aggregate(flags, (current, effect) => current & ~effect);

            // ---- Equivalent to:
            //foreach (StatusEffect effect in GetAllStatusEffects())
            //{
            //    if (effect != StatusEffect.Exhausted)
            //    {
            //        flags = flags & ~effect;
            //    }
            //}
        }
    }
}