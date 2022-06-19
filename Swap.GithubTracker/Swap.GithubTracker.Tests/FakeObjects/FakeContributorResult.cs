using Bogus;
using Octokit;
using System.Collections.Generic;
using System.Linq;

namespace Swap.GithubTracker.Tests.FakeObjects
{
    public static class FakeContributorResult
    {
        public static IReadOnlyList<RepositoryContributor> CreateContributorResult(int qtyContrib)
        {
            return new Faker<RepositoryContributor>()
                .RuleFor(s => s.Contributions, f => f.Random.Int(1, 5))
                .RuleFor(s => s.Login, f => f.Random.AlphaNumeric(6))
                .GenerateLazy(qtyContrib)
                .ToList();
        }
    }
}
