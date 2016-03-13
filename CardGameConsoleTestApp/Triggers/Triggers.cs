using CardGameConsoleTestApp.Cards.Interfaces;

namespace CardGameConsoleTestApp.Triggers
{
    public static class Triggers
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
