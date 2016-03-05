using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameConsoleTestApp.DTO.Interfaces
{
    public interface ITriggerable
    {
        event EventHandler Attacking;
        event EventHandler Healed;
        event EventHandler GetHit;
        event EventHandler Death;

        void OnAttacking(EventArgs e);
        void OnHealed(EventArgs e);
        void OnGetHit(EventArgs e);
        void OnDeath(EventArgs e);
    }
}
