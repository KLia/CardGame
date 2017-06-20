using System;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;

namespace CardGame.Model.Engine
{
    public class GameEventRegistrator
    {
        public static void RegisterEvents(ITriggerable triggerable, Type baseType)
        {
            foreach (var triggerType in TriggerTypeExtensions.GetAllTriggerTypes())
            {
                //Check the type of "triggerable" object, compared to typeof(BaseClass)
                //if different, then the method was implemented by the extending class, and we register its event
                if (triggerable.GetType().GetMethod(triggerType.ToString()).DeclaringType == baseType)
                {
                    continue;
                }

                switch (triggerType)
                {
                    case TriggerType.None:
                        break;
                    case TriggerType.OnTurnStart:
                        GameEventManager.RegisterForEventTurnStart(triggerable as ICard, triggerable.OnTurnStart);
                        break;
                    case TriggerType.OnTurnEnd:
                        GameEventManager.RegisterForEventTurnEnd(triggerable as ICard, triggerable.OnTurnEnd);
                        break;
                    case TriggerType.OnCardDrawn:
                        GameEventManager.RegisterForEventCardDrawn(triggerable as ICard, triggerable.OnCardDrawn);
                        break;
                    case TriggerType.OnAttack:
                        GameEventManager.RegisterForEventAttack(triggerable as ICard, triggerable.OnAttack);
                        break;
                    case TriggerType.OnHealed:
                        GameEventManager.RegisterForEventHealed(triggerable as ICard, triggerable.OnHealed);
                        break;
                    case TriggerType.OnGetHit:
                        GameEventManager.RegisterForEventGetHit(triggerable as ICard, triggerable.OnGetHit);
                        break;
                    case TriggerType.OnDeath:
                        GameEventManager.RegisterForEventDeath(triggerable as ICard, triggerable.OnDeath);
                        break;
                    case TriggerType.OnCardPlayed:
                        GameEventManager.RegisterForEventCardPlayed(triggerable as ICard, triggerable.OnCardPlayed);
                        break;
                    case TriggerType.OnMinionSummoned:
                        GameEventManager.RegisterForEventMinionSummoned(triggerable as ICard, triggerable.OnMinionSummoned);
                        break;
                    case TriggerType.OnSpellCast:
                        GameEventManager.RegisterForEventSpellCast(triggerable as ICard, triggerable.OnSpellCast);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}