using Deosrc.MoviesTechnicalTest.Api.Entities;

namespace Deosrc.MoviesTechnicalTest.Api.DataAccess
{
    public interface IMovieReadOnlyRepository
    {
        public IQueryable<Movie> Movies { get; }
        public IQueryable<Genre> Genres { get; }
    }
}
