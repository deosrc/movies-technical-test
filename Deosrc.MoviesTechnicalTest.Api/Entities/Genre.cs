namespace Deosrc.MoviesTechnicalTest.Api.Entities
{
    public record Genre
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Movie> Movies { get; set; } = [];
    }
}
