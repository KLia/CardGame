using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Decks.Interfaces;
using CardGame.Model.Engine;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Decks
{
    public class GameDeck : IGameDeck
    {
        private readonly List<ICard> _cards;
        private GameDeckContext _filter;

        public GameDeck(IEnumerable<ICard> p1Cards, IEnumerable<ICard> p2Cards)
        {
            _cards = p1Cards.Union(p2Cards).ToList();
        }

        public List<ICard> GetCards()
        {
            return _cards;
        }

        public void AddCard(ICard card)
        {
            _cards.Add(card);
        }

        public GameDeckContext Filter()
        {
            _filter = new GameDeckContext(_cards);
            return _filter;
        }
    }
}
