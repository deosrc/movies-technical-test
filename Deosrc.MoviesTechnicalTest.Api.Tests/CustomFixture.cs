using AutoFixture;

namespace Deosrc.MoviesTechnicalTest.Api.Tests
{
    public class CustomFixture : Fixture
    {
        public CustomFixture()
            : base()
        {
            Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));

            // Turn off recursion to avoid EF 
            Behaviors.Remove(Behaviors.OfType<ThrowingRecursionBehavior>().First());
            Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
