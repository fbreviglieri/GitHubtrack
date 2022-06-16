using Swap.GithubTracker.Application.ViewModels;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Application.Interfaces
{
    public interface IGithubTrackerApplicationService
    {
        Task<bool> AddGithubTrackAsync(RequestGithubTrackViewModel request);
        Task ProcessScheduledTracksAsync();
    }
}
