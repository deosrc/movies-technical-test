using Deosrc.MoviesTechnicalTest.Api.DataAccess;
using Deosrc.MoviesTechnicalTest.Api.Models.Requests;
using Deosrc.MoviesTechnicalTest.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Deosrc.MoviesTechnicalTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController(MovieDatabaseContext dbContext, ILogger<MovieController> logger) : ControllerBase
    {
        private readonly MovieDatabaseContext _dbContext = dbContext;
        private readonly ILogger<MovieController> _logger = logger;

        [HttpPost(Name = "Search")]
        public async Task<IEnumerable<MovieResponse>> Search(MovieSearchRequest searchRequest)
        {
            return await _dbContext.Movies
                .Take(10)
                .Select(m=> new MovieResponse
                {
                    Id = m.Id,
                    Title = "Movie",
                    Overview = "Movie",
                    ReleaseDate = "2024-04-26",
                    Popularity = 0,
                    VoteCount = 0,
                    VoteAverage = 0,
                    OriginalLanguage = "en",
                    Genres = Array.Empty<string>(),
                    PosterUrl = ""
                })
                .ToListAsync();
        }
    }
}
