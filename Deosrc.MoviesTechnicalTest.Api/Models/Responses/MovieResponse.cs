namespace Deosrc.MoviesTechnicalTest.Api.Models.Responses
{
    public record MovieResponse
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string ReleaseDate { get; set; }
        public required string Overview { get; set; }
        public required decimal Popularity { get; set; }
        public required int VoteCount { get; set; }
        public required decimal VoteAverage { get; set; }
        public required string OriginalLanguage { get; set; }
        public required string[] Genres { get; set; }
        public required string PosterUrl { get; set; }
    }
}
