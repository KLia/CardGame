//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ScriptCs.Contracts;

//namespace CardGameConsoleApp.ScriptLoader
//{
//    public class CardScriptPack : IScriptPack
//    {
//        private readonly CardScriptPackContext _ctx;

//        public CardScriptPack( )
//        {
//            _ctx = new CardScriptPackContext();
//        }

//        public void Initialize(IScriptPackSession session)
//        {
//            session.AddReference("C:\\Users\\keith\\Documents\\GitHub\\CardGame\\CardGame\\bin\\Debug\\CardGame.dll");
//            session.ImportNamespace("CardGame.Model.Cards");
//            session.ImportNamespace("CardGameConsoleApp");
//        }

//        public IScriptPackContext GetContext()
//        {
//            return _ctx;
//        }

//        public void Terminate()
//        {
//        }
//    }
//}
