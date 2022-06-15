using Octokit;
using Swap.GithubTracker.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Infra.External.Services
{
    public class GithubService : IGithubService
    {
        public GithubService()
        {
        }
        public async Task<SearchIssuesResult> GetIssues(string user, string repo)
        {            
            var client = new GitHubClient(new ProductHeaderValue(user));            
            SearchIssuesResult result = await client.Search.SearchIssues(
              new SearchIssuesRequest()
              {                  
                  Repos = new RepositoryCollection { $"{user}/{repo}" }
              });

            return result;
        }

        public async Task<IReadOnlyList<RepositoryContributor>> GetContributors(string user, string repo)
        {
            var client = new GitHubClient(new ProductHeaderValue(user));
            var result = await client.Repository.GetAllContributors(user, repo);
            return result;
        }

        public async Task<string> GetUserName(string user)
        {
            var client = new GitHubClient(new ProductHeaderValue(user));
            var result = await client.User.Get(user);
            return result != null ? result.Name : "";            
        }
    }
}
