using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.Model.Engine;

namespace CardGame.Model.Decks
{
    public class GameDeckCardCount
    {
        private int DeckCount;
        private int HandCount;
        private int BoardCount;
        private int GraveyardCount;

        public GameDeckCardCount(int deck, int hand, int board, int graveyard)
        {
            DeckCount = deck;
            HandCount = hand;
            BoardCount = board;
            GraveyardCount = graveyard;
        }

        private void ChangeCount(GameBoardZone zone, int diff)
        {
            switch (zone)
            {
                case GameBoardZone.Deck:
                    DeckCount += diff;
                    break;
                case GameBoardZone.Hand:
                    HandCount += diff;
                    break;
                case GameBoardZone.Board:
                    BoardCount += diff;
                    break;
                case GameBoardZone.Graveyard:
                    GraveyardCount += diff;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(zone), zone, null);
            }
        }

        public void Increment(GameBoardZone zone, int diff)
        {
            if (diff < 0)
            {
                diff = Math.Abs(diff);
            }

            ChangeCount(zone, diff);
        }

        public void Decrement(GameBoardZone zone, int diff)
        {
            if (diff > 0)
            {
                diff *= -1;
            }

            ChangeCount(zone, diff);
        }

        public int GetCount(GameBoardZone zone)
        {
            switch(zone)
            {
                case GameBoardZone.Deck:
                    return DeckCount;
                case GameBoardZone.Hand:
                    return HandCount;
                case GameBoardZone.Board:
                    return BoardCount;
                case GameBoardZone.Graveyard:
                    return GraveyardCount;
                default:
                    throw new ArgumentOutOfRangeException(nameof(zone), zone, null);
            }
        }
    }
}
