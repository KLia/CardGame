namespace CardGame.Model.Cards.Interfaces
{
    public interface IDamageable
    {
        int BaseHealth { get; set; }
        int TemporaryHealthBuff { get; set; }
        int PermanentHealthBuff { get; set; }
        int CurrentHealth { get; set; }
        bool IsDead { get; set; }
        void TakeDamage (int damage);
        void Die();
        void GetHealed(int heal);
    }
}
