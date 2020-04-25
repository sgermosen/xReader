using News.Client.ViewModels;
using Newtonsoft.Json;

namespace News.Client.Models
{
    public partial class Source : Prism.Mvvm.BindableBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set { SetProperty(ref _isFavorite, value); }
        }

        string _imagenFavorite;
        public string ImagenFavorite
        {
            get {
                return _imagenFavorite;
            }
            set { SetProperty(ref _imagenFavorite, value); }
        }
    }
}
