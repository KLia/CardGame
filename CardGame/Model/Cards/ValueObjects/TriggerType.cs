using System;
using System.Collections.Generic;

namespace CardGame.Model.Cards.ValueObjects
{
    [Flags]
    public enum TriggerType
    {
        None = 0,
        OnTurnStart = 1,
        OnTurnEnd = 2,
        OnCardDrawn = 4,
        OnAttack = 8,
        OnHealed = 16,
        OnGetHit = 32,
        OnDeath = 64,
        OnCardPlayed = 128,
        OnMinionSummoned = 512,
        OnSpellCast = 1024
    }

    public static class TriggerTypeExtensions
    {
        /// <summary>
        /// Create an IEnumerable for all TriggerTypes. Used as a helper function
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<TriggerType> GetAllTriggerTypes()
        {
            return (TriggerType[]) Enum.GetValues(typeof(TriggerType));
        }
    }
}
