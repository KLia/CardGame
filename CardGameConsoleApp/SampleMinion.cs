using System;
using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGameConsoleApp
{
    public class SampleMinion : Minion, ITriggerable
    {
        public SampleMinion()
        {
            BaseCost = 1;
            BaseAttack = 1;
            BaseHealth = 1;
        }

        public void OnTurnStart(IPlayer player)
        {
            if (player.TotalMana < GameConstants.TOTAL_MANA)
            {
                player.TotalMana++;
            }
        }

        public void OnSummon(ICard card)
        {
            if (card == this)
            {
                if (((Minion) card).PlayerOwner.CurrentMana < GameConstants.TOTAL_MANA)
                {
                    ((Minion) card).PlayerOwner.CurrentMana++;
                }
            }
        }

        public TriggerType TriggerTypes => TriggerType.OnTurnStart;

        public void AttachEvent()
        {
            GameEventManager.RegisterForEventTurnStart(this, OnTurnStart);
            GameEventManager.RegisterForEventCardPlayed(this, OnSummon);
        }

        public new void Silence()
        {
            base.Silence();
            OnSummon(this);
        }
    }
}
