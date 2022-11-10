
using System.CommandLine;
using System.Reflection;
using System.Text.Json;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealitSystem_CLI.Commands;

namespace RealitSystem_CLI
{
    public class RealitBuilder
    {
        public static RealitBuilder Instance;
        public RealitBuilderData Data;
        public readonly string rltbPath;
        public readonly string enginePath;

        public RealitBuilder(Settings settings)
        {
            rltbPath = RealitBuilderData.GetTargetedPath();
            Data = RealitBuilderData.GetRltbAtPath(rltbPath);

            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            enginePath = Path.Combine(assemblyPath, "Engine/MiaoXRealit.exe");

            Instance = this;
        }

        internal List<string> GetMissingData()
        {
            List<string> missing = new List<string>();

            if (Data.PlayerPosition == null)
                missing.Add("player-pos");

            if (Data.PlayerRotation == null)
                missing.Add("player-rot");

            if (Data.ModelPath == null)
                missing.Add("model-path");
                
            return missing;
        }
    }
}