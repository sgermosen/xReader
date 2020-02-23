using Newtonsoft.Json;

namespace News.Client.Models
{
    public partial class Translations
    {
        [JsonProperty("de", Required = Required.AllowNull)]
        public string De { get; set; }

        [JsonProperty("es", Required = Required.AllowNull)]
        public string Es { get; set; }

        [JsonProperty("fr", Required = Required.AllowNull)]
        public string Fr { get; set; }

        [JsonProperty("ja", Required = Required.AllowNull)]
        public string Ja { get; set; }

        [JsonProperty("it", Required = Required.AllowNull)]
        public string It { get; set; }

        [JsonProperty("br", Required = Required.Always)]
        public string Br { get; set; }

        [JsonProperty("pt", Required = Required.Always)]
        public string Pt { get; set; }

        [JsonProperty("nl", Required = Required.AllowNull)]
        public string Nl { get; set; }

        [JsonProperty("hr", Required = Required.AllowNull)]
        public string Hr { get; set; }

        [JsonProperty("fa", Required = Required.Always)]
        public string Fa { get; set; }
    }

}
