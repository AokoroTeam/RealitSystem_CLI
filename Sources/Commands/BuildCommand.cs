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
            RealitBuilder? current = RealitBuilder.Current;
            var missingSettings = current.GetMissingData();
            if (missingSettings.Count > 0)
            {
                string missings = string.Join("\n", missingSettings);

                return new RealitReturnCode(ReturnStatus.Failure, $"Cannot build the scene because this settings are missing : \n {missings}");
            }

            string enginePath = current.settings.EnginePath;
            using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
            {
                pProcess.StartInfo.FileName = enginePath;
                pProcess.StartInfo.Arguments = current._Path; //argument
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                pProcess.StartInfo.CreateNoWindow = true;
                pProcess.Start();
                string output = pProcess.StandardOutput.ReadToEnd(); //The output result
                pProcess.WaitForExit();
                int code = pProcess.ExitCode;
            }
            return new RealitReturnCode(ReturnStatus.Success);
        }
    }
}
