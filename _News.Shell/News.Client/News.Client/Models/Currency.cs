using Newtonsoft.Json;

namespace News.Client.Models
{
    public partial class Currency
    {
        [JsonProperty("code", Required = Required.AllowNull)]
        public string Code { get; set; }

        [JsonProperty("name", Required = Required.AllowNull)]
        public string Name { get; set; }

        [JsonProperty("symbol", Required = Required.AllowNull)]
        public string Symbol { get; set; }
    }
}
