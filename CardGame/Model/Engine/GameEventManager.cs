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

            Healed += OnHealed;
            onHealedListeners = new List<Tuple<ICard, HealedEventHandler>>();

            GetHit += OnGetHit;
            onGetHitListeners = new List<Tuple<ICard, GetHitEventHandler>>();

            Death += OnDeath;
            onDeathEventListeners = new List<Tuple<ICard, DeathEventHandler>>();

            CardPlayed += OnCardPlayed;
            onCardPlayedListeners = new List<Tuple<ICard, CardPlayedHandler>>();

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
            onHealedListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onGetHitListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onDeathEventListeners.RemoveAll(c => c.Item1.Id == card.Id);
            onCardPlayedListeners.RemoveAll(c => c.Item1.Id == card.Id);
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

        public delegate void AttackEventHandler(IAttacker attacker, IDamageable target, out bool abort);

        public static AttackEventHandler Attack;
        internal static List<Tuple<ICard, AttackEventHandler>> onAttackListeners;

        public delegate void OtherAttackEventHandler(IAttacker attacker, IDamageable target, out bool abort);

        public delegate void HealedEventHandler(IDamageable target, int heal);

        public static HealedEventHandler Healed;
        internal static List<Tuple<ICard, HealedEventHandler>> onHealedListeners;

        public delegate void GetHitEventHandler(IDamageable target, int damage);

        public static GetHitEventHandler GetHit;
        internal static List<Tuple<ICard, GetHitEventHandler>> onGetHitListeners;

        public delegate void DeathEventHandler(IDamageable card);

        public static DeathEventHandler Death;
        internal static List<Tuple<ICard, DeathEventHandler>> onDeathEventListeners;
        

        public delegate void CardPlayedHandler(ICard card);

        public static CardPlayedHandler CardPlayed;
        internal static List<Tuple<ICard, CardPlayedHandler>> onCardPlayedListeners;

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

        public static void RegisterForEventHealed(ICard card, HealedEventHandler callback)
        {
            onHealedListeners.Add(new Tuple<ICard, HealedEventHandler>(card, callback));
        }

        public static void RegisterForEventGetHit(ICard card, GetHitEventHandler callback)
        {
            onGetHitListeners.Add(new Tuple<ICard, GetHitEventHandler>(card, callback));
        }

        public static void RegisterForEventDeath(ICard card, DeathEventHandler callback)
        {
            onDeathEventListeners.Add(new Tuple<ICard, DeathEventHandler>(card, callback));
        }
        public static void RegisterForEventCardPlayed(ICard card, CardPlayedHandler callback)
        {
            onCardPlayedListeners.Add(new Tuple<ICard, CardPlayedHandler>(card, callback));
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

        public static void OnAttack(IAttacker attacker, IDamageable target, out bool abort)
        {
            abort = false;

            if (!onAttackListeners.Any())
            {
                return;
            }

            var card = attacker as ICard;
            if (card != null)
            {
                var mainCard = onAttackListeners.Find(c => c.Item1.Id == card.Id);
                var listeners = onAttackListeners.Where(c => c.Item1.Id != card.Id).OrderBy(c => c.Item1.PlayOrder).ToList();
                listeners.Insert(0, mainCard);

                foreach (var listener in listeners)
                {
                    listener.Item2(attacker, target, out abort);
                }
            }
        }

        public static void OnHealed(IDamageable target, int heal)
        {
            if (!onHealedListeners.Any())
            {
                return;
            }

            var card = target as ICard;
            if (card != null)
            {
                var mainCard = onHealedListeners.Find(c => c.Item1.Id == card.Id);
                var listeners = onHealedListeners.Where(c => c.Item1.Id != card.Id).OrderBy(c => c.Item1.PlayOrder).ToList();
                listeners.Insert(0, mainCard);

                foreach (var listener in listeners)
                {
                    listener.Item2(target, heal);
                }
            }
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
                var mainCard = onGetHitListeners.Find(c => c.Item1.Id == card.Id);
                var listeners = onGetHitListeners.Where(c => c.Item1.Id != card.Id).OrderBy(c => c.Item1.PlayOrder).ToList();
                listeners.Insert(0, mainCard);

                foreach (var listener in listeners)
                {
                    listener.Item2(target, damage);
                }
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
                var mainCard = onDeathEventListeners.Find(c => c.Item1.Id == card.Id);
                var listeners =
                    onDeathEventListeners.Where(c => c.Item1.Id != card.Id).OrderBy(c => c.Item1.PlayOrder).ToList();
                listeners.Insert(0, mainCard);

                foreach (var listener in listeners)
                {
                    listener?.Item2(target);
                }
            }
        }

        public static void OnCardPlayed(ICard card)
        {
            if (!onCardDrawnListeners.Any())
            {
                return;
            }

            var mainCard = onCardDrawnListeners.Find(c => c.Item1.Id == card.Id);
            var listeners = onCardDrawnListeners.Where(c => c.Item1.Id != card.Id).OrderBy(c => c.Item1.PlayOrder).ToList();
            listeners.Insert(0, mainCard);

            foreach (var listener in listeners)
            {
                listener?.Item2(card);
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