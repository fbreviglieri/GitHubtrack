using Bogus;
using Swap.GithubTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swap.GithubTracker.Tests.FakeObjects
{
    public static class FakeGithubTrack
    {
        public static List<GithubTrack> CreateList(int qtyList)
        {
            return CreateOneFaker()
                .GenerateLazy(qtyList)
                .ToList();
        }

        private static Faker<GithubTrack> CreateOneFaker()
        {
           return new Faker<GithubTrack>()
                .RuleFor(s => s.Id, f => Guid.NewGuid().ToString())
                .RuleFor(s => s.Contributors, f => CreateContributorList(3))
                .RuleFor(s => s.Issues, f => CreateIssueList(2))
                .RuleFor(s => s.Processed, f => false)
                .RuleFor(s => s.RepositoryName, f => f.Random.AlphaNumeric(10))
                .RuleFor(s => s.UserName, f => f.Random.AlphaNumeric(10))
                .RuleFor(s => s.CreatedAt, f => System.DateTime.Now.AddDays(-1));
        }

        public static GithubTrack CreateOne()
        {
            return CreateOneFaker().Generate();
        }

        public static List<Contributor> CreateContributorList(int qtyList)
        {
            return new Faker<Contributor>()
                .RuleFor(s => s.Name, f => f.Random.AlphaNumeric(10))
                .RuleFor(s => s.User, f => f.Random.AlphaNumeric(6))
                .RuleFor(s => s.CommitsQuantity, f => f.Random.Int(1, 100))
                .GenerateLazy(qtyList)
                .ToList();
        }

        public static List<Issue> CreateIssueList(int qtyList)
        {
            return new Faker<Issue>()
                .RuleFor(s => s.Author, f => f.Random.AlphaNumeric(10))
                .RuleFor(s => s.Title, f => f.Random.AlphaNumeric(6))
                .RuleFor(s => s.Labels, f => new List<string> {"Bug", "Issue"})
                .GenerateLazy(qtyList)
                .ToList();
        }
    }
}
