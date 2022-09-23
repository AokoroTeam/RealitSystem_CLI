
using System.CommandLine;
using System.Text.Json;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealitSystem_CLI.Commands;

namespace RealitSystem_CLI
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RealitBuilder
    {
        public readonly string _Path;

        [JsonProperty]
        public string? ModelPath { get; set; }

        [JsonProperty]
        public string[] Appertures { get; set; }


        [JsonProperty]
        public Vector3 PlayerPosition { get; set; }
        [JsonProperty]
        public Vector3 PlayerRotation { get; set; }

        public bool Dirty { get; internal set; }

        private static RealitBuilder? current;

        public RealitBuilder(string path)
        {
            this._Path = path;
        }

        public static RealitBuilder Current => GetCurrent();


        private static RealitBuilder GetCurrent()
        {
            if (current == null)
            {
                string filePath = GetFilePath();
                current = new RealitBuilder(filePath);

                if (File.Exists(filePath))
                {
                    string? value = File.ReadAllText(filePath);
                    if (value != null)
                        current = JsonConvert.DeserializeObject<RealitBuilder>(value);

                }
                else
                {
                    try
                    {
                        using (new FileStream(filePath, FileMode.CreateNew)){}
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

        private static string GetFilePath()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string realitBuilderFile = Path.Combine(currentPath, ".rltb");
            return realitBuilderFile;
        }

        public async Task<RealitReturnCode> Apply()
        {
            try
            {
                if (Dirty)
                {
                    string path = GetFilePath();
                    string content = JsonConvert.SerializeObject(this);
                    await File.WriteAllTextAsync(path, content);
                }

                return new RealitReturnCode(ReturnStatus.Success);
            }
            catch(Exception e)
            {
                return new RealitReturnCode(ReturnStatus.Failure, e.Message);
            }
        }


        internal List<string> GetMissingSettings()
        {
            List<string> settings = new List<string>();
            
            if(PlayerPosition == null)
                settings.Add("player-pos");
            
            if (PlayerRotation == null)
                settings.Add("player-rot");

            if (ModelPath == null)
                settings.Add("model-path");

            return settings;

        }

    }
}