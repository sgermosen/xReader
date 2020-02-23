using Newtonsoft.Json;
using System.Collections.Generic;

namespace News.Client.Models
{
    public partial class RegionalBloc
    {
        [JsonProperty("acronym")]
        public string Acronym { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("otherAcronyms")]
        public List<string> OtherAcronyms { get; set; }

        [JsonProperty("otherNames")]
        public List<string> OtherNames { get; set; }
    }
}
