using Newtonsoft.Json;
using NayaViewAnimeApi.Application;
using NayaViewAnimeApi.Domain;

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

        public async Task<MyAnimeListResponseDto> GetAnimeDataAsync(string query, string token)
        {

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, query);
                request.Headers.Add("X-MAL-CLIENT-ID", token);
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonC
                }
                throw new HttpRequestException($"Error al acceder a la API externa. Código de estado: {response.StatusCode}");
            } catch (Exception ex){
                throw new HttpRequestException($"Error {ex}");
            }
        }

       
    }
}

