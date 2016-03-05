using System;

namespace CardGameConsoleTestApp.DTO.Interfaces
{
    public interface ICard
    {
        event EventHandler CardDrawn;
        event EventHandler CardPlayed;

        void OnCardDrawn(EventArgs e);
        void OnCardPlayed(EventArgs e);
    }
}
