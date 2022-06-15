using System;
using System.Collections.Generic;
using System.Linq;

namespace Swap.GithubTracker.Domain.Model
{
    public class GithubTrack
    {
        public GithubTrack(string user, string repository, IReadOnlyList<Octokit.Issue> issues, IReadOnlyList<Octokit.RepositoryContributor> contributors)
        {
            this.UserName = user;
            this.RepositoryName = repository;
            this.Contributors = contributors.Select(x => new Contributor( x.Login, x.Contributions)).ToList();
            this.Issues = issues.Select(x => new Issue(x.Title, x.User.Login, x.Labels.Select(y => y.Name).ToList())).ToList();
            this.CreatedAt = DateTime.UtcNow;
        }

        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string RepositoryName { get; private set; }
        public List<Contributor> Contributors { get; private set; }
        public List<Issue> Issues { get; private set; }
        public DateTime CreatedAt { get; private set; }


    }
}
