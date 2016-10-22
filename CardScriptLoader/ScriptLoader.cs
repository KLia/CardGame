using System;
using System.Collections.Generic;
using System.Linq;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Mono;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;

namespace CardScriptLoader
{
    public class ScriptLoader
    {
        private static ScriptExecutor _executor;

        public static void LoadScript()
        {
            var console = (IConsole)new ScriptConsole();
            var logProvider = new ColoredConsoleLogProvider(LogLevel.Info, console);

            var builder = new ScriptServicesBuilder(console, logProvider);

            SetEngine(builder);
            var services = builder.Build();

            _executor = (ScriptExecutor)services.Executor;
            _executor.Initialize(Enumerable.Empty<string>(), new List<IScriptPack> { new CardScriptPack() });
        }

        private static void SetEngine(ScriptServicesBuilder builder)
        {
            var useMono = Type.GetType("Mono.Runtime") != null;
            if (useMono)
            {
                builder.ScriptEngine<MonoScriptEngine>();
            }
            else
            {
                builder.ScriptEngine<CSharpScriptEngine>();
            }
        }

        public static void ExecuteFile(string script)
        {
            if (_executor == null)
            {
                LoadScript();
            }

           var result = _executor?.Execute(script);

            if (result?.CompileExceptionInfo != null) throw new Exception("Compilation fail");
            if (result?.ExecuteExceptionInfo != null) throw new Exception("Execution fail");
        }
    }
}
