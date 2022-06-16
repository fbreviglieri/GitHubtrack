using Swap.GithubTracker.Domain.Model;
using Swap.GithubTracker.Infra.External.Model;
using System.Linq;

namespace Swap.GithubTracker.Infra.External.Mappers
{
    public static class GithubTrackPostRequestMapper
    {
        public static GithubTrackPostRequest Map(GithubTrack model)
        {
            return new GithubTrackPostRequest
            {
                User = model.UserName,
                Repository = model.RepositoryName,
                Issues = model.Issues?.Select(x => new IssuePostRequest
                {
                    Author = x.Author,
                    Labels = x.Labels.Select(y => y).ToList(),
                    Title = x.Title
                }).ToList(),
                Contributors = model.Contributors?.Select(x => new ContributorPostRequest { Name = x.Name, User = x.User, Qtd_Commits = x.CommitsQuantity }).ToList()
            };
        }
    }
}
