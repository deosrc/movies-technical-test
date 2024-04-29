using Deosrc.MoviesTechnicalTest.Api.Entities;
using Deosrc.MoviesTechnicalTest.Api.Services.Lookups;
using Microsoft.AspNetCore.Mvc;

namespace Deosrc.MoviesTechnicalTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController(ILookupService lookupService, ILogger<GenreController> logger) : ControllerBase
    {
        private readonly ILookupService _lookupService = lookupService;
        private readonly ILogger<GenreController> _logger = logger;

        [HttpGet(Name = "Get")]
        [Produces<IReadOnlyCollection<string>>]
        public async Task<IActionResult> GetAsync()
        {
            var results = (await _lookupService.GetGenresAsync())
                .Select(ConvertEntityToResponse)
                .ToList();
            return Ok(results);
        }

        private static string ConvertEntityToResponse(Genre entity) => entity.Name;
    }
}
