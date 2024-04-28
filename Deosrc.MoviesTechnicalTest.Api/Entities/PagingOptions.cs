using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Deosrc.MoviesTechnicalTest.Api.Entities
{
    public record PagingOptions
    {
        [Range(1, 100)]
        [DefaultValue(30)]
        public int ItemsPerPage { get; set; }

        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int Page { get; set; } = 1;
    }
}
