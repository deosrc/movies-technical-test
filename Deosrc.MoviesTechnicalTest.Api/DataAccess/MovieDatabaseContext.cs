using Deosrc.MoviesTechnicalTest.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deosrc.MoviesTechnicalTest.Api.DataAccess
{
    public class MovieDatabaseContext(DbContextOptions<MovieDatabaseContext> options) : DbContext(options), IMovieReadOnlyRepository
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        IQueryable<Movie> IMovieReadOnlyRepository.Movies => Movies
            .Include(m => m.Genres)
            .AsNoTracking();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity(
                    "MovieGenres",
                    l => l.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId").HasConstraintName("fk_Genre"),
                    r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("MovieId").HasConstraintName("fk_Movie"));
        }
    }
}
