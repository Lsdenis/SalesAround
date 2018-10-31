using Newtonsoft.Json;

namespace SalesAround.Core.DTO
{
    public class SlivkiSalesDTO
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public Feature[] Features { get; set; }
    }

    public class Feature
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }

    public class Properties
    {
        [JsonProperty("iconClass")]
        public string IconClass { get; set; }

        [JsonProperty("iconContent")]
        public string IconContent { get; set; }

        [JsonProperty("locationID")]
        public long LocationId { get; set; }

        [JsonProperty("offerID")]
        public long OfferId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("hintContent")]
        public string HintContent { get; set; }
    }
}