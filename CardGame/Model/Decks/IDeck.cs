﻿using System.Collections.Generic;
using CardGame.Model.Cards.Interfaces;

namespace CardGame.Model.Decks
{
    public interface IDeck
    {
        List<ICard> Cards { get; }
        void AddCard (ICard card);
        void RemoveCard(ICard card);
        void RemoveCard(int index);
        void Shuffle();
        ICard DrawCard();
        List<ICard> DrawCards(int count);
    }
}
