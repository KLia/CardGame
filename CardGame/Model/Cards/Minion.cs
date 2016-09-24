using System;
using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public class Minion : Card, IAttacker, IDamageable
    {
        public Minion() : this("", 0, null, 0, 0, CardSubType.None, null)
        {
        }

        public Minion(IPlayer player, List<CardTrigger> triggers)
            : this("", 0, player, 0, 0, CardSubType.None, triggers)
        {
        }

        public Minion(string name, int cost, IPlayer player) : this(name, cost, player, 0, 0, CardSubType.None, null)
        {
        }

        public Minion(string name, int cost, IPlayer player, int attack, int baseHealth, CardSubType subType,
            List<CardTrigger> triggers) :
            base(name, cost, player, CardType.Minion, subType, triggers)
        {
            BaseAttack = attack;
            TemporaryAttackBuff = 0;
            PermanentAttackBuff = 0;

            BaseHealth = baseHealth;
            TemporaryHealthBuff = 0;
            PermanentHealthBuff = 0;
            MaxHealth = BaseHealth + TemporaryHealthBuff + PermanentHealthBuff;
            CurrentHealth = MaxHealth;
            IsDead = false;

            CanAttack = false;
        }

        public int BaseAttack { get; set; }
        public int TemporaryAttackBuff { get; set; }
        public int PermanentAttackBuff { get; set; }
        public int CurrentAttack => Math.Min(0, BaseAttack + TemporaryAttackBuff + PermanentAttackBuff);

        public int BaseHealth { get; set; }
        public int TemporaryHealthBuff { get; set; }
        public int PermanentHealthBuff { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }

        public bool CanAttack { get; set; }

        public void AttackTarget(IDamageable target)
        {
            if (!CanAttack)
            {
                throw new InvalidOperationException("Cannot attack yet");
            }

            if (target == this)
            {
                throw new InvalidOperationException("Cannot attack yourself");
            }

            var abort = false;
            GameEventManager.OnAttack(this, target);
            GameEventManager.OnOtherAttack(this, target, out abort);
        }

        public void TakeDamage(int damage)
        {
            //Apply damage
            CurrentHealth -= damage;

            //OnGetHit events
            GameEventManager.OnGetHit(this, damage);
            GameEventManager.OnOtherGetHit(this, damage);

            //Check for Death
            if (CurrentHealth <= 0)
            {
                IsDead = true;
                //OnDeath events
                GameEventManager.OnDeath(this);
                GameEventManager.OnOtherDeath(this);

                //Unregister GameEvents
                GameEventManager.UnregisterForEvents(this);
            }
        }

    }
}
