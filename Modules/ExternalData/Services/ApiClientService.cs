using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NayaViewAnime.Modules.ExternalData.Services
{
    public class ApiClientService {
        private readonly HttpClient _httpClient;

        public ApiClientService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<T?> FetchDataAsync<T>(string url) {
            var response = await _httpClient.GetStringAsync(url);

            if (string.IsNullOrWhiteSpace(response)){
                return default;
            }

            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}