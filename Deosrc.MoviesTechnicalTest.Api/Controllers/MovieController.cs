using Deosrc.MoviesTechnicalTest.Api.Models.Requests;
using Deosrc.MoviesTechnicalTest.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Deosrc.MoviesTechnicalTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController(ILogger<MovieController> logger) : ControllerBase
    {
        private readonly ILogger<MovieController> _logger = logger;

        [HttpPost(Name = "Search")]
        public IEnumerable<MovieResponse> Search(MovieSearchRequest searchRequest)
        {
            return Enumerable.Range(1, 5).Select(index => new MovieResponse
            {
                Id = Guid.NewGuid(),
                Title = $"Movie {index}",
                Overview = $"Movie {index}",
                ReleaseDate = "2024-04-26",
                Popularity = 0,
                VoteCount = 0,
                VoteAverage = 0,
                OriginalLanguage = "en",
                Genres = [],
                PosterUrl = ""
            })
            .ToArray();
        }
    }
}
