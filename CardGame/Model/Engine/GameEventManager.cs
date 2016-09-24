using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;

namespace CardGame.Model.Engine
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

        public delegate void CardDrawnHandler(ICard card);

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

        public delegate void GetHitEventHandler(IDamageable target, int damage);

        public static GetHitEventHandler GetHit;
        internal static List<Tuple<ICard, GetHitEventHandler>> onGetHitListeners;

        public delegate void OtherGetHitEventHandler(IDamageable target, int damage);

        public static OtherGetHitEventHandler OtherGetHit;
        internal static List<Tuple<ICard, OtherGetHitEventHandler>> onOtherGetHitListeners;

        public delegate void DeathEventHandler(IDamageable card);

        public static DeathEventHandler Death;
        internal static List<Tuple<ICard, DeathEventHandler>> onDeathEventListeners;

        public delegate void OtherDeathEventHandler(IDamageable card);

        public static OtherDeathEventHandler OtherDeath;
        internal static List<Tuple<ICard, OtherDeathEventHandler>> onOtherDeathEventListeners;

        public delegate void CardPlayedHandler(Minion card);

        public static CardPlayedHandler CardPlayed;
        internal static List<Tuple<ICard, CardPlayedHandler>> onCardPlayedListeners;

        public delegate void OtherCardPlayedHandler(ICard card);

        public static OtherCardPlayedHandler OtherCardPlayed;
        internal static List<Tuple<ICard, OtherCardPlayedHandler>> onOtherCardPlayedListeners;

        public delegate void SpellCastHandler(Spell spell, IDamageable target, out bool abort);

        public static SpellCastHandler SpellCast;
        internal static List<Tuple<ICard, SpellCastHandler>> onSpellCastListeners;

        public delegate void SpellTargetHandler(IDamageable target);

        public static SpellTargetHandler SpellTarget;
        internal static List<Tuple<ICard, SpellTargetHandler>> onSpellTargetListeners;

        //Register for events
        public static void RegisterForEventTurnStart(ICard card, TurnStartHandler callback)
        {
            onTurnStartListeners.Add(new Tuple<ICard, TurnStartHandler>(card, callback));
        }

        public static void RegisterForEventTurnEnd(ICard card, TurnEndHandler callback)
        {
            onTurnEndListeners.Add(new Tuple<ICard, TurnEndHandler>(card, callback));
        }

        public static void RegisterForEventCardDrawn(ICard card, CardDrawnHandler callback)
        {
            onCardDrawnListeners.Add(new Tuple<ICard, CardDrawnHandler>(card, callback));
        }

        public static void RegisterForEventAttack(ICard card, AttackEventHandler callback)
        {
            onAttackListeners.Add(new Tuple<ICard, AttackEventHandler>(card, callback));
        }

        public static void RegisterForEventOtherAttack(ICard card, OtherAttackEventHandler callback)
        {
            onOtherAttackListeners.Add(new Tuple<ICard, OtherAttackEventHandler>(card, callback));
        }

        public static void RegisterForEventHealed(ICard card, HealedEventHandler callback)
        {
            onHealedListeners.Add(new Tuple<ICard, HealedEventHandler>(card, callback));
        }

        public static void RegisterForEventOtherHealed(ICard card, OtherHealedEventHandler callback)
        {
            onOtherHealedListeners.Add(new Tuple<ICard, OtherHealedEventHandler>(card, callback));
        }

        public static void RegisterForEventGetHit(ICard card, GetHitEventHandler callback)
        {
            onGetHitListeners.Add(new Tuple<ICard, GetHitEventHandler>(card, callback));
        }

        public static void RegisterForEventOtherGetHit(ICard card, OtherGetHitEventHandler callback)
        {
            onOtherGetHitListeners.Add(new Tuple<ICard, OtherGetHitEventHandler>(card, callback));
        }

        public static void RegisterForEventDeath(ICard card, DeathEventHandler callback)
        {
            onDeathEventListeners.Add(new Tuple<ICard, DeathEventHandler>(card, callback));
        }

        public static void RegisterForEventOtherDeath(ICard card, OtherDeathEventHandler callback)
        {
            onOtherDeathEventListeners.Add(new Tuple<ICard, OtherDeathEventHandler>(card, callback));
        }

        public static void RegisterForEventCardPlayed(ICard card, CardPlayedHandler callback)
        {
            onCardPlayedListeners.Add(new Tuple<ICard, CardPlayedHandler>(card, callback));
        }

        public static void RegisterForEventOtherCardPlayed(ICard card, OtherCardPlayedHandler callback)
        {
            onOtherCardPlayedListeners.Add(new Tuple<ICard, OtherCardPlayedHandler>(card, callback));
        }

        public static void RegisterForEventSpellCast(ICard card, SpellCastHandler callback)
        {
            onSpellCastListeners.Add(new Tuple<ICard, SpellCastHandler>(card, callback));
        }

        public static void RegisterForEventSpellTarget(ICard card, SpellTargetHandler callback)
        {
            onSpellTargetListeners.Add(new Tuple<ICard, SpellTargetHandler>(card, callback));
        }


        //Trigger the event handlers
        public static void OnTurnStart()
        {
            if (!onTurnStartListeners.Any())
            {
                return;
            }

            var listeners = onTurnStartListeners.OrderBy(c => c.Item1.PlayOrder).ToList();
            foreach (var listener in listeners.Select(c => c.Item2))
            {
                listener();
            }
        }

        public static void OnTurnEnd()
        {
            if (!onTurnEndListeners.Any())
            {
                return;
            }

            var listeners = onTurnEndListeners.OrderBy(c => c.Item1.PlayOrder).ToList();
            foreach (var listener in listeners.Select(c => c.Item2))
            {
                listener();
            }
        }

        public static void OnCardDrawn(ICard card)
        {
            if (!onCardDrawnListeners.Any())
            {
                return;
            }

            var listener = onCardDrawnListeners.Find(c => c.Item1.Id == card.Id);
            listener?.Item2(card);
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

        public static void OnGetHit(IDamageable target, int damage)
        {
            if (!onGetHitListeners.Any())
            {
                return;
            }

            var card = target as ICard;
            if (card != null)
            {
                var listener = onGetHitListeners.Find(c => c.Item1.Id == card.Id);
                listener.Item2(target, damage);
            }
        }

        public static void OnOtherGetHit(IDamageable target, int damage)
        {
            if (!onOtherGetHitListeners.Any())
            {
                return;
            }

            var card = target as ICard;
            if (card != null)
            {
                var listener = onOtherGetHitListeners.Find(c => c.Item1.Id == card.Id);
                listener.Item2(target, damage);
            }
        }

        public static void OnDeath(IDamageable target)
        {
            if (!onDeathEventListeners.Any())
            {
                return;
            }

            var card = target as ICard;
            if (card != null)
            {
                var listener = onDeathEventListeners.Find(c => c.Item1.Id == card.Id);
                listener?.Item2(target);
            }
        }

        public static void OnOtherDeath(IDamageable target)
        {
            if (!onOtherDeathEventListeners.Any())
            {
                return;
            }

            var card = target as ICard;
            if (card != null)
            {
                var listener = onOtherDeathEventListeners.Find(c => c.Item1.Id == card.Id);
                listener?.Item2(target);
            }
        }

        public static void OnCardPlayed(ICard card)
        {
            if (!onCardDrawnListeners.Any())
            {
                return;
            }

            var listener = onCardDrawnListeners.Find(c => c.Item1.Id == card.Id);
            listener?.Item2(card);
        }

        public static void OnOtherCardPlayed(ICard card)
        {
            if (!onOtherCardPlayedListeners.Any())
            {
                return;
            }

            var listeners = onOtherCardPlayedListeners.OrderBy(c => c.Item1.PlayOrder).ToList();
            foreach (var listener in listeners)
            {
                listener.Item2(card);
            }
        }

        public static void OnSpellCast(Spell spell, IDamageable target, out bool abort)
        {
            abort = false; 

            if (!onSpellCastListeners.Any())
            {
                return;
            }

            var listeners = onSpellCastListeners.OrderBy(c => c.Item1.PlayOrder).ToList();
            foreach (var listener in listeners)
            {
                listener.Item2(spell, target, out abort);
            }
        }

        public static void OnSpellTarget(IDamageable target)
        {
            if (!onSpellTargetListeners.Any())
            {
                return;
            }

            var card = target as ICard;
            if (card != null)
            {
                var listener = onSpellTargetListeners.Find(c => c.Item1.Id == card.Id);
                listener.Item2(target);
            }
        }
    }
}