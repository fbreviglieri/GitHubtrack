using Swap.GithubTracker.Domain.Model;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Domain.Interfaces.Repositories
{
    public interface IGithubTrackRepository
    {
        Task<GithubTrack> InsertAsync(GithubTrack model);
    }
}
