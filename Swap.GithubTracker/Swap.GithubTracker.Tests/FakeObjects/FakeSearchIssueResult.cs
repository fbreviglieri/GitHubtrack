using Bogus;
using Octokit;
using System.Collections.Generic;
using System.Linq;

namespace Swap.GithubTracker.Tests.FakeObjects
{
    public static class FakeSearchIssueResult
    {
        public static SearchIssuesResult CreateSearchIssuesResult(int qtyItems)
        {
            return new Faker<SearchIssuesResult>()
                .RuleFor(s => s.Items, f => CreateItems(qtyItems))                
                .RuleFor(s => s.TotalCount, f => f.Random.Int(1,5));         
        }

        public static IReadOnlyList<Issue> CreateItems(int qty)
        {
            return new Faker<Issue>()
                .RuleFor(s => s.User, f => CreateUser())
                .RuleFor(s => s.Labels, f => CreateLabel(qty))
                .GenerateLazy(qty)
                .ToList();                
        }        
        public static User CreateUser()
        {
            return new Faker<User>()
                .RuleFor(s => s.Login, f => f.Random.AlphaNumeric(6));                
        }
        public static IReadOnlyList<Label> CreateLabel(int qty)
        {
            return new Faker<Label>()
                .RuleFor(s => s.Name, f => f.Random.AlphaNumeric(6))
                .GenerateLazy(qty)
                .ToList();
        }
    }
}
