using System.Collections.Generic;

namespace CardGameConsoleTestApp.Model.Cards.ValueObjects
{
    public class CardTrigger
    {
        public TriggerType Type { get; set; }
        public string MethodClass { get; set; }
        public string MethodName { get; set; }
        public List<TriggerMethodParam> MethodParams { get; set; }
    }
}
