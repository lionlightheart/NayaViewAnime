using NayaViewAnimeApi.Domain;

namespace NayaViewAnimeApi.Application
{
    public interface IExternalApiService
    {
        Task<string> GetDataFromExternalApiAsync(string endpoint);
        Task<MyAnimeListResponseDto?> GetAnimeDataAsync(string query, string token);
    }
}
