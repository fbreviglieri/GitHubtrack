using Microsoft.Extensions.Logging;
using Swap.GithubTracker.Application.Interfaces;
using Swap.GithubTracker.Application.ViewModels;
using Swap.GithubTracker.Domain.Interfaces.Repositories;
using Swap.GithubTracker.Domain.Interfaces.Services;
using Swap.GithubTracker.Domain.Model;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Application.Services
{
    public class GithubTrackerApplicationService : IGithubTrackerApplicationService
    {
        private readonly ILogger<GithubTrackerApplicationService> _logger;
        private readonly IGithubTrackRepository _githubTrackRepository;
        private readonly IGithubService _githubService;
        private readonly IWebHookService _webHookService;

        public GithubTrackerApplicationService(ILogger<GithubTrackerApplicationService> logger, IGithubTrackRepository githubTrackRepository, IGithubService githubService, IWebHookService webHookService)
        {
            _logger = logger;
            _githubTrackRepository = githubTrackRepository;
            _githubService = githubService;
            _webHookService = webHookService;
        }

        public async Task<bool> AddGithubTrackAsync(RequestGithubTrackViewModel request)
        {
            var issues = await _githubService.GetIssues(request.UserName, request.RepositoryName);
            var contributors = await _githubService.GetContributors(request.UserName, request.RepositoryName);
            var githubTrack = new GithubTrack(request.UserName, request.RepositoryName, issues.Items, contributors);
            foreach (var contributor in githubTrack.Contributors)
            {
                contributor.SetName(await _githubService.GetUserName(contributor.User));
            }
            var result = await _githubTrackRepository.InsertAsync(githubTrack);

            return result != null;
        }

        public async Task<bool> ProcessScheduledTracksAsync()
        {
            var trackList = await _githubTrackRepository.GetScheduledReadyToProcessAsync();
            if (trackList != null)
            {
                foreach (var track in trackList)
                {
                    //enviar para webhook
                    await _webHookService.PostGithubTrack(track);
                    //atualizar mongo para processado
                }
            }
            return true;
        }
    }
}
