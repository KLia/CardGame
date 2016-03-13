using System;
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
                        Health = c.Health,
                        CurrentHealth = c.Health
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

        public void LoadMinion(int id)
        {
            using (var context = new CardDataContext())
            {
                var x = (from m in context.Cards
                    join n in context.CardNames on m.Id equals n.CardId
                    join tr in context.CardTriggers on m.Id equals tr.CardId
                    select new
                {
                    Id = m.Id,
                    Name = n.Name,
                    Trigger = (TriggerType)tr.TriggerType,
                    Method = tr.Method.Name
                });

                foreach (var y in x)
                {
                    Console.WriteLine($"{y.Id}, {y.Name}, {y.Trigger}, {y.Method}");
                }
            }
        }
    }
}
