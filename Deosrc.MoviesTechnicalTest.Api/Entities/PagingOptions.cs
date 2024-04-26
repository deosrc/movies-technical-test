using System.ComponentModel.DataAnnotations;

namespace Deosrc.MoviesTechnicalTest.Api.Entities
{
    public record PagingOptions
    {
        [Range(1, 100)]
        public int ItemsPerPage { get; set; } = 30;

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;
    }
}
