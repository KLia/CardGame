using System;
using System.Collections.Generic;
using System.IO;
using CardGame.Model.Cards.Interfaces;

namespace CardScriptLoader
{
    public class CardScriptLoader
    {
        //todo - move script code into separate .csx file
        public static IList<ICard> GetCards(string[] scripts)
        {
            var context = new CardContext();
            var globs = new Dictionary<string, object> {{"CardContext", context}};
            var sb = new ScriptBridge(globs);
            var code = "#r \"C:\\Users\\keith\\Documents\\GitHub\\CardGame\\CardGame\\bin\\Debug\\CardGame.dll\"; \r\n";
            code += "using CardGame.Model.Cards; \r\n";

            code += File.ReadAllText(scripts[0]);
            code += $@"  var minion = new {Path.GetFileNameWithoutExtension(scripts[0])}();
                             CardContext.AddCard(minion);
                             ""0""";
            var rv = sb.Execute(code);
            Console.WriteLine("---- OutThing ----");
            Console.WriteLine(context.Cards);
            Console.WriteLine($"Number of Cards loaded: {context.Cards.Count}");
            Console.WriteLine("---- Return Value ----");
            Console.WriteLine(rv);

            return context.Cards;
        }
    }
}
