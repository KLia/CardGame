namespace CardGame.Model.Cards.Interfaces
{
    public interface ISpell : ICard
    {
        void Cast(IDamageable target);
    }
}
