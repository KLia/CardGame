using CardGameBackend.Model.Cards.Interfaces;
using CardGameBackend.Model.Players;

namespace CardGameBackend.Controller
{
    public static class TriggersController
    {
        public static void DealDamage(IDamageable target, int damage)
        {
            target.CurrentHealth -= damage;
            if (target.CurrentHealth < 0)
            {
                target.CurrentHealth = 0;
                target.IsDead = true;
            }
        }

        public static void Heal(IDamageable target, int heal)
        {
            target.CurrentHealth += heal;
        }
    }
}