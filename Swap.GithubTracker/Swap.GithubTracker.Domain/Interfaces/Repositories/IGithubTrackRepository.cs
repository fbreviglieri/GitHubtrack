using Swap.GithubTracker.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Domain.Interfaces.Repositories
{
    public interface IGithubTrackRepository
    {
        Task<List<GithubTrack>> GetScheduledReadyToProcessAsync();
        Task<GithubTrack> InsertAsync(GithubTrack model);
        Task<GithubTrack> UpdateAsync(GithubTrack model);
    }
}
