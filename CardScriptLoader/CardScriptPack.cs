using ScriptCs.Contracts;
using CardGame;

namespace CardScriptLoader
{
    public class CardScriptPack : IScriptPack
    {
        private readonly CardScriptPackContext _ctx;

        public CardScriptPack()
        {
            _ctx = new CardScriptPackContext();
        }

        public void Initialize(IScriptPackSession session)
        {
            session.AddReference("C:\\Users\\keith\\Documents\\GitHub\\CardGame\\CardGame\\bin\\Debug\\CardGame.dll");
            session.ImportNamespace("CardGame.Model.Cards");
        }

        public IScriptPackContext GetContext()
        {
            return _ctx;
        }

        public void Terminate()
        {
        }
    }
}
