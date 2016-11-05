using System;
using System.Collections.Generic;
using CardGame.Model.Cards;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Decks;
using CardGame.Model.Decks.Interfaces;
using CardGame.Model.Engine;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Players
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalMana { get; set; }
        public int CurrentMana { get; set; }
        public AreaBuff AreaBuffs { get; set; }
        public IDeck Deck { get; set; }

        public List<ICard> CardsInHand { get; set; }
        public List<ICard> CardsInPlay { get; set; }
        public List<ICard> CardsInGraveyard { get; set; }

        public Player(int id, string name, IDeck deck) : this(id, name, 0, deck, new AreaBuff())
        {
        }

        public Player(int id, string name, int mana, IDeck deck) : this(id, name, mana, deck, new AreaBuff())
        {
        }

        public Player(int id, string name, int mana, IDeck deck, AreaBuff areaBuffs)
        {
            Id = id;
            Deck = deck;
            Name = name;
            TotalMana = mana;
            CurrentMana = mana;
            AreaBuffs = areaBuffs;
            CardsInHand = new List<ICard>(GameConstants.MAX_CARDS_IN_HAND);
            CardsInPlay = new List<ICard>(GameConstants.MAX_CARDS_IN_PLAY);
            CardsInGraveyard = new List<ICard>();
        }

        public ICard DrawCard(bool isMulligan = false)
        {
            var card = Deck.DrawCard();

            //if player already has maximum  number of cards in hand, burn this card
            if (CardsInHand.Count == GameConstants.MAX_CARDS_IN_HAND)
            {
                throw new InvalidOperationException($"Cannot have more than {GameConstants.MAX_CARDS_IN_HAND} cards in hand");
            }

            //add card to hand
            CardsInHand.Add(card);

            //only fire the is card drawn if it's not the mulligan stage
            if (!isMulligan)
            {
                GameEventManager.OnCardDrawn(card);
            }

            return card;
        }

        public List<ICard> DrawCards(int count, bool isMulligan = false)
        {
            //todo = handle case of no more cards to draw
            var cards = Deck.DrawCards(count);
            CardsInHand.AddRange(cards);

            if (!isMulligan)
            {
                foreach (var card in cards)
                {
                    GameEventManager.CardDrawn(card);
                }
            }

            return cards;
        }

        private void CastSpell(ICard card, IDamageable target)
        {

        }

       
    }
}
