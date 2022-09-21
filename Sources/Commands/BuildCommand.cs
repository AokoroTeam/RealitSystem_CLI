using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace RealitSystem_CLI.Sources.Commands
{
    [Verb("Build", HelpText = "Generate a .rsz file")]
    internal class BuildCommand
    {
        [Option('p', "path", Required = true)]
        public string? BuildPath { get; set; }

        public void Execute()
        {
            Console.WriteLine($"Building at {BuildPath}");
        }
    }
}
