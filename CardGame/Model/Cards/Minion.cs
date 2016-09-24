using System;
using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public class Minion : Card, IDamageable
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

        public Minion(string name, int cost, IPlayer player, int attack, int health, CardSubType subType,
            List<CardTrigger> triggers) :
            base(name, cost, player, CardType.Minion, subType, triggers)
        {
            Attack = attack;
            TemporaryAttackBuff = 0;
            PermanentAttackBuff = 0;

            Health = health;
            TemporaryHealthBuff = 0;
            PermanentHealthBuff = 0;
            MaxHealth = Health + TemporaryHealthBuff + PermanentHealthBuff;
            CurrentHealth = MaxHealth;
            IsDead = false;
        }

        public int Attack { get; set; }
        public int TemporaryAttackBuff { get; set; }
        public int PermanentAttackBuff { get; set; }
        public int CurrentAttack => Math.Min(0, Attack + TemporaryAttackBuff + PermanentAttackBuff);

        public int Health { get; set; }
        public int TemporaryHealthBuff { get; set; }
        public int PermanentHealthBuff { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;

            //OnGetHit events
            GameEventManager.OnGetHit(this, damage);
            GameEventManager.OnOtherGetHit(this, damage);

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                //OnDeath events
                GameEventManager.OnDeath();
                GameEventManager.OnOtherDeath();

                //Unregister GameEvents
                GameEventManager.UnregisterForEvents(this);
            }
        }
    }
}
