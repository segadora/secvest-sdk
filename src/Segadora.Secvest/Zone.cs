using System.Text.Json.Serialization;

namespace Segadora.Secvest
{
    public class Zone
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }
    }
}