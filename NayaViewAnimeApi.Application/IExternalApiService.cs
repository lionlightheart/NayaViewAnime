namespace NayaViewAnimeApi.Application
{
    public interface IExternalApiService
    {
        Task<string> GetDataFromExternalApiAsync(string endpoint);
        Task<string> GetAnimeDataAsync(string query, string token);
    }
}
