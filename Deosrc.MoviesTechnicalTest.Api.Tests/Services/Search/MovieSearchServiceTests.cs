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

        /// <summary>
        /// A search string which will match against all results.
        /// </summary>
        private const string MatchAllSearch = "";

        /// <summary>
        /// Paging options a search which will return all matched results as a single page.
        /// </summary>
        private readonly PagingOptions AllResults = new()
        {
            // This value is not a valid use case, but is used for testing to ensure all matching results are returned.
            // Minus 1 is to ensure int doesn't overflow when checking for the next page.
            ItemsPerPage = int.MaxValue - 1,
            Page = 1
        };

        public MovieSearchServiceTests()
        {
            _searchMatch = _fixture.Build<Movie>()
                .With(x => x.Title, "Vegetables")
                .With(x => x.Genres, [
                    new Genre { Id = Guid.NewGuid(), Name = "Genre A" },
                    new Genre { Id = Guid.NewGuid(), Name = "Genre B" },
                    new Genre { Id = Guid.NewGuid(), Name = "Genre C" },
                ])
                .Create();

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

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(20)]
        public async Task SearchAsync_Paging_FirstPageWithMorePages(int pageSize)
        {
            _mockRepository
                .SetupGet(x => x.Movies)
                .Returns(_fixture.CreateMany<Movie>(100).AsQueryable());

            var results = await _sut.SearchAsync(
                MatchAllSearch,
                new PagingOptions()
                {
                    ItemsPerPage = pageSize,
                    Page = 1
                });

            Assert.Multiple(
                () => Assert.Equal(pageSize, results.Results.Count()),
                () => Assert.Equivalent(
                    new PageInfo()
                    {
                        ItemsPerPage = pageSize,
                        PageNumber = 1,
                        HasMorePages = true
                    },
                    results.Page)
                );
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(20)]
        public async Task SearchAsync_Paging_LaterPageWithMorePages(int pageSize)
        {
            _mockRepository
                .SetupGet(x => x.Movies)
                .Returns(_fixture.CreateMany<Movie>(100).AsQueryable());

            var results = await _sut.SearchAsync(
                MatchAllSearch,
                new PagingOptions()
                {
                    ItemsPerPage = pageSize,
                    Page = 2
                });

            Assert.Multiple(
                () => Assert.Equal(pageSize, results.Results.Count()),
                () => Assert.Equivalent(
                    new PageInfo()
                    {
                        ItemsPerPage = pageSize,
                        PageNumber = 2,
                        HasMorePages = true
                    },
                    results.Page)
                );
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(10, 1, 10)]
        [InlineData(15, 2, 5)]
        [InlineData(20, 2, 10)]
        public async Task SearchAsync_Paging_LastPage(int movieCount, int pageNumber, int expectedResultCount)
        {
            _mockRepository
                .SetupGet(x => x.Movies)
                .Returns(_fixture.CreateMany<Movie>(movieCount).AsQueryable());

            var results = await _sut.SearchAsync(
                MatchAllSearch,
                new PagingOptions()
                {
                    ItemsPerPage = 10,
                    Page = pageNumber
                });

            Assert.Multiple(
                () => Assert.Equal(expectedResultCount, results.Results.Count()),
                () => Assert.Equivalent(
                    new PageInfo()
                    {
                        ItemsPerPage = 10,
                        PageNumber = pageNumber,
                        HasMorePages = false
                    },
                    results.Page)
                );
        }
    }
}
