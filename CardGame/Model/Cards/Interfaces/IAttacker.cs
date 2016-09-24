using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Model.Cards.Interfaces
{
    public interface IAttacker
    {
        int CurrentAttack { get; }
        void AttackTarget(IDamageable target);
    }
}
