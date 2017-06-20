using System;
using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGameConsoleApp
{
    public class SampleMinion : Minion
    {
        private int turnPlayed;
        public SampleMinion()
        {
            BaseCost = 1;
            BaseAttack = 1;
            BaseHealth = 1;
        }

        public override void OnTurnStart(IPlayer player)
        {
            if (player.TotalMana < GameConstants.TOTAL_MANA)
            {
                player.TotalMana++;
            }
        }

        public override void OnMinionSummoned(IMinion card, IDamageable target)
        {
            if (card == this)
            {
                if (((Minion) card).PlayerOwner.CurrentMana < GameConstants.TOTAL_MANA)
                {
                    ((Minion) card).PlayerOwner.CurrentMana++;
                }

                turnPlayed = GameEngine.GameState.Turn;
            }
        }

        public new TriggerType TriggerTypes => TriggerType.OnTurnStart;

        public void AttachEvent()
        {
            GameEventManager.RegisterForEventTurnStart(this, OnTurnStart);
            GameEventManager.RegisterForEventCardPlayed(this, OnMinionSummoned);
        }

        public new void Silence()
        {
            base.Silence();
            OnMinionSummoned(this);
        }

        public void OnMinionSummoned(ICard minion)
        {
            
        }

        public void OnTurnEnd(ICard card)
        {
            if (GameEngine.GameState.Turn == turnPlayed + 1)
            {
                //do stuff
            }
        }
    }
}
