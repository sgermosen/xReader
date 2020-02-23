using Newtonsoft.Json;
using System.Collections.Generic;

namespace News.Client.Models
{
    public partial class ResponseTopHeadLines
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("articles")]
        public List<TopHeadlines> TopHeadlines { get; set; }

    }
}
