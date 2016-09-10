using System;
using System.Collections.Generic;
using System.Linq;
using CardGameConsoleTestApp.DBML;
using CardGameConsoleTestApp.Model.Cards;
using CardGameConsoleTestApp.Model.Cards.Interfaces;

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

        public IList<Minion> GetAllMinionsList(int languageId)
        {
            using (var context = new CardDataContext())
            {
                var minions = (from c in context.Cards
                    join n in context.CardNames on c.Id equals n.CardId
                    where c.Type == (int) CardType.Minion
                    && n.LanguageId == languageId
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

        public IList<Spell> GetAllSpellsList(int languageId)
        {
            using (var context = new CardDataContext())
            {
                var spells = (from c in context.Cards
                    join n in context.CardNames on c.Id equals n.CardId
                    where c.Type == (int) CardType.Spell
                    && n.LanguageId == languageId
                    select new Spell()
                    {
                        Name = n.Name,
                        Cost = c.Cost
                    });

                return spells.ToList();
            }
        }

        public void LoadTriggerItem()
        {
            //using (var context = new CardDataContext())
            //{
            //    var x = (from c in context.Cards
            //        join n in context.CardNames on c.Id equals n.CardId
            //        join tr in context.CardTriggers on c.Id equals tr.CardId
            //        where c.Id == triggerable.Id
            //        select new
            //    {
            //        Id = c.Id,
            //        Name = n.Name,
            //        Trigger = (TriggerType)tr.TriggerType,
            //        Method = tr.Method.Name,
            //        MethodParams = (from tp in context.CardTriggerParams
            //                        where tp.CardTriggerId == tr.Id
            //                        select new
            //                        {
            //                            tp.ParamName,
            //                            tp.ParamValue
            //                        }).AsEnumerable()
            //    });

            //    foreach (var y in x)
            //    {
            //        var paramsera = y.MethodParams.ToDictionary(t => t.ParamName, t => t.ParamValue);
            //        Console.WriteLine($"{y.Id}, {y.Name}, {y.Trigger}, {y.Method}");
            //    }
            //}
        }
    }
}
