using Microsoft.Extensions.Logging;
using Moq;
using Swap.GithubTracker.Application.Services;
using Swap.GithubTracker.Application.ViewModels;
using Swap.GithubTracker.Domain.Interfaces.Repositories;
using Swap.GithubTracker.Domain.Interfaces.Services;
using Swap.GithubTracker.Domain.Model;
using Swap.GithubTracker.Tests.FakeObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Swap.GithubTracker.Tests.Application
{
    public class GithubTrackerApplicationServiceTest
    {

        private readonly Mock<ILogger<GithubTrackerApplicationService>> _logger;
        private readonly Mock<IGithubTrackRepository> _githubTrackRepository;
        private readonly Mock<IGithubService> _githubService;
        private readonly Mock<IWebHookService> _webHookService;
        private readonly GithubTrackerApplicationService _githubTrackerApplicationService;

        public GithubTrackerApplicationServiceTest()
        {
            _logger = new Mock<ILogger<GithubTrackerApplicationService>>();
            _githubTrackRepository = new Mock<IGithubTrackRepository>();
            _githubService = new Mock<IGithubService>();
            _webHookService = new Mock<IWebHookService>();

            _githubTrackerApplicationService = new GithubTrackerApplicationService(_logger.Object, _githubTrackRepository.Object,
                                                                                   _githubService.Object, _webHookService.Object);

        }
        [Fact]
        public async Task GithubTrackerApplicationService_AddGithubTrackAsync_ShouldAddTrack()
        {
            //arrange
            var request = new RequestGithubTrackViewModel { UserName = "userTest", RepositoryName = "repositoryTest" };
            var contribs = FakeContributorResult.CreateContributorResult(2);

            _githubService.Setup(x => x.GetIssues(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(FakeSearchIssueResult.CreateSearchIssuesResult(2));
            _githubService.Setup(x => x.GetContributors(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(contribs);
            _githubService.Setup(x => x.GetUserName(It.IsAny<string>()));
            _githubTrackRepository.Setup(x => x.InsertAsync(It.IsAny<GithubTrack>()));

            //act
            await _githubTrackerApplicationService.AddGithubTrackAsync(request);

            //assert
            _githubService.Verify(x => x.GetIssues(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _githubService.Verify(x => x.GetContributors(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _githubService.Verify(x => x.GetUserName(It.IsAny<string>()), Times.Exactly(contribs.Count));
            _githubTrackRepository.Verify(x => x.InsertAsync(It.IsAny<GithubTrack>()), Times.Once);
        }

        [Fact]
        public async Task GithubTrackerApplicationService_AddGithubTrackAsyncWithoutContributors_ShouldAddTrack()
        {
            //arrange
            var request = new RequestGithubTrackViewModel { UserName = "userTest", RepositoryName = "repositoryTest" };

            _githubService.Setup(x => x.GetIssues(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(FakeSearchIssueResult.CreateSearchIssuesResult(2));
            _githubService.Setup(x => x.GetContributors(It.IsAny<string>(), It.IsAny<string>()));
            _githubService.Setup(x => x.GetUserName(It.IsAny<string>()));
            _githubTrackRepository.Setup(x => x.InsertAsync(It.IsAny<GithubTrack>()));

            //act
            await _githubTrackerApplicationService.AddGithubTrackAsync(request);

            //assert
            _githubService.Verify(x => x.GetIssues(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _githubService.Verify(x => x.GetContributors(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _githubService.Verify(x => x.GetUserName(It.IsAny<string>()), Times.Never);
            _githubTrackRepository.Verify(x => x.InsertAsync(It.IsAny<GithubTrack>()), Times.Once);
        }

        [Fact]
        public async Task GithubTrackerApplicationService_AddGithubTrackAsyncWithoutIssues_ShouldAddTrack()
        {
            //arrange
            var request = new RequestGithubTrackViewModel { UserName = "userTest", RepositoryName = "repositoryTest" };

            _githubService.Setup(x => x.GetIssues(It.IsAny<string>(), It.IsAny<string>()));
            _githubService.Setup(x => x.GetContributors(It.IsAny<string>(), It.IsAny<string>()));
            _githubService.Setup(x => x.GetUserName(It.IsAny<string>()));
            _githubTrackRepository.Setup(x => x.InsertAsync(It.IsAny<GithubTrack>()));

            //act
            await _githubTrackerApplicationService.AddGithubTrackAsync(request);

            //assert
            _githubService.Verify(x => x.GetIssues(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _githubService.Verify(x => x.GetContributors(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _githubService.Verify(x => x.GetUserName(It.IsAny<string>()), Times.Never);
            _githubTrackRepository.Verify(x => x.InsertAsync(It.IsAny<GithubTrack>()), Times.Once);
        }


        [Fact]
        public async Task GithubTrackerApplicationService_AddGithubTrackAsyncWithoutRepository_ShouldThrowException()
        {
            //arrange
            var request = new RequestGithubTrackViewModel { UserName = "userTest", RepositoryName = "repositoryTest" };
            var contribs = FakeContributorResult.CreateContributorResult(2);

            _githubService.Setup(x => x.GetIssues(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            _githubService.Setup(x => x.GetContributors(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(contribs);
            _githubService.Setup(x => x.GetUserName(It.IsAny<string>()));
            _githubTrackRepository.Setup(x => x.InsertAsync(It.IsAny<GithubTrack>()));

            //act
            await Assert.ThrowsAsync<Exception>(() => _githubTrackerApplicationService.AddGithubTrackAsync(request));

            //assert
            _githubService.Verify(x => x.GetIssues(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _githubService.Verify(x => x.GetContributors(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _githubService.Verify(x => x.GetUserName(It.IsAny<string>()), Times.Never);
            _githubTrackRepository.Verify(x => x.InsertAsync(It.IsAny<GithubTrack>()), Times.Never);
        }

        [Fact]
        public async Task GithubTrackerApplicationService_ProcessScheduledTracksAsync_ShouldSendTracksToWebHookAndUpdateDatabase()
        {
            //arrange            
            var githubTrackList = FakeGithubTrack.CreateList(3);

            _githubTrackRepository.Setup(x => x.GetScheduledReadyToProcessAsync()).ReturnsAsync(githubTrackList);
            _webHookService.Setup(x => x.PostGithubTrack(It.IsAny<GithubTrack>())).ReturnsAsync(true);
            _githubTrackRepository.Setup(x => x.UpdateAsync(It.IsAny<GithubTrack>()));

            //act
            await _githubTrackerApplicationService.ProcessScheduledTracksAsync();

            //assert
            _githubTrackRepository.Verify(x => x.GetScheduledReadyToProcessAsync(), Times.Once);
            _webHookService.Verify(x => x.PostGithubTrack(It.IsAny<GithubTrack>()), Times.Exactly(githubTrackList.Count));
            _githubTrackRepository.Verify(x => x.UpdateAsync(It.IsAny<GithubTrack>()), Times.Exactly(githubTrackList.Count));
        }

        [Fact]
        public async Task GithubTrackerApplicationService_ProcessScheduledTracksAsyncFailSendWebHook_ShouldNotUpdateDatabase()
        {
            //arrange            
            var githubTrackList = FakeGithubTrack.CreateList(2);

            _githubTrackRepository.Setup(x => x.GetScheduledReadyToProcessAsync()).ReturnsAsync(githubTrackList);
            _webHookService.Setup(x => x.PostGithubTrack(It.IsAny<GithubTrack>())).ReturnsAsync(false);
            _githubTrackRepository.Setup(x => x.UpdateAsync(It.IsAny<GithubTrack>()));

            //act
            await _githubTrackerApplicationService.ProcessScheduledTracksAsync();

            //assert
            _githubTrackRepository.Verify(x => x.GetScheduledReadyToProcessAsync(), Times.Once);
            _webHookService.Verify(x => x.PostGithubTrack(It.IsAny<GithubTrack>()), Times.Exactly(githubTrackList.Count));
            _githubTrackRepository.Verify(x => x.UpdateAsync(It.IsAny<GithubTrack>()), Times.Never);
        }

        [Fact]
        public async Task GithubTrackerApplicationService_ProcessScheduledTracksAsyncWithoutTracksToProcess_ShouldNotHitWebHookAndUpdateDatabase()
        {
            //arrange                        
            _githubTrackRepository.Setup(x => x.GetScheduledReadyToProcessAsync()).ReturnsAsync(new List<GithubTrack>());
            _webHookService.Setup(x => x.PostGithubTrack(It.IsAny<GithubTrack>())).ReturnsAsync(false);
            _githubTrackRepository.Setup(x => x.UpdateAsync(It.IsAny<GithubTrack>()));

            //act
            await _githubTrackerApplicationService.ProcessScheduledTracksAsync();

            //assert
            _githubTrackRepository.Verify(x => x.GetScheduledReadyToProcessAsync(), Times.Once);
            _webHookService.Verify(x => x.PostGithubTrack(It.IsAny<GithubTrack>()), Times.Never);
            _githubTrackRepository.Verify(x => x.UpdateAsync(It.IsAny<GithubTrack>()), Times.Never);

        }
    }
}
