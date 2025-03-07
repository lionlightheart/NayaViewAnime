using Microsoft.AspNetCore.Mvc;
using NayaViewAnimeApi.Application;

namespace NayaViewAnimeApi.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalApiController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IConfiguration _configuration;
 

    public ExternalApiController(IExternalApiService externalApiService, IConfiguration configuration)
        {
            _externalApiService = externalApiService;
            _configuration = configuration;

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
    }
}
