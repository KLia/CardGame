using System;

namespace CardGameConsoleTestApp.Model.Cards.Interfaces
{
    public interface ITriggerable
    {
        int Id { get; set; }

        event EventHandler Attacking;
        event EventHandler Healed;
        event EventHandler GetHit;
        event EventHandler Death;

        void OnAttacking(object sender, EventArgs e);
        void OnHealed(object sender, EventArgs e);
        void OnGetHit(object sender, EventArgs e);
        void OnDeath(object sender, EventArgs e);
    }
}
