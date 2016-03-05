using System;
using CardGameConsoleTestApp.DTO.Interfaces;

namespace CardGameConsoleTestApp.DTO
{
    public class Minion : Card, IDamageable, ITriggerable
    {
        public Minion() : this("", 0, 0, 0)
        {
        }

        public Minion(string name, int cost) : this(name, cost, 0, 0)
        {
        }

        public Minion(string name, int cost, int attack, int health) : base(name, cost)
        {
            Attack = attack;
            Health = health;
            CurrentHealth = Health;
            IsDead = false;
        }

        public int Attack { get; set; }
        public int Health { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }

        public event EventHandler Attacking;
        public event EventHandler Healed;
        public event EventHandler GetHit;
        public event EventHandler Death;

        public override void OnCardDrawn(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnCardPlayed(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnAttacking(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnHealed(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnGetHit(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnDeath(EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
