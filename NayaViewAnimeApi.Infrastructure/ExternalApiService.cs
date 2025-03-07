using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using NayaViewAnimeApi.Application;

namespace NayaViewAnimeApi.Infrastructure
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDataFromExternalApiAsync(string enpoint)
        {
            var response = await _httpClient.GetAsync(enpoint);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new HttpRequestException($"Error al acceder a la API externa. Código de estado: {response.StatusCode}");
        }
    }
}
