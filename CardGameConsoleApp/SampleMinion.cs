using System;
using CardGame.Model.Cards;
using CardGame.Model.Engine;
using CardGame.Model.Players.Interfaces;

namespace CardGameConsoleApp
{
    public class SampleMinion : Minion
    {
        public SampleMinion()
        {
            BaseCost = 1;
            BaseAttack = 1;
            BaseHealth = 1;
        }
        public new void AttachEvents()
        {
        }

        public override void RegisterTriggers()
        {
            GameEventManager.RegisterForEventTurnStart(this, OnTurnStart);
        }

        public void OnTurnStart(IPlayer player)
        {
            player.Mana++;
        }
    }
}
