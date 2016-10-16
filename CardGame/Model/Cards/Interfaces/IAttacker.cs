using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Model.Cards.Interfaces
{
    public interface IAttacker
    {bool CanAttack { get; }
        int CurrentAttack { get; }
        void ResetTemporaryAttackBuff();
        void AttackTarget(IDamageable target);
    }
}
