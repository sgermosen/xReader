using Newtonsoft.Json;
using System.Collections.Generic;

namespace News.Client.Models
{
    public partial class ResponseEverything
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("articles")]
        public List<Everything> Everything { get; set; }
    }
}
