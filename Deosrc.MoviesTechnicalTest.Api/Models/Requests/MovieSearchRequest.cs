using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Deosrc.MoviesTechnicalTest.Api.Entities;

namespace Deosrc.MoviesTechnicalTest.Api.Models.Requests
{
    public record MovieSearchRequest
    {
        [Required]
        [MinLength(1)]
        public required string Title { get; set; }

        [DefaultValue(null)]
        public string? Genre { get; set; }

        public PagingOptions Paging { get; set; } = new();
    }
}
