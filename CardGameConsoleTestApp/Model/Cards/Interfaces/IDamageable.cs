namespace CardGameConsoleTestApp.Model.Cards.Interfaces
{
    public interface IDamageable
    {
        int Health { get; set; }
        int CurrentHealth { get; set; }
        bool IsDead { get; set; }
    }
}
