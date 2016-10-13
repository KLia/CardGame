using System;
using CardGame.Model.Cards;

namespace CardGameConsoleApp
{
    public class SampleMinion : Minion
    {
        public SampleMinion()
        {
            BaseCost = 1;
            BaseAttack = 1;
            BaseHealth = 1;
        }
        public new void AttachEvents()
        {
        }
    }
}
