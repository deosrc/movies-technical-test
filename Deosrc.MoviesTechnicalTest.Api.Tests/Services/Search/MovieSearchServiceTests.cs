using AutoFixture;
using Deosrc.MoviesTechnicalTest.Api.DataAccess;
using Deosrc.MoviesTechnicalTest.Api.Entities;
using Deosrc.MoviesTechnicalTest.Api.Services.Search;
using Microsoft.Extensions.Logging;
using Moq;

namespace Deosrc.MoviesTechnicalTest.Api.Tests.Services.Search
{
    public class MovieSearchServiceTests
    {
        private readonly CustomFixture _fixture = new();
        private readonly Mock<IMovieReadOnlyRepository> _mockRepository = new();
        private readonly MovieSearchService _sut;

        private readonly Movie _searchMatch;

        private readonly PagingOptions AllResults = new()
        {
            // This value is not a valid use case, but is used for testing to ensure all matching results are returned.
            // Minus 1 is to ensure int doesn't overflow when checking for the next page.
            ItemsPerPage = int.MaxValue - 1,
            Page = 1
        };

        public MovieSearchServiceTests()
        {
            _searchMatch = _fixture.Build<Movie>().With(x => x.Title, "Vegetables").Create();

            _mockRepository
                .SetupGet(x => x.Movies)
                .Returns(new List<Movie>()
                {
                    _fixture.Build<Movie>().With(x => x.Title, "Fruits").Create(),
                    _fixture.Build<Movie>().With(x => x.Title, "Salad").Create(),
                    _searchMatch
                }.AsQueryable());

            _sut = new(_mockRepository.Object, Mock.Of<ILogger<MovieSearchService>>());
        }

        [Theory]
        [InlineData("Case insensitive", "VEGETABLES")]
        [InlineData("Partial match", "etab")]
        public async Task SearchAsync_BasicSearch_MatchesResults(string _, string search)
        {
            var results = await _sut.SearchAsync(search, AllResults);

            Assert.Collection(
                results.Results,
                x => Assert.Equal(_searchMatch, x)
                );
        }

        [Fact]
        public async Task SearchAsync_NoMatches()
        {
            var results = await _sut.SearchAsync("a b c", AllResults);
            Assert.Empty(results.Results);
        }
    }
}
