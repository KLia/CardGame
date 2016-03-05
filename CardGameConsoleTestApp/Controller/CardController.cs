using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using CardGameConsoleTestApp.DBML;
using CardGameConsoleTestApp.DTO;

namespace CardGameConsoleTestApp.Controller
{
    public class CardController
    {
        private static CardController _cardController;

        private CardController()
        {
        }

        public static CardController GetInstance()
        {
            return _cardController ?? (_cardController = new CardController());
        }

        public IList<Minion> GetAllMinionsList()
        {
            using (var context = new CardDataContext())
            {
                var minions = (from c in context.Cards
                    join n in context.CardNames on c.Id equals n.CardId
                    where c.Type == (int) CardType.Minion
                    select new Minion()
                    {
                        Name = n.Name,
                        Cost = c.Cost,
                        Attack = c.Attack,
                        Health = c.Health
                    });

                return minions.ToList();
            }
        }

        public IList<Spell> GetAllSpellsList()
        {
            using (var context = new CardDataContext())
            {
                var spells = (from c in context.Cards
                    join n in context.CardNames on c.Id equals n.CardId
                    where c.Type == (int) CardType.Spell
                    select new Spell()
                    {
                        Name = n.Name,
                        Cost = c.Cost
                    });

                return spells.ToList();
            }
        }
    }
}
