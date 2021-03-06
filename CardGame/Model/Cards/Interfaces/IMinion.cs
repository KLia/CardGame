﻿using CardGame.Model.Cards.ValueObjects;

namespace CardGame.Model.Cards.Interfaces
{
    public interface IMinion : ICard, IAttacker, IDamageable, ITriggerable
    {
        void ApplyStatusEffect(StatusEffect effects);
        void RemoveStatusEffect(StatusEffect effects);

    }
}
