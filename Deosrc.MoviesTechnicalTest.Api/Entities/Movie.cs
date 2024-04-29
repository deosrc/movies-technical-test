using System.ComponentModel.DataAnnotations.Schema;

namespace Deosrc.MoviesTechnicalTest.Api.Entities
{
    public record Movie
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required DateOnly ReleaseDate { get; set; }
        public required string Overview { get; set; }
        public required decimal Popularity { get; set; }
        public required int VoteCount { get; set; }
        public required decimal VoteAverage { get; set; }
        public required string OriginalLanguage { get; set; }
        public required string PosterUrl { get; set; }

        public ICollection<Genre> Genres { get; set; } = [];
    }
}
