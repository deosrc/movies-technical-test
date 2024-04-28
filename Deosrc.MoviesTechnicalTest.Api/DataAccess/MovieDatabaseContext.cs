using Deosrc.MoviesTechnicalTest.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deosrc.MoviesTechnicalTest.Api.DataAccess
{
    public class MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options) : DbContext(options), IMovieReadOnlyRepository
    {
        public DbSet<Movie> Movies { get; set; }

        IQueryable<Movie> IMovieReadOnlyRepository.Movies => Movies.AsNoTracking();
    }
}
