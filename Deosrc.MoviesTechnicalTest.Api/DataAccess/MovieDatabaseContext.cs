using Deosrc.MoviesTechnicalTest.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deosrc.MoviesTechnicalTest.Api.DataAccess
{
    public class MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies { get; set; }
    }
}
