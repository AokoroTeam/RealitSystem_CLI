using System;
using System.Collections.Generic;
using System.CommandLine;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace RealitSystem_CLI.Sources.Commands
{
    [Verb("model", HelpText = "refer the model that will be used")]
    internal class ModelCommands
    {
        [Option('p', "path", Required = true)]
        public string? path { get; set; }

        public void Build()
        {
        }
    }
}
