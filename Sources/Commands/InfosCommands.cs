using System;
using System.Collections.Generic;
using System.CommandLine;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace RealitSystem_CLI.Commands
{
    [Verb("info", HelpText = "Get currentInfos")]
    internal class InfosCommand
    {
        public RealitReturnCode GetInfos()
        {
            string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine("Aller aller");
            return new RealitReturnCode(ReturnStatus.Success);
        }
    }
}