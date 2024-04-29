using Deosrc.MoviesTechnicalTest.Api.Controllers;
using Deosrc.MoviesTechnicalTest.Api.Entities;
using Deosrc.MoviesTechnicalTest.Api.Services.Lookups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Deosrc.MoviesTechnicalTest.Api.Tests.Controllers
{
    public class GenreControllerTests
    {
        private readonly CustomFixture _fixture = new();
        private readonly Mock<ILookupService> _mockLookupService = new();
        private readonly GenreController _sut;

        public GenreControllerTests()
        {
            _sut = new(_mockLookupService.Object, Mock.Of<ILogger<GenreController>>());

            _mockLookupService
                .Setup(x => x.GetGenresAsync())
                .ReturnsAsync(new List<Genre>
                {
                    new() { Id = Guid.NewGuid(), Name = "Genre A" },
                    new() { Id = Guid.NewGuid(), Name = "Genre B" },
                    new() { Id = Guid.NewGuid(), Name = "Genre C" }
                });
        }

        [Fact]
        public async Task GetAsync_ReturnsAllGenres()
        {
            var result = await _sut.GetAsync();

            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;

            Assert.Equivalent(new[]
                {
                    "Genre A",
                    "Genre B",
                    "Genre C",
                },
                objectResult.Value);
        }
    }
}
