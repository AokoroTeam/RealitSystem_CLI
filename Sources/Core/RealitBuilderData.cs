
using System.CommandLine;
using System.Text.Json;
using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealitSystem_CLI.Commands;

namespace RealitSystem_CLI
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RealitBuilderData
    {
        [JsonProperty]
        public string? ModelPath { get; set; }
        [JsonProperty]
        public string[] Appertures { get; set; }
        [JsonProperty]
        public Vector3 PlayerPosition { get; set; }
        [JsonProperty]
        public Vector3 PlayerRotation { get; set; }
        [JsonProperty]
        public string ProjectName { get; internal set; }

        public bool Dirty { get; internal set; }
        public async Task<RealitReturnCode> Apply()
        {
            try
            {
                if (Dirty)
                {
                    string path = GetTargetedPath();
                    string content = JsonConvert.SerializeObject(this);
                    await File.WriteAllTextAsync(path, content);
                }

                return new RealitReturnCode(ReturnStatus.Success);
            }
            catch (Exception e)
            {
                return new RealitReturnCode(ReturnStatus.Failure, e.Message);
            }
        }

        internal List<string> GetMissingData()
        {
            List<string> missing = new List<string>();
            if (string.IsNullOrEmpty(ProjectName))
                missing.Add("project-name");

            if (PlayerPosition == null)
                missing.Add("player-pos");

            if (PlayerRotation == null)
                missing.Add("player-rot");

            if (ModelPath == null)
                missing.Add("model-path");

            return missing;

        }

        public static RealitBuilderData? GetRltbAtPath(string rltbPath)
        {
            if (File.Exists(rltbPath))
            {
                string? value = File.ReadAllText(rltbPath);
                if (value != null)
                {
                    try {
                        RealitBuilderData data = JsonConvert.DeserializeObject<RealitBuilderData>(value);
                        if(data == null)
                            data = new RealitBuilderData();

                        return data;
                    }
                    catch {
                        File.Delete(rltbPath);
                        throw;
                    }
                }
            }
            
            using (new FileStream(rltbPath, FileMode.CreateNew)) { }
            return new RealitBuilderData();
        }

        public static string GetTargetedPath()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string realitBuilderFile = Path.Combine(currentPath, ".rltb");
            return realitBuilderFile;
        }
    }
}