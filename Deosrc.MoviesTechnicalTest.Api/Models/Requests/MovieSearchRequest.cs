namespace Deosrc.MoviesTechnicalTest.Api.Models.Requests
{
    public record MovieSearchRequest
    {
        public required string Title { get; set; }
    }
}
