using System;

namespace CardGameConsoleTestApp.DTO.Interfaces
{
    public interface ICard
    {
        event EventHandler CardDrawn;
        event EventHandler CardPlayed;
        event EventHandler RoundStart;
        event EventHandler RoundEnd;

        void OnCardDrawn(object sender, EventArgs e);
        void OnCardPlayed(object sender, EventArgs e);
        void OnRoundStart(object sender, EventArgs e);
        void OnRoundEnd(object sender, EventArgs e);
    }
}
