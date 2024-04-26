using Deosrc.MoviesTechnicalTest.Api.DataAccess;
using Deosrc.MoviesTechnicalTest.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deosrc.MoviesTechnicalTest.Api.Services.Search
{
    public class EFMovieSearchService(MovieDatabaseContext dbContext, ILogger<EFMovieSearchService> logger) : IMovieSearchService
    {
        private readonly MovieDatabaseContext _dbContext = dbContext;
        private readonly ILogger<EFMovieSearchService> _logger = logger;

        public async Task<IEnumerable<Movie>> SearchAsync(string title)
        {
            _logger.LogInformation("Searching for movie with title '{title}'...", title);

            var results = await _dbContext.Movies
                .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();

            _logger.LogInformation("Found {count} movies for title search '{title}'.", results.Count, title);

            return results;
        }
    }
}
