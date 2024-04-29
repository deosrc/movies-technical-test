using Deosrc.MoviesTechnicalTest.Api.Entities;

namespace Deosrc.MoviesTechnicalTest.Api.Services.Lookups
{
    public interface ILookupService
    {
        Task<IReadOnlyCollection<Genre>> GetGenresAsync();
    }
}
