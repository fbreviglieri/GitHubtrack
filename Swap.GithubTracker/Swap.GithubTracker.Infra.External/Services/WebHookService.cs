using Newtonsoft.Json;
using Swap.GithubTracker.Domain.Interfaces.Services;
using Swap.GithubTracker.Domain.Model;
using Swap.GithubTracker.Infra.External.Mappers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Infra.External.Services
{
    public class WebHookService : IWebHookService
    {
        private readonly HttpClient _httpClient;        
        public WebHookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> PostGithubTrack(GithubTrack model)
        {
            var request = GithubTrackPostRequestMapper.Map(model);                     
            var requestData = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");           
            var response = await _httpClient.PostAsync("", requestData);
            var result = await response.Content.ReadAsStringAsync();
            //var catalog = JsonConvert.DeserializeObject<Catalog>(responseString);
            return false;
        }
    }
}
