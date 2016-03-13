using System;
using CardGameConsoleTestApp.DTO.Interfaces;

namespace CardGameConsoleTestApp.DTO
{
    public class Minion : Card, IDamageable, ITriggerable
    {
        private int _currentHealth;

        public Minion() : this("", 0, 0, 0)
        {
        }

        public Minion(string name, int cost) : this(name, cost, 0, 0)
        {
        }

        public Minion(string name, int cost, int attack, int health) : base(name, cost)
        {
            Attack = attack;
            CurrentAttack = attack;
            Health = health;
            CurrentTotalHealth = health;
            _currentHealth = Health;
            IsDead = false;
        }

        public int Attack { get; set; }
        public int CurrentAttack { get; set; }

        public int Health { get; set; }
        public int CurrentTotalHealth { get; set; }
        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                if (value < _currentHealth)
                {
                    OnGetHit(this, new EventArgs());
                }
                else if (value > _currentHealth)
                {
                    OnHealed(this, new EventArgs());
                }


                if (value < 0)
                {
                    _currentHealth = 0;
                    IsDead = true;
                    OnDeath(this, new EventArgs());
                }
                else if (value > CurrentTotalHealth)
                {
                    _currentHealth = CurrentTotalHealth;
                }
                else
                {
                    _currentHealth = value;
                }
            }
        }

        public bool IsDead { get; set; }

        public override event EventHandler CardDrawn;
        public override event EventHandler CardPlayed;
        public override event EventHandler RoundStart;
        public override event EventHandler RoundEnd;
        public event EventHandler Attacking;
        public event EventHandler Healed;
        public event EventHandler GetHit;
        public event EventHandler Death;

        public override void OnCardDrawn(object sender, EventArgs e)
        {
            CardDrawn?.Invoke(sender, e);
        }

        public override void OnCardPlayed(object sender, EventArgs e)
        {
            CardPlayed?.Invoke(sender, e);
        }

        public override void OnRoundStart(object sender, EventArgs e)
        {
            RoundStart?.Invoke(sender, e);
        }

        public override void OnRoundEnd(object sender, EventArgs e)
        {
            RoundEnd?.Invoke(sender, e);
        }

        public void OnAttacking(object sender, EventArgs e)
        {
            Attacking?.Invoke(sender, e);
        }

        public void OnHealed(object sender, EventArgs e)
        {
            Healed?.Invoke(sender, e);
        }

        public void OnGetHit(object sender, EventArgs e)
        {
            GetHit?.Invoke(sender, e);
        }

        public void OnDeath(object sender, EventArgs e)
        {
            Death?.Invoke(sender, e);
        }
    }
}
