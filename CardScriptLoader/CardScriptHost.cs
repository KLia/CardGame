using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;

namespace CardScriptLoader
{
    public class CardScriptHost : ScriptHost
    {
        public CardScriptHost(IScriptPackManager scriptPackManager, ScriptEnvironment environment)
            : base(scriptPackManager, environment)
        {
        }

        public dynamic InputAmbient { get; set; }
        public dynamic OutputAmbient { get; set; }
    }

    class CardScriptHostFactory : IScriptHostFactory
    {
        private readonly Dictionary<string, object> _globs;
        public CardScriptHost Host;
        public CardScriptHostFactory(Dictionary<string, object> globs)
        {
            this._globs = globs;
        }
        public IScriptHost CreateScriptHost(IScriptPackManager scriptPackManager, string[] scriptArgs)
        {
            Host = new CardScriptHost(scriptPackManager, new ScriptEnvironment(scriptArgs))
            {
                InputAmbient = _globs["InputAmbient"],
                OutputAmbient = _globs["OutputAmbient"]
            };
            return Host;
        }
    }
    public class ScriptBridge
    {
        private readonly Dictionary<string, object> _globals;
        public ScriptBridge(Dictionary<string, object> globals)
        {
            _globals = globals;
        }

        public dynamic ReturnValue { get; private set; }
        public dynamic Execute(string code)
        {
            var console = (IConsole)new ScriptConsole();
            var logProvider = new ColoredConsoleLogProvider(LogLevel.Info, console);

            var builder = new ScriptServicesBuilder(console, logProvider);
            builder.LogLevel(LogLevel.Info).Cache(false).Repl(false).ScriptEngine<CSharpScriptEngine>();

            var shf = new CardScriptHostFactory(_globals);
            builder.SetOverride<IScriptHostFactory, CardScriptHostFactory>(shf);
            var services = builder.Build();

            var executor = (ScriptExecutor)services.Executor;
            executor.Initialize(Enumerable.Empty<string>(), new[] {new CardScriptPack()});
            
            var result = executor.ExecuteScript(code);
            ReturnValue = result.ReturnValue;

            if (result.CompileExceptionInfo != null)
                throw result.CompileExceptionInfo.SourceException;
            if (result.ExecuteExceptionInfo != null)
                throw result.ExecuteExceptionInfo.SourceException;

            return result.ReturnValue;
        }
    }
}
