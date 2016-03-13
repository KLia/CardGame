using System;

namespace CardGameConsoleTestApp.Cards.Interfaces
{
    public interface ITriggerable
    {
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
