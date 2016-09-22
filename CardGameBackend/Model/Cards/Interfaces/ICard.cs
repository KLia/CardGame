using System;
using System.Collections.Generic;
using CardGameBackend.Model.Cards.ValueObjects;
using CardGameBackend.Model.Players;

namespace CardGameBackend.Model.Cards.Interfaces
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
