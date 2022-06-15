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

        public GithubTrackerApplicationService(ILogger<GithubTrackerApplicationService> logger, IGithubTrackRepository githubTrackRepository, IGithubService githubService)
        {
            _logger = logger;
            _githubTrackRepository = githubTrackRepository;
            _githubService = githubService;
        }

        public async Task<bool> AddGithubTrackAsync(RequestGithubTrackViewModel request)
        {
            var issues = await _githubService.GetIssues(request.NomeUsuario, request.NomeRepositorio);
            var contributors = await _githubService.GetContributors(request.NomeUsuario, request.NomeRepositorio);
            var githubTrack = new GithubTrack(request.NomeUsuario, request.NomeRepositorio, issues.Items, contributors);
            foreach (var contributor in githubTrack.Contributors)
            {
                contributor.SetName(await _githubService.GetUserName(contributor.User));
            }
            var result = await _githubTrackRepository.InsertAsync(githubTrack);

            return result != null;
        }
    }
}
