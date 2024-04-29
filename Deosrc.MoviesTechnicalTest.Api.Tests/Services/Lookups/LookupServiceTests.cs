using Deosrc.MoviesTechnicalTest.Api.DataAccess;
using Deosrc.MoviesTechnicalTest.Api.Entities;
using Deosrc.MoviesTechnicalTest.Api.Services.Lookups;
using Microsoft.Extensions.Logging;
using Moq;

namespace Deosrc.MoviesTechnicalTest.Api.Tests.Services.Lookups
{
    public class LookupServiceTests
    {
        private readonly CustomFixture _fixture = new();
        private readonly Mock<IMovieReadOnlyRepository> _mockRepository = new();
        private readonly LookupService _sut;

        private readonly IReadOnlyCollection<Genre> _genres = new List<Genre>()
        {
            new() { Id = Guid.NewGuid(), Name = "Genre A" },
            new() { Id = Guid.NewGuid(), Name = "Genre B" },
            new() { Id = Guid.NewGuid(), Name = "Genre C" }
        };

        public LookupServiceTests()
        {
            _mockRepository
                .SetupGet(x => x.Genres)
                .Returns(_genres.AsQueryable());

            _sut = new(_mockRepository.Object, Mock.Of<ILogger<LookupService>>());
        }

        [Fact]
        public async Task GetGenresAsync_ReturnsAllGenres()
        {
            var results = await _sut.GetGenresAsync();
            Assert.Equivalent(_genres, results);
        }
    }
}
