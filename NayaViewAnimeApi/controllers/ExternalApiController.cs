using Microsoft.AspNetCore.Mvc;

using NayaViewAnimeApi.Application;

namespace NayaViewAnimeApi.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalApiController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
 

    public ExternalApiController(IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        [HttpGet]
        [Route("get-data")]
        public async Task<IActionResult> GetDataFromExternalApi(string endpoint)
        {
            try
            {
                var data = await _externalApiService.GetDataFromExternalApiAsync(endpoint);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener datos de la API externa: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("AnimeList")]
        public async Task<IActionResult> GetAnimeDataAsync(string query)
        {
            var token = Environment.GetEnvironmentVariable("MAL_TOKEN");
            var mal_api_version = Environment.GetEnvironmentVariable("MAL_API_VERSION");
            var base_url = Environment.GetEnvironmentVariable("MAL_HOST");

            if (string.IsNullOrEmpty(token)){
                return Unauthorized("No have token");
            }

            var url = $"{base_url}/{mal_api_version}{query}";
            var data = await _externalApiService.GetAnimeDataAsync(url, token);

            return Ok(data);
        }
    }
}
