using Newtonsoft.Json;

namespace News.Client.Models
{
    public partial class Language
    {
        [JsonProperty("iso639_1")]
        public string Iso6391 { get; set; }

        [JsonProperty("iso639_2")]
        public string Iso6392 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }
    }
}
