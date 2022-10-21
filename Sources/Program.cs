using System.CommandLine;
using System.Text.Json;
using CommandLine;
using Microsoft.Extensions.Configuration;
using RealitSystem_CLI.Commands;
namespace RealitSystem_CLI
{
    class Program
    {
        public static event Action OnCancel;
        static async Task Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Console.WriteLine("Cancel event triggered");
                eventArgs.Cancel = true;
                OnCancel?.Invoke();
            };   
            
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            // Get values from the config given their key and their target type.
            Settings settings = config.GetRequiredSection("Settings").Get<Settings>();
            RealitBuilder realitBuilder = new RealitBuilder(settings);

            RealitReturnCode returnCode = CommandLine.Parser.Default.ParseArguments<
                BuildCommand,
                SceneCommands,
                InfosCommand,
                RealitReturnCode>


            (args).MapResult(
                (BuildCommand buildCommand) => buildCommand.Build(),
                (SceneCommands sceneCommands) => sceneCommands.Apply(),
                (InfosCommand infosCommands) => infosCommands.GetInfos(),
                errs => new RealitReturnCode(ReturnStatus.Failure));


            if (returnCode.returnStatus == ReturnStatus.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(returnCode.message);
                Console.ForegroundColor = ConsoleColor.White;

                await realitBuilder.Data.Apply();
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