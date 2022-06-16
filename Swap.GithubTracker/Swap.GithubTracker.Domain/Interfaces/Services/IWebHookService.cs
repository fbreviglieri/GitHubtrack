using Swap.GithubTracker.Domain.Model;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Domain.Interfaces.Services
{
    public interface IWebHookService
    {
        Task<bool> PostGithubTrack(GithubTrack request);
    }
}
