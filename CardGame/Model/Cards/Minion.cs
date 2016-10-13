using System;
using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public abstract class Minion : Card, IAttacker, IDamageable
    {
        protected Minion() : this("", 0, null, 0, 0, CardSubType.None, null)
        {
        }

        protected Minion(IPlayer player, List<CardTrigger> triggers)
            : this("", 0, player, 0, 0, CardSubType.None, triggers)
        {
        }

        protected Minion(string name, int baseCost, IPlayer player) : this(name, baseCost, player, 0, 0, CardSubType.None, null)
        {
        }

        protected Minion(string name, int baseCost, IPlayer player, int attack, int baseHealth, CardSubType subType,
            List<CardTrigger> triggers) :
            base(name, baseCost, player, CardType.Minion, subType, triggers)
        {
            BaseAttack = attack;
            TemporaryAttackBuff = 0;
            PermanentAttackBuff = 0;

            BaseHealth = Math.Min(1, baseHealth);
            TemporaryHealthBuff = 0;
            PermanentHealthBuff = 0;
            MaxHealth = BaseHealth + TemporaryHealthBuff + PermanentHealthBuff;
            CurrentHealth = MaxHealth;
            IsDead = false;

            StatusEffects |= StatusEffect.Exhausted;
        }

        private int _tempHealthBuff = 0;
        private int _permHealthBuff = 0;
        private int _tempAttackBuff = 0;
        private int _permAttackBuff = 0;

        public int BaseAttack { get; set; }

        public int TemporaryAttackBuff
        {
            get { return _tempAttackBuff; }
            set
            {
                if (value == 0)
                {
                    //reset Attack Buff
                    value = -_tempAttackBuff;
                }

                _tempAttackBuff = value;
            }
        }

        public int PermanentAttackBuff
        {
            get { return _permAttackBuff; }
            set
            {
                if (value == 0)
                {
                    //reset Attack Buff
                    value = -_permAttackBuff;
                }

                _permAttackBuff = value;
            }
        }

        public int CurrentAttack => Math.Min(0, BaseAttack + TemporaryAttackBuff + PermanentAttackBuff);

        public int BaseHealth { get; set; }
        public int TemporaryHealthBuff { get; set; }
        public int PermanentHealthBuff { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }

        public bool CanAttack => StatusEffects.HasFlag(StatusEffect.Exhausted);

        /// <summary>
        /// Attach all the event triggers associated with this card
        /// </summary>
        public abstract void AttachEvents();

        /// <summary>
        /// Add StatusEffects to this Minion
        /// </summary>
        private StatusEffect StatusEffects { get; set; }

        public void ApplyStatusEffects(StatusEffect effects)
        {
            StatusEffects |= effects;
        }

        /// <summary>
        /// Remove StatusEffects from this Minion
        /// </summary>
        public void RemoveStatusEffects(StatusEffect effects)
        {
            StatusEffects = StatusEffects & ~effects;
        }


        /// <summary>
        /// Takes effect whenever a Minion receives a health buff to adjust health values
        /// </summary>
        /// <param name="buff">The buff amount</param>
        private void ApplyHealthBuff(int buff)
        {
            MaxHealth += buff;
            CurrentHealth = CurrentHealth + buff;

            //just in case the minion dies when the buffs wear off
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// IAttacker AttackTarget
        /// </summary>
        /// <param name="target">The IDamageable entity that receives damage</param>
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
            GameEventManager.Attack(this, target, out abort);

            if (!abort)
            {
                //give damage equal to this minions attack
                target.TakeDamage(CurrentAttack);
            }
        }

        /// <summary>
        /// IDamageable TakeDamage
        /// </summary>
        /// <param name="damage">The damage given to this target</param>
        public void TakeDamage(int damage)
        {
            //Apply damage
            CurrentHealth -= damage;

            //OnGetHit events
            GameEventManager.GetHit(this, damage);

            //Check for Death
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// IDamageable Die
        /// The minion dies when health reaches 0.
        /// Trigger Death events, unregister all the events associated with it and move the card to the graveyard
        /// </summary>
        public void Die()
        {
            IsDead = true;
            CurrentHealth = 0;

            //OnDeath events
            GameEventManager.Death(this);

            //Unregister GameEvents
            GameEventManager.UnregisterForEvents(this);

            //move to graveyard
            GameEngine.MoveCard(this, PlayerOwner, GameBoardZone.Board, PlayerOwner, GameBoardZone.Graveyard);
        }


        /// <summary>
        /// IDamageable GetHealed
        /// </summary>
        /// <param name="heal">The amount of health points to heal</param>
        public void GetHealed(int heal)
        {
            if (CurrentHealth == MaxHealth)
            {
                //do nothing
                return;
            }

            if (CurrentHealth + heal > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            else
            {
                CurrentHealth += heal;
            }

            GameEventManager.Healed(this, heal);
        }
    }
}
