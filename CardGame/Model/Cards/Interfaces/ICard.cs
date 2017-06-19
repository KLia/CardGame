using System.Collections.Generic;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Cards.Interfaces
{
    public interface ICard
    {
        int Id { get; }
        int PlayOrder { get; set; }
        IPlayer PlayerOwner { get; set; }
        string Name { get; set; }
        int BaseCost { get; set; }
        int CurrentCost { get; }
        int TemporaryCostBuff { get; set; }
        int PermanentCostBuff { get; set; }
        CardType Type { get; set; }
        CardSubType SubType { get; set; }
        GameBoardZone Zone { get; set; }
        int ZonePos { get; set; }
        void PlayCard(int boardPos, IDamageable target = null);
    }
}