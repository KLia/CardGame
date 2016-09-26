using System;
using CardGame.Model.Cards;

namespace CardGameConsoleApp
{
    public class SampleMinion : Minion
    {
        public SampleMinion()
        {
            BaseCost = 0;
        }
        public override void AttachEvents()
        {
        }
    }
}
