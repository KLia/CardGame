using System;
using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public abstract class Minion : Card, IMinion
    {
        protected Minion()
        {
            Type = CardType.Minion;
            
            TemporaryAttackBuff = 0;
            PermanentAttackBuff = 0;

            TemporaryHealthBuff = 0;
            PermanentHealthBuff = 0;
            IsDead = false;

            StatusEffects |= StatusEffect.Exhausted;
        }

        public int BaseAttack { get; set; }
        public int TemporaryAttackBuff { get; set; }
        public int PermanentAttackBuff { get; set; }

        public int CurrentAttack => Math.Min(0, BaseAttack + TemporaryAttackBuff + PermanentAttackBuff + PlayerOwner.AreaBuffs.AreaAttackBuff);

        public int BaseHealth { get; set; }
        public int TemporaryHealthBuff { get; set; }
        public int PermanentHealthBuff { get; set; }
        public int MaxHealth => BaseHealth + TemporaryHealthBuff + PermanentHealthBuff + PlayerOwner.AreaBuffs.AreaHealthBuff;
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }

        public bool CanAttack => !StatusEffects.HasFlag(StatusEffect.Exhausted | StatusEffect.CantAttack);

        public override void PlayCard(int boardPos, IDamageable target = null)
        {
            if (PlayerOwner.CardsInPlay.Count == GameConstants.MAX_CARDS_IN_PLAY)
            {
                throw new InvalidOperationException(
                    $"Cannot have more than {GameConstants.MAX_CARDS_IN_PLAY} cards in play");
            }
            
            //PlayerOwner.MoveCard(this, GameBoardZone.Hand, GameBoardZone.Board, boardPos);

            //Trigger the on MinionSummoned Event 
            GameEventManager.MinionSummoned(this, target);
        }

        /// <summary>
        /// Add StatusEffects to this Minion
        /// </summary>
        private StatusEffect StatusEffects { get; set; }

        public void ApplyStatusEffect(StatusEffect effects)
        {
            StatusEffects |= effects;
        }

        /// <summary>
        /// Remove StatusEffects from this Minion
        /// </summary>
        public void RemoveStatusEffect(StatusEffect effects)
        {
            StatusEffects &= ~effects;
        }


        /// <summary>
        /// Reset the Temporary Health Buffs and adjust CurrentHealth
        /// </summary>
        public void ResetTemporaryHealthBuff()
        {
            CurrentHealth -= TemporaryHealthBuff;
            TemporaryHealthBuff = 0;

            //just in case the minion dies when the buffs wear off
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Reset the Temporary Attack Buffs
        /// </summary>
        public void ResetTemporaryAttackBuff()
        {
            TemporaryHealthBuff = 0;
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

        /// <summary>
        /// Removes and StatusEffects, TriggerEvents, Temporary/Permanent Buffs
        /// </summary>
        public void Silence()
        {
            StatusEffects.ResetAll();
            ResetTemporaryAttackBuff();
            ResetTemporaryHealthBuff();
            GameEventManager.UnregisterForEvents(this);
        }
    }
}
