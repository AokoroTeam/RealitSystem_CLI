using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RealitSystem_CLI.Communication;
using CommandLine;

namespace RealitSystem_CLI.Commands
{
    [Verb("build", HelpText = "Generate a .rsz file")]
    internal class BuildCommand
    {
        [Option('p', "path", Required = true)]
        public string? output { get; set; }

        [Option("miao")]
        public string miao { get; set; }
        
        [Option("embed")]
        public bool embed { get; set; }

        public RealitReturnCode Build()
        {
            RealitBuilder realitBuilder = RealitBuilder.Instance;
            RealitBuilderData? data = realitBuilder.Data;
            var missingSettings = data.GetMissingData();
            System.Diagnostics.Stopwatch watch = new();

            if (missingSettings.Count > 0)
            {
                string missings = string.Join("\n", missingSettings);

                return new RealitReturnCode(ReturnStatus.Failure, $"Cannot build the scene because this settings are missing : \n {missings}");
            }
            List<string> arguments = new List<string>()
            {
                $"-rltb {realitBuilder.rltbPath}",
                $"-output {output}",
                $"-batchmode"
            };

            if (!string.IsNullOrEmpty(miao))
                arguments.Add($"-miao {miao}");

            if(embed)
                    arguments.Add("-embed");

            string enginePath = realitBuilder.enginePath;
            string joinedArgs = string.Join(' ', arguments);

            using (Process pProcess = new Process())
            {
                RealitPipeServer realitPipe = new RealitPipeServer();
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    FileName = enginePath,
                    Arguments = joinedArgs,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                };
                pProcess.StartInfo = startInfo;
                Program.OnCancel += pProcess.Kill;
                Program.OnCancel += realitPipe.Close;

                pProcess.Start();
                realitPipe.Start();

                pProcess.WaitForExit();

                Program.OnCancel -= realitPipe.Close;
                Program.OnCancel -= pProcess.Kill;

                realitPipe.Close();

                int code = pProcess.ExitCode;

                if (code == 1)
                    return new RealitReturnCode(ReturnStatus.Success, "Successfuly generated");
            }

            return new RealitReturnCode(ReturnStatus.Failure, "Generation failed");
        }
    }
}
