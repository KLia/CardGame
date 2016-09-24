using System.Collections.Generic;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards.Interfaces
{
    public interface ICard
    {
        int Id { get; }
        int PlayOrder { get; set; }
        IPlayer Player { get; set; }
        string Name { get; set; }
        int Cost { get; set; }
        CardType Type { get; set; }
        CardSubType SubType { get; set; }
        List<CardTrigger> Triggers { get; set; }
    }
}
