using System;
using System.Collections.Generic;
using System.CommandLine;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace RealitSystem_CLI.Commands
{
    [Verb("build", HelpText = "Generate a .rsz file")]
    internal class BuildCommand
    {
        [Option('p', "path", Required = true)]
        public string? BuildPath { get; set; }


        public RealitReturnCode Build()
        {
            var missingSettings = RealitBuilder.Current.GetMissingSettings();
            if(missingSettings.Count > 0)
            {
                string missings = string.Join("\n", missingSettings);

                return new RealitReturnCode(ReturnStatus.Failure, $"Cannot build the scene because this settings are missing : \n {missings}");
            }

            return new RealitReturnCode(ReturnStatus.Success);
        }
    }
}
