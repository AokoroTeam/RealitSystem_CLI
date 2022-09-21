using System.CommandLine;
using System.Text.Json;
using CommandLine;
using RealitSystem_CLI.Sources.Commands;

namespace RealitSystem_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = CommandLine.Parser.Default.ParseArguments<BuildCommand, bool>(args)
            .MapResult(
                (BuildCommand buildCommand) => Build(buildCommand),
                errs => 1);

        }

        private static int Build(BuildCommand build)
        {
            build.Build();
            return 1;
        }
    }
}