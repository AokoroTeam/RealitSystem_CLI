using System.CommandLine;
using System.Text.Json;
using CommandLine;
using RealitSystem_CLI.Commands;

namespace RealitSystem_CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RealitReturnCode returnCode = CommandLine.Parser.Default.ParseArguments<
                BuildCommand, 
                SceneCommands, 
                RealitReturnCode>
            (args).MapResult(
                (BuildCommand buildCommand) => buildCommand.Build(),
                (SceneCommands sceneCommands) => sceneCommands.Apply(),
                errs => new RealitReturnCode(ReturnStatus.Failure));


            if(returnCode.returnStatus == ReturnStatus.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(returnCode.message);
                Console.ForegroundColor = ConsoleColor.White;

                await RealitBuilder.Current.Apply();
                //Apply
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(returnCode.message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}