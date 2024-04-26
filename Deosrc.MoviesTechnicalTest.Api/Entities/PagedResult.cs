namespace Deosrc.MoviesTechnicalTest.Api.Entities
{
    public record PagedResult<T>
    {
        public required IEnumerable<T> Results { get; set; }
        public required PageInfo Page { get; set; }
    }
}
