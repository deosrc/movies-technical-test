using Deosrc.MoviesTechnicalTest.Api.DataAccess;
using Deosrc.MoviesTechnicalTest.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deosrc.MoviesTechnicalTest.Api.Services.Search
{
    public class MovieSearchService(IMovieReadOnlyRepository movieRepository, ILogger<MovieSearchService> logger) : IMovieSearchService
    {
        private readonly IMovieReadOnlyRepository _movieRepository = movieRepository;
        private readonly ILogger<MovieSearchService> _logger = logger;

        public async Task<PagedResult<Movie>> SearchAsync(string title, PagingOptions pagingOptions)
        {
            _logger.LogInformation("Searching for movie with title '{title}'...", title);

            var results = await _movieRepository.Movies
                .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                .OrderByDescending(x => x.Popularity)
                .Skip((pagingOptions.Page - 1) * pagingOptions.ItemsPerPage)
                .Take(pagingOptions.ItemsPerPage + 1) // Retrieve one more than request to see if there are more results
                .ToListAsync();

            // Check if there are more results. If there are, reduce the list to the requested amount.
            var hasMorePages = results.Count > pagingOptions.ItemsPerPage;
            if (hasMorePages)
            {
                results = results.Take(pagingOptions.ItemsPerPage).ToList();
            }

            _logger.LogInformation("Returning {count} movies for title search '{title}'.", results.Count, title);

            return new()
            {
                Results = results,
                Page = new(pagingOptions, hasMorePages)
            };
        }
    }
}
