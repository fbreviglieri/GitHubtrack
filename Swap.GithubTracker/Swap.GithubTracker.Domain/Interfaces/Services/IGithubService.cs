using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Domain.Interfaces.Services
{
    public interface IGithubService
    {
        Task<IReadOnlyList<RepositoryContributor>> GetContributors(string user, string repo);
        Task<SearchIssuesResult> GetIssues(string user, string repo);
        Task<string> GetUserName(string user);
    }
}
