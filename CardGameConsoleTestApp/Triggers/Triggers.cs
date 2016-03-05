using CardGameConsoleTestApp.DTO.Interfaces;

namespace CardGameConsoleTestApp.Triggers
{
    public class Triggers
    {
        public void DealDamage(IDamageable target, int damage)
        {
            target.CurrentHealth -= damage;
            if (target.CurrentHealth < 0)
            {
                target.IsDead = true;
            }
        }

        public void Heal(IDamageable target, int heal)
        {
            target.CurrentHealth += heal;
            if (target.CurrentHealth > target.Health)
            {
                target.CurrentHealth = target.Health;
            }
        }
    }
}
