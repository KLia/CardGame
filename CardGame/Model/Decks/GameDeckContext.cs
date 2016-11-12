using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Engine;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Decks
{
    public class GameDeckContext
    {
        public IEnumerable<ICard> FilteredCards { get; set; }

        public GameDeckContext(IEnumerable<ICard> cards)
        {
            FilteredCards = cards;
        }

        public IEnumerable<ICard> Get()
        {
            return FilteredCards;
        }

        public GameDeckContext ByPlayer(IPlayer player)
        {
            FilteredCards = FilteredCards.Where(c => c.PlayerOwner == player);
            return this;
        }

        public GameDeckContext ByBoardZone(GameBoardZone boardZone)
        {
            FilteredCards = FilteredCards.Where(c => c.Zone == boardZone);
            return this;
        }

        public GameDeckContext ByType(CardType type)
        {
            FilteredCards = FilteredCards.Where(c => c.Type == type);
            return this;
        }
    }
}
