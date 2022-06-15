using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Swap.GithubTracker.Domain.Configurations;
using Swap.GithubTracker.Domain.Interfaces.Repositories;
using Swap.GithubTracker.Domain.Model;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Infra.Data.Repositories
{
    public class GithubTrackRepository : IGithubTrackRepository
    {
        private const string _nameCollection = "gitHubTrack";
        private readonly IMongoDatabase _database;

        public GithubTrackRepository(IMongoClient client, IOptions<DbGithubTrackerSettings> settings)
        {
            _database = client.GetDatabase(settings.Value.NomeDataBase);
        }

        public async Task<GithubTrack> InsertAsync(GithubTrack model)
        {
            var collection = _database.GetCollection<GithubTrack>(_nameCollection);
            await collection.InsertOneAsync(model);
            return model;
        }
    }
}
