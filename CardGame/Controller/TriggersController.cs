using CardGame.Model.Cards.Interfaces;

namespace CardGame.Controller
{
    public static class TriggersController
    {
        public static void DealDamage(IDamageable target, int damage)
        {
            target.BaseHealth -= damage;
            if (target.CurrentHealth < 0)
            {
                target.CurrentHealth = 0;
                target.TemporaryHealthBuff = 0;
                target.PermanentHealthBuff = 0;
                target.IsDead = true;
            }
        }

        public static void Heal(IDamageable target, int heal)
        {
            target.CurrentHealth += heal;
        }
    }
}