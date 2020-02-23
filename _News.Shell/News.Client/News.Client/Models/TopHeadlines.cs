using Newtonsoft.Json;
using System;

namespace News.Client.Models
{
    public partial class TopHeadlines
    {
        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("urlToImage")]
        public string UrlToImage { get; set; }

        [JsonProperty("publishedAt")]
        public DateTimeOffset PublishedAt { get; set; }


        //[JsonProperty("source")]
        //public Source Source { get; set; }

        //[JsonProperty("author")]
        //public string Author { get; set; }

        //[JsonProperty("title")]
        //public string Title { get; set; }

        //[JsonProperty("description")]
        //public string Description { get; set; }

        //[JsonProperty("url")]
        //public string Url { get; set; }

        //[JsonProperty("urlToImage")]
        //public string UrlToImage { get; set; }

        //[JsonProperty("publishedAt")]
        //public DateTimeOffset PublishedAt { get; set; }



    }
}
