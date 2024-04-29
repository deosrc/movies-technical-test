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
        [Produces<PagedResult<MovieResponse>>]
        public async Task<IActionResult> SearchAsync(MovieSearchRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Search request was invalid.");
                return BadRequest(ModelState);
            }

            var results = await _searchService.SearchAsync(searchRequest.Title, searchRequest.Genre, searchRequest.Paging);

            return Ok(new PagedResult<MovieResponse>()
                {
                    Results = results.Results.Select(ConvertEntityToResponse).ToList(),
                    Page = results.Page
                });
        }

        private static MovieResponse ConvertEntityToResponse(Movie entity)
        {
            return new MovieResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Overview = entity.Overview,
                ReleaseDate = entity.ReleaseDate.ToString("yyyy-MM-dd"),
                Popularity = (double)entity.Popularity,
                VoteCount = entity.VoteCount,
                VoteAverage = (double)entity.VoteAverage,
                OriginalLanguage = entity.OriginalLanguage,
                Genres = entity.Genres.Select(x => x.Name).ToArray(),
                PosterUrl = entity.PosterUrl
            };
        }
    }
}
