


using Newtonsoft.Json;

namespace RealitSystem_CLI
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Vector3
    {
        [JsonProperty]
        public float x {get; set;}
        [JsonProperty]
        public float y {get; set;}
        [JsonProperty]
        public float z {get; set;}

        [JsonConstructor]
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static bool TrySerialize(string serializedValue, out Vector3 vector3)
        {
            string[] values = serializedValue.Split('/');
            if(values.Length == 3)
            {
                float x, y, z;
                if(float.TryParse(values[0], out x) && float.TryParse(values[1], out y) && float.TryParse(values[2], out z))
                {
                    vector3 = new Vector3(x, y, z);
                    return true;
                }
            }

            vector3 = default;
            return false;
        }
    }
}