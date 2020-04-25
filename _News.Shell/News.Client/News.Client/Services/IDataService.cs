using News.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Client.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Source>> GetSourcesAsync();
        Task<IEnumerable<TopHeadlines>> GetTopHeadlinesAsync(string SourceID, int pageSize);
        Task<IEnumerable<Everything>> GetEverythingsAsync(string SourceID, int pageSize);
        Task<IEnumerable<Country>> GetCountriesAsync();



    }
}
