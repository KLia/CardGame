using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards
{
    public class Minion : Card, IDamageable
    {
        public Minion() : this("", 0, null, 0, 0, CardSubType.None, null)
        {
        }

        public Minion(IPlayer player, List<CardTrigger> triggers) : this("", 0, player, 0, 0, CardSubType.None, triggers)
        {
        }

        public Minion(string name, int cost, IPlayer player) : this(name, cost, player, 0, 0, CardSubType.None, null)
        {
        }

        public Minion(string name, int cost, IPlayer player, int attack, int health, CardSubType subType, List<CardTrigger> triggers) : 
            base(name, cost, player, CardType.Minion, subType, triggers)
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
