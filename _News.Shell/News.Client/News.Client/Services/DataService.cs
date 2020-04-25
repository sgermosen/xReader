using News.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace News.Client.Services
{
    public class DataService : IDataService
    {

        //private ObservableCollection<TopHeadlines> OriginalGlobalTopHeadLines = new ObservableCollection<TopHeadlines>();
        private ObservableCollection<Source> OriginalGlobalSources = new ObservableCollection<Source>();
        //private ObservableCollection<Everything> OriginalGlobalEverythings = new ObservableCollection<Everything>();
        private ObservableCollection<Country> OriginalGlobalCountry = new ObservableCollection<Country>();

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            if (OriginalGlobalCountry != null && OriginalGlobalCountry.Count > 0)
                return OriginalGlobalCountry;

            List<Country> result;
            var resultLanguage = new List<Language>();
            try
            {
                using (var client = new HttpClient())
                {
                    var stringResponse = await client.GetStringAsync("https://restcountries.eu/rest/v2/all");
                    result = JsonConvert.DeserializeObject<List<Country>>(stringResponse);
                    OriginalGlobalCountry = new ObservableCollection<Country>(result);



                }
            }
            catch (Exception ex)
            {


                //DisplayAlert("Alert", "You have been alerted", "OK");
            }

            return OriginalGlobalCountry;
        }


        public async Task<IEnumerable<Everything>> GetEverythingsAsync(String SourceID, Int32 pageSize)
        {

            //if (OriginalGlobalEverythings != null && OriginalGlobalEverythings.Count > 0)
            //    return OriginalGlobalEverythings;

            if (SourceID != null && SourceID != null)
            {
                var result = new List<Everything>();
                try
                {
                    using (var client = new HttpClient())
                    {
                        var stringResponse = await client.GetStringAsync($"https://newsapi.org/v2/everything?sources={SourceID}&pageSize{pageSize}&apiKey=037ec3876d274e97ba55ecb4bbd830b9");
                        result = JsonConvert.DeserializeObject<ResponseEverything>(stringResponse).Everything;
                        //OriginalGlobalEverythings = new ObservableCollection<Everything>(result);
                    }
                }
                catch (Exception ex)
                {
                    //DisplayAlert("Alert", "You have been alerted", "OK");
                }
                return result;
            }
            else
            {
                return null;
            }



        }

        public async Task<IEnumerable<Source>> GetSourcesAsync()
        {

            if (OriginalGlobalSources != null && OriginalGlobalSources.Count > 0)
                return OriginalGlobalSources;

            List<Source> result;
            try
            {
                using (var client = new HttpClient())
                {
                    var stringResponse = await client.GetStringAsync("https://newsapi.org/v2/sources?apiKey=037ec3876d274e97ba55ecb4bbd830b9");
                    //result = JsonConvert.DeserializeObject<>(stringResponse);
                    result = JsonConvert.DeserializeObject<ResponseSources>(stringResponse).Source;

                    OriginalGlobalSources = new ObservableCollection<Source>(result);
                }
            }
            catch (Exception ex)
            {


                //DisplayAlert("Alert", "You have been alerted", "OK");
            }

            return OriginalGlobalSources;
        }

        public async Task<IEnumerable<TopHeadlines>> GetTopHeadlinesAsync(String SourceID, Int32 pageSize)
        {
            if (SourceID != null && SourceID != null)
            {
                var result = new List<TopHeadlines>();
                try
                {
                    using (var client = new HttpClient())
                    {
                        var stringResponse = await client.GetStringAsync($"https://newsapi.org/v2/top-headlines?sources={SourceID}&pageSize={pageSize}&apiKey=037ec3876d274e97ba55ecb4bbd830b9");
                        result = JsonConvert.DeserializeObject<ResponseTopHeadLines>(stringResponse).TopHeadlines;
                        //OriginalGlobalTopHeadLines = new ObservableCollection<TopHeadlines>(result);
                    }
                }
                catch (Exception ex)
                {
                    //DisplayAlert("Alert", "You have been alerted", "OK");
                }
                return result;
            }
            else
            {
                return null;
            }

        }


    }
}
