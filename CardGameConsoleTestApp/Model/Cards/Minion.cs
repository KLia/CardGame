using System;
using System.Collections.Generic;
using CardGameBackend.Model.Cards.Interfaces;
using CardGameBackend.Model.Cards.ValueObjects;

namespace CardGameBackend.Model.Cards
{
    public class Minion : Card, IDamageable
    {
        public Minion() : this("", 0, 0, 0, CardSubType.None, null)
        {
        }

        public Minion(List<CardTrigger> triggers) : this("", 0, 0, 0, CardSubType.None, triggers)
        {
        }

        public Minion(string name, int cost) : this(name, cost, 0, 0, CardSubType.None, null)
        {
        }

        public Minion(string name, int cost, int attack, int health, CardSubType subType, List<CardTrigger> triggers) : 
            base(name, cost, CardType.Minion, subType, triggers)
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
