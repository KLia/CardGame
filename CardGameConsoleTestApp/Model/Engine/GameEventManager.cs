﻿using System;
using System.Collections.Generic;
using System.Linq;
using CardGameBackend.Model.Cards.Interfaces;
using CardGameBackend.Model.Players;

namespace CardGameBackend.Model.Engine
{
    public class GameEventManager
    {
        public static void Initialize()
        {
            TurnStart += OnTurnStart;
            onTurnStartListeners = new List<Tuple<ICard, TurnStartHandler>>();

            TurnEnd += OnTurnEnd;
            onTurnEndListeners = new List<Tuple<ICard, TurnEndHandler>>();

            CardDrawn += OnCardDrawn;
            onCardDrawnListeners = new List<Tuple<ICard, CardDrawnHandler>>();

            Attack += OnAttack;
            onAttackListeners = new List<Tuple<ICard, AttackEventHandler>>();

            OtherAttack += OnOtherAttack;
            onOtherAttackListeners = new List<Tuple<ICard, OtherAttackEventHandler>>();

            Healed += OnHealed;
            onHealedListeners = new List<Tuple<ICard, HealedEventHandler>>();

            OtherHealed += OnOtherHealed;
            onOtherHealedListeners = new List<Tuple<ICard, OtherHealedEventHandler>>();

            GetHit += OnGetHit;
            onGetHitListeners = new List<Tuple<ICard, GetHitEventHandler>>();

            OtherGetHit += OnOtherGetHit;
            onOtherGetHitListeners = new List<Tuple<ICard, OtherGetHitEventHandler>>();

            Death += OnDeath;
            onDeathEventListeners = new List<Tuple<ICard, DeathEventHandler>>();

            OtherDeath += OnOtherDeath;
            onOtherDeathEventListeners = new List<Tuple<ICard, OtherDeathEventHandler>>();

            CardPlayed += OnCardPlayed;
            onCardPlayedListeners = new List<Tuple<ICard, CardPlayedHandler>>();

            OtherCardPlayed += OnOtherCardPlayed;
            onOtherCardPlayedListeners = new List<Tuple<ICard, OtherCardPlayedHandler>>();

            SpellCast += OnSpellCast;
            onSpellCastListeners = new List<Tuple<ICard, SpellCastHandler>>();

            SpellTarget += OnSpellTarget;
            onSpellTargetListeners = new List<Tuple<ICard, SpellTargetHandler>>();
        }

        public static void UnregisterForEvents(ICard card)
        {
            onTurnStartListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onTurnEndListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onCardDrawnListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onAttackListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onOtherAttackListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onHealedListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onOtherHealedListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onGetHitListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onOtherGetHitListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onDeathEventListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onOtherDeathEventListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onCardPlayedListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onOtherCardPlayedListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onSpellCastListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onSpellTargetListeners.RemoveAll(c => c.Item1.Id == card.Id);
        }


        //Event Handlers
        public delegate void TurnStartHandler();

        public static TurnStartHandler TurnStart;
        internal static List<Tuple<ICard, TurnStartHandler>> onTurnStartListeners;

        public delegate void TurnEndHandler();

        public static TurnEndHandler TurnEnd;
        internal static List<Tuple<ICard, TurnEndHandler>> onTurnEndListeners;

        public delegate void CardDrawnHandler(IPlayer player, ICard card);

        public static CardDrawnHandler CardDrawn;
        internal static List<Tuple<ICard, CardDrawnHandler>> onCardDrawnListeners;

        public delegate void AttackEventHandler();

        public static AttackEventHandler Attack;
        internal static List<Tuple<ICard, AttackEventHandler>> onAttackListeners;

        public delegate void OtherAttackEventHandler();

        public static OtherAttackEventHandler OtherAttack;
        internal static List<Tuple<ICard, OtherAttackEventHandler>> onOtherAttackListeners;

        public delegate void HealedEventHandler();

        public static HealedEventHandler Healed;
        internal static List<Tuple<ICard, HealedEventHandler>> onHealedListeners;

        public delegate void OtherHealedEventHandler();

        public static OtherHealedEventHandler OtherHealed;
        internal static List<Tuple<ICard, OtherHealedEventHandler>> onOtherHealedListeners;

        public delegate void GetHitEventHandler();

        public static GetHitEventHandler GetHit;
        internal static List<Tuple<ICard, GetHitEventHandler>> onGetHitListeners;

        public delegate void OtherGetHitEventHandler();

        public static OtherGetHitEventHandler OtherGetHit;
        internal static List<Tuple<ICard, OtherGetHitEventHandler>> onOtherGetHitListeners;

        public delegate void DeathEventHandler();

        public static DeathEventHandler Death;
        internal static List<Tuple<ICard, DeathEventHandler>> onDeathEventListeners;

        public delegate void OtherDeathEventHandler();

        public static OtherDeathEventHandler OtherDeath;
        internal static List<Tuple<ICard, OtherDeathEventHandler>> onOtherDeathEventListeners;

        public delegate void CardPlayedHandler();

        public static CardPlayedHandler CardPlayed;
        internal static List<Tuple<ICard, CardPlayedHandler>> onCardPlayedListeners;

        public delegate void OtherCardPlayedHandler();

        public static OtherCardPlayedHandler OtherCardPlayed;
        internal static List<Tuple<ICard, OtherCardPlayedHandler>> onOtherCardPlayedListeners;

        public delegate void SpellCastHandler();

        public static SpellCastHandler SpellCast;
        internal static List<Tuple<ICard, SpellCastHandler>> onSpellCastListeners;

        public delegate void SpellTargetHandler();

        public static SpellTargetHandler SpellTarget;
        internal static List<Tuple<ICard, SpellTargetHandler>> onSpellTargetListeners;

        //Register for events
        public static void RegisterForEventTurnStart(ICard self, TurnStartHandler callback)
        {
            onTurnStartListeners.Add(new Tuple<ICard, TurnStartHandler>(self, callback));
        }

        public static void RegisterForEventTurnEnd(ICard self, TurnEndHandler callback)
        {
            onTurnEndListeners.Add(new Tuple<ICard, TurnEndHandler>(self, callback));
        }

        public static void RegisterForEventCardDrawn(ICard self, CardDrawnHandler callback)
        {
            onCardDrawnListeners.Add(new Tuple<ICard, CardDrawnHandler>(self, callback));
        }

        public static void RegisterForEventAttack(ICard self, AttackEventHandler callback)
        {
            onAttackListeners.Add(new Tuple<ICard, AttackEventHandler>(self, callback));
        }

        public static void RegisterForEventOtherAttack(ICard self, OtherAttackEventHandler callback)
        {
            onOtherAttackListeners.Add(new Tuple<ICard, OtherAttackEventHandler>(self, callback));
        }

        public static void RegisterForEventHealed(ICard self, HealedEventHandler callback)
        {
            onHealedListeners.Add(new Tuple<ICard, HealedEventHandler>(self, callback));
        }

        public static void RegisterForEventOtherHealed(ICard self, OtherHealedEventHandler callback)
        {
            onOtherHealedListeners.Add(new Tuple<ICard, OtherHealedEventHandler>(self, callback));
        }

        public static void RegisterForEventGetHit(ICard self, GetHitEventHandler callback)
        {
            onGetHitListeners.Add(new Tuple<ICard, GetHitEventHandler>(self, callback));
        }

        public static void RegisterForEventOtherGetHit(ICard self, OtherGetHitEventHandler callback)
        {
            onOtherGetHitListeners.Add(new Tuple<ICard, OtherGetHitEventHandler>(self, callback));
        }

        public static void RegisterForEventDeath(ICard self, DeathEventHandler callback)
        {
            onDeathEventListeners.Add(new Tuple<ICard, DeathEventHandler>(self, callback));
        }

        public static void RegisterForEventOtherDeath(ICard self, OtherDeathEventHandler callback)
        {
            onOtherDeathEventListeners.Add(new Tuple<ICard, OtherDeathEventHandler>(self, callback));
        }

        public static void RegisterForEventCardPlayed(ICard self, CardPlayedHandler callback)
        {
            onCardPlayedListeners.Add(new Tuple<ICard, CardPlayedHandler>(self, callback));
        }

        public static void RegisterForEventOtherCardPlayed(ICard self, OtherCardPlayedHandler callback)
        {
            onOtherCardPlayedListeners.Add(new Tuple<ICard, OtherCardPlayedHandler>(self, callback));
        }

        public static void RegisterForEventSpellCast(ICard self, SpellCastHandler callback)
        {
            onSpellCastListeners.Add(new Tuple<ICard, SpellCastHandler>(self, callback));
        }

        public static void RegisterForEventSpellTarget(ICard self, SpellTargetHandler callback)
        {
            onSpellTargetListeners.Add(new Tuple<ICard, SpellTargetHandler>(self, callback));
        }


        //Trigger the event handlers
        public static void OnTurnStart()
        {
            if (!onTurnStartListeners.Any())
            {
                return;
            }

            var sortedListeners = onTurnStartListeners.OrderBy(c => c.Item1.PlayOrder).ToList();
            foreach (var handler in sortedListeners.Select(c => c.Item2))
            {
                handler();
            }
        }

        public static void OnTurnEnd()
        {
            if (!onTurnEndListeners.Any())
            {
                return;
            }

            var sortedListeners = onTurnEndListeners.OrderBy(c => c.Item1.PlayOrder).ToList();
            foreach (var handler in sortedListeners.Select(c => c.Item2))
            {
                handler();
            }
        }

        public static void OnCardDrawn(IPlayer player, ICard card)
        {
            if (!onCardDrawnListeners.Any())
            {
                return;
            }

            var cardListener = onCardDrawnListeners.Find(c => c.Item1.Id == card.Id);
            cardListener?.Item2(player, card);
        }

        public static void OnAttack()
        {
        }

        public static void OnOtherAttack()
        {
        }

        public static void OnHealed()
        {
        }

        public static void OnOtherHealed()
        {
        }

        public static void OnGetHit()
        {
        }

        public static void OnOtherGetHit()
        {
        }

        public static void OnDeath()
        {
        }

        public static void OnOtherDeath()
        {
        }

        public static void OnCardPlayed()
        {
        }

        public static void OnOtherCardPlayed()
        {
        }

        public static void OnSpellCast()
        {
        }

        public static void OnSpellTarget()
        {
        }
    }
}