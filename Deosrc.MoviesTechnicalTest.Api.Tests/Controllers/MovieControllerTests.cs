using AutoFixture;
using Deosrc.MoviesTechnicalTest.Api.Controllers;
using Deosrc.MoviesTechnicalTest.Api.Entities;
using Deosrc.MoviesTechnicalTest.Api.Models.Requests;
using Deosrc.MoviesTechnicalTest.Api.Models.Responses;
using Deosrc.MoviesTechnicalTest.Api.Services.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Deosrc.MoviesTechnicalTest.Api.Tests.Controllers
{
    public class MovieControllerTests
    {
        private readonly CustomFixture _fixture = new();
        private readonly Mock<IMovieSearchService> _mockMovieSearchService = new();
        private readonly MovieController _sut;

        public MovieControllerTests()
        {
            _sut = new(_mockMovieSearchService.Object, Mock.Of<ILogger<MovieController>>());

            _mockMovieSearchService
                .Setup(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<PagingOptions>()))
                .ReturnsAsync(new PagedResult<Movie>()
                {
                    Results = new List<Movie>()
                    {
                        new Movie
                        {
                            Id = Guid.Parse("a61053af-ff78-4350-981a-75febddfddd7"),
                            Title = "abc",
                            ReleaseDate = new DateOnly(2024, 04, 26),
                            Overview = "def",
                            Popularity = 12.3M,
                            VoteCount = 456,
                            VoteAverage = 7.89M,
                            OriginalLanguage = "ghi",
                            Genres = [
                                new Genre { Id = Guid.NewGuid(), Name = "jk" },
                                new Genre { Id = Guid.NewGuid(), Name = "lm" }
                            ],
                            PosterUrl = "opq",
                        }
                    },
                    Page = new PageInfo
                    {
                        ItemsPerPage = 12,
                        PageNumber = 3,
                        HasMorePages = true
                    }
                });
        }

        [Fact]
        public async Task SearchAsync_PerformsCorrectSearch()
        {
            var request = _fixture.Create<MovieSearchRequest>();

            var result = await _sut.SearchAsync(request);

            Assert.Multiple(
                () => _mockMovieSearchService.Verify(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<PagingOptions>()), Times.Once()),
                () => _mockMovieSearchService.Verify(x => x.SearchAsync(request.Title, request.Paging), Times.Once())
                );
        }

        [Fact]
        public async Task SearchAsync_CorrectlyConvertsResult()
        {
            var request = new MovieSearchRequest()
            {
                Title = "abc",
                Paging = new PagingOptions()
                {
                    ItemsPerPage = 12,
                    Page = 3
                }
            };

            var result = await _sut.SearchAsync(request);

            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;

            var expected = new PagedResult<MovieResponse>()
            {
                Results =
                [
                    new()
                    {
                        Id = Guid.Parse("a61053af-ff78-4350-981a-75febddfddd7"),
                        Title = "abc",
                        ReleaseDate = "2024-04-26",
                        Overview = "def",
                        Popularity = 12.3,
                        VoteCount = 456,
                        VoteAverage = 7.89,
                        OriginalLanguage = "ghi",
                        Genres = ["jk" , "lm"],
                        PosterUrl = "opq",
                    }
                ],
                Page = new PageInfo
                {
                    ItemsPerPage = 12,
                    PageNumber = 3,
                    HasMorePages = true
                }
            };
            Assert.Equivalent(expected, objectResult.Value);
        }
    }
}
