using Deosrc.MoviesTechnicalTest.Api.Entities;
using Deosrc.MoviesTechnicalTest.Api.Models.Requests;
using Deosrc.MoviesTechnicalTest.Api.Models.Responses;
using Deosrc.MoviesTechnicalTest.Api.Services.Search;
using Microsoft.AspNetCore.Mvc;

namespace Deosrc.MoviesTechnicalTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController(IMovieSearchService searchService, ILogger<MovieController> logger) : ControllerBase
    {
        private readonly IMovieSearchService _searchService = searchService;
        private readonly ILogger<MovieController> _logger = logger;

        [HttpPost(Name = "Search")]
        public async Task<IEnumerable<MovieResponse>> Search(MovieSearchRequest searchRequest)
        {
            var results = await _searchService.SearchAsync(searchRequest.Title, searchRequest.Paging);

            return results
                .Select(ConvertEntityToResponse)
                .ToList();
        }

        private static MovieResponse ConvertEntityToResponse(Movie entity)
        {
            return new MovieResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Overview = entity.Overview,
                ReleaseDate = entity.ReleaseDate.ToString("yyyy-MM-dd"),
                Popularity = entity.Popularity,
                VoteCount = entity.VoteCount,
                VoteAverage = entity.VoteAverage,
                OriginalLanguage = entity.OriginalLanguage,
                Genres = entity.Genres,
                PosterUrl = entity.PosterUrl
            };
        }
    }
}
