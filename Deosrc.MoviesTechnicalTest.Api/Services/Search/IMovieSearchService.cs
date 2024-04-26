using Deosrc.MoviesTechnicalTest.Api.Entities;

namespace Deosrc.MoviesTechnicalTest.Api.Services.Search
{
    public interface IMovieSearchService
    {
        public Task<PagedResult<Movie>> SearchAsync(string title, PagingOptions pagingOptions);
    }
}
