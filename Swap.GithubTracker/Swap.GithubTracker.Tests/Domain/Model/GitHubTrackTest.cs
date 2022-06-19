using Xunit;

namespace Swap.GithubTracker.Tests.Domain.Model
{
    public class GitHubTrackTest
    {
        [Fact]
        public void GitHubTrack_ChangeProcessed_ShouldChangeProcessed()
        {
            //arrange
            var gitHubTrack = FakeObjects.FakeGithubTrack.CreateOne();
            var processedBefore = gitHubTrack.Processed;
            //act
            gitHubTrack.ChangeProcessed();
            //assert
            Assert.NotEqual(processedBefore, gitHubTrack.Processed);
        }
    }
}
