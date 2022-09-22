
using System.CommandLine;
using System.Text.Json;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealitSystem_CLI.Sources.Commands;

namespace RealitSystem_CLI
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RealitBuilder
    {
        public readonly string _Path;

        [JsonProperty]
        public string? ModelPath { get; set; }
        
        [JsonProperty]
        public Vector3 PlayerPosition { get; set; }
        [JsonProperty]
        public Vector3 PlayerRotation { get; set; }

        private static RealitBuilder? current;

        public RealitBuilder(string path)
        {
            this._Path = path;
        }

        public static async Task<RealitBuilder> Current()
        {
            if(current == null)
            {
                string currentPath = Directory.GetCurrentDirectory();
                string realitBuilderFile = Path.Combine(currentPath, ".rltb");
                current = new RealitBuilder(realitBuilderFile);
                
                if(File.Exists(realitBuilderFile))
                {
                    using(FileStream fileStream = new FileStream(realitBuilderFile, FileMode.Open))
                    using(StreamReader streamReader = new StreamReader(fileStream))
                    using(JsonReader reader = new JsonTextReader(streamReader))
                    {
                        string? value = await reader.ReadAsStringAsync();
                        
                        if(value != null)
                            JsonConvert.PopulateObject(value, current);
                    }
                }
                else
                {
                    try
                    {
                        using (new FileStream(realitBuilderFile, FileMode.CreateNew)) {}
                    }
                    catch
                    {
                        //TODO
                        throw;
                    }
                }
            }

            return current;
        }
    }
}