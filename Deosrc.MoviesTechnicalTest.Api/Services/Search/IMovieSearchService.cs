using Deosrc.MoviesTechnicalTest.Api.Entities;

namespace Deosrc.MoviesTechnicalTest.Api.Services.Search
{
    public interface IMovieSearchService
    {
        public Task<IEnumerable<Movie>> SearchAsync(string title, PagingOptions pagingOptions);
    }
}
