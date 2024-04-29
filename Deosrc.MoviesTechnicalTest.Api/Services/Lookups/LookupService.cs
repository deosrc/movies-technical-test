using Deosrc.MoviesTechnicalTest.Api.DataAccess;
using Deosrc.MoviesTechnicalTest.Api.Entities;
using Deosrc.MoviesTechnicalTest.Api.Services.Search;

namespace Deosrc.MoviesTechnicalTest.Api.Services.Lookups
{
    public class LookupService(IMovieReadOnlyRepository movieRepository, ILogger<MovieSearchService> logger) : ILookupService
    {
        private readonly IMovieReadOnlyRepository _movieRepository = movieRepository;
        private readonly ILogger<MovieSearchService> _logger = logger;

        public Task<IReadOnlyCollection<Genre>> GetGenresAsync()
        {
            // TODO: This method should be async but using ToListAsync causes issues for unit tests. See README.
            var genres = _movieRepository.Genres.ToList();
            return Task.FromResult<IReadOnlyCollection<Genre>>(genres);
        }
    }
}
