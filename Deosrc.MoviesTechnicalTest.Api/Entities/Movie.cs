namespace Deosrc.MoviesTechnicalTest.Api.Entities
{
    public record Movie
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required DateOnly ReleaseDate { get; set; }
        public required string Overview { get; set; }
        public required double Popularity { get; set; }
        public required int VoteCount { get; set; }
        public required double VoteAverage { get; set; }
        public required string OriginalLanguage { get; set; }
        public required string Genre { get; set; }
        public required string PosterUrl { get; set; }

        public string[] Genres => Genre.Split(',').Select(x => x.Trim()).ToArray();
    }
}
