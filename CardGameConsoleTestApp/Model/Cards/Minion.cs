using System;
using CardGameConsoleTestApp.Model.Cards.Interfaces;

namespace CardGameConsoleTestApp.Model.Cards
{
    public class Minion : Card, IDamageable
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
            CurrentAttack = attack;
            Health = health;
            CurrentTotalHealth = health;
            CurrentHealth = Health;
            IsDead = false;
        }
        
        public int Attack { get; set; }
        public int CurrentAttack { get; set; }
        
        public int Health { get; set; }
        public int CurrentTotalHealth { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsDead { get; set; }
    }
}
