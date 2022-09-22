


using Newtonsoft.Json;

namespace RealitSystem_CLI
{
    [JsonObject(MemberSerialization.OptIn)]
    public struct Vector3
    {
        [JsonProperty]
        public float x {get; set;}
        [JsonProperty]
        public float y {get; set;}
        [JsonProperty]
        public float z {get; set;}

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}