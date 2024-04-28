using AutoFixture;

namespace Deosrc.MoviesTechnicalTest.Api.Tests
{
    public class CustomFixture : Fixture
    {
        public CustomFixture()
            : base()
        {
            Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        }
    }
}
