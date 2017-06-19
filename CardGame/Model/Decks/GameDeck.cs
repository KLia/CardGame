using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Model.Cards.Interfaces;
using CardGame.Model.Cards.ValueObjects;
using CardGame.Model.Decks.Interfaces;
using CardGame.Model.Engine;
using CardGame.Model.Engine.ValueObjects;
using CardGame.Model.Players.Interfaces;

namespace CardGame.Model.Decks
{
    public class GameDeck : IGameDeck
    {
        private readonly List<ICard> _cards;
        private GameDeckContext _filter;
        private Dictionary<IPlayer, GameDeckCardCount> _counts;

        public GameDeck(IPlayer p1, IPlayer p2, IEnumerable<ICard> p1Cards, IEnumerable<ICard> p2Cards)
        {
            _cards = p1Cards.Union(p2Cards).ToList();

            _counts = new Dictionary<IPlayer, GameDeckCardCount>
            {
                {p1, new GameDeckCardCount(p1Cards.Count(), 0, 0, 0)},
                {p2, new GameDeckCardCount(p2Cards.Count(), 0, 0, 0)}
            };
        }

        public IEnumerable<ICard> GetCards()
        {
            return _cards;
        }

        public void AddCard(ICard card)
        {
            _cards.Add(card);
            _counts[card.PlayerOwner].Increment(card.Zone, 1);
        }

        public GameDeckContext Filter()
        {
            _filter = new GameDeckContext(_cards);
            return _filter;
        }

        /// <summary>		      
        /// Moves the card from one board zone to another		
        /// </summary>		
        /// <param name="card">The card to move</param>		
        /// <param name="sourcePlayer">The source Player who currently controls the card</param>		
        /// <param name="sourceZone">From which board zone</param>		
        /// <param name="destPlayer">Card might change ownership</param>
        /// <param name="destZone">To which board zone</param>		
        /// <param name="boardPos">If it is dropped in a specific position, use it</param>		
        /// <param name="isCopy">If it should be copies rather than moved, do not remove it from source zone</param>	
        public void MoveCard(ICard card, IPlayer sourcePlayer, GameBoardZone sourceZone, IPlayer destPlayer,
            GameBoardZone destZone, int boardPos = -1, bool isCopy = false)
        {
            if (card.Zone != sourceZone)
            {
                throw new InvalidOperationException($"Card does not exist in {sourceZone.Stringify()}");
            }

            var handCount = Filter().ByPlayer(sourcePlayer).ByBoardZone(GameBoardZone.Hand).Get().Count();
            if (destZone == GameBoardZone.Hand && handCount >= GameConstants.MAX_CARDS_IN_HAND)
            {
                throw new InvalidOperationException($"{destZone.Stringify()} is full");
            }

            var boardCount = Filter().ByPlayer(destPlayer).ByBoardZone(destZone).Get().Count();
            if (destZone == GameBoardZone.Board && boardCount >= GameConstants.MAX_CARDS_IN_PLAY)
            {
                throw new InvalidOperationException($"{destZone.Stringify()} is full");
            }

            //todo - if is copy, clone the card
            if (isCopy)
            {
            }

            //Arrange sourceZone's card ordering
            if (card.ZonePos > -1)
            { 
                ArrangeCardOrder(sourcePlayer, sourceZone, card.ZonePos, false);
            }

            //insert into the correct position		
            var destCount = Filter().ByPlayer(destPlayer).ByType(card.Type).ByBoardZone(destZone).Get().Count();
            if (boardPos > -1 && boardPos < destCount)
            {
                ArrangeCardOrder(destPlayer, destZone, boardPos, true);
                card.ZonePos = boardPos;
            }
            else
            {
                card.Zone = destZone;
                card.ZonePos = boardPos;
            }

            //if card changes Players, change card's PlayerOwner field
            if (card.PlayerOwner != destPlayer)
            {
                card.PlayerOwner = destPlayer;
            }
        }

        /// <summary>
        /// After playing a new card, we need to arrange the card order on the board to match their new position
        /// (For example, there are 5 cards in play, and a new card is played in position 2. All cards in position 2 
        /// onwards must increment their position to match the new one. So, 2->3, 3->4, 4->5, 5->6).
        /// The same happens if a card is removed from the board, somehow.
        /// </summary>
        /// <param name="player">The player whose board is being affected</param>
        /// <param name="zone">Which player zone (or board)</param>
        /// <param name="boardPos">Which position is the new card played at, or old card is removed from</param>
        /// <param name="isAdd">If yes, increment, else decrement</param>
        private void ArrangeCardOrder(IPlayer player, GameBoardZone zone, int boardPos, bool isAdd)
        {
            //Find all cards with the parameter's criteria, and add 
            _cards.FindAll(c => c.PlayerOwner == player && c.Zone == zone && c.ZonePos >= boardPos)
                .ForEach(c => c.ZonePos = isAdd ? c.ZonePos++ : c.ZonePos--);
        }
    }
}
