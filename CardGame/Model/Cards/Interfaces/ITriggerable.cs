using CardGame.Model.Cards.ValueObjects;

namespace CardGame.Model.Cards.Interfaces
{
    public interface ITriggerable
    {
        TriggerType TriggerTypes { get; }
        void AttachEvent();
    }
}
