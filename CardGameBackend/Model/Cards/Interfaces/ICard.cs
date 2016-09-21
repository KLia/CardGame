using System;

namespace CardGameBackend.Model.Cards.Interfaces
{
    public interface ICard
    {
        int Id { get; }
        int PlayOrder { get; set; }
    }
}
