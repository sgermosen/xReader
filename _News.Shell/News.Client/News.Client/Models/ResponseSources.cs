using Newtonsoft.Json;
using System.Collections.Generic;

namespace News.Client.Models
{
    public partial class ResponseSources
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("sources")]
        public List<Source> Source { get; set; }


    }
}
