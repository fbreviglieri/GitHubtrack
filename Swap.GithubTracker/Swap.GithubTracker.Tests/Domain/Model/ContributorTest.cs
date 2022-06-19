using Swap.GithubTracker.Domain.Model;
using Xunit;

namespace Swap.GithubTracker.Tests.Domain.Model
{
    public class ContributorTest
    {
        [Fact]
        public void Contributor_SetName_ShouldSetName()
        {
            //arrange
            var contributor = new Contributor("userTest", 2);
            //act
            contributor.SetName("nameTest");
            //assert
            Assert.Equal("nameTest", contributor.Name);
        }
    }
}
