using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace YtVideosFromFb.Services
{
    public class FacebookApiClient : IFacebookApiClient
    {
        private readonly ILogger<FacebookApiClient> _logger;
        private readonly HttpClient _httpClient;
 
        public FacebookApiClient(ILogger<FacebookApiClient> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
 
        public async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"GET {endpoint} returned {response.ReasonPhrase}");
                return default(T);
            }
        
            var result = await response.Content.ReadAsStringAsync();
 
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}