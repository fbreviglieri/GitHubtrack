using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Swap.GithubTracker.Domain.Configurations;
using Swap.GithubTracker.Domain.Interfaces.Repositories;
using Swap.GithubTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Infra.Data.Repositories
{
    public class GithubTrackRepository : IGithubTrackRepository
    {
        private const string _nameCollection = "gitHubTrack";
        private readonly IMongoDatabase _database;
        private readonly int _diffHours;

        public GithubTrackRepository(IMongoClient client, IOptions<DbGithubTrackerSettings> settings)
        {
            _database = client.GetDatabase(settings.Value.NomeDataBase);
            _diffHours = settings.Value.DiffIntervalHours;
        }

        public async Task<GithubTrack> InsertAsync(GithubTrack model)
        {
            var collection = _database.GetCollection<GithubTrack>(_nameCollection);
            await collection.InsertOneAsync(model);
            return model;
        }

        public async Task<GithubTrack> UpdateAsync(GithubTrack model)
        {
            var collection = _database.GetCollection<GithubTrack>(_nameCollection);
            var filter = Builders<GithubTrack>.Filter.Eq(f => f.Id, model.Id);
            var update = Builders<GithubTrack>.Update
                .Set(u => u.Processed, model.Processed);

            await collection.UpdateOneAsync(filter, update);
            return model;
        }

        /// <summary>
        ///   Get All GithubTrack with flag Processed: False and created 24+ hours ago
        /// </summary>
        /// <returns>GithubTrack list to be processed</returns>
        public async Task<List<GithubTrack>> GetScheduledReadyToProcessAsync()
        {
            var collection = _database.GetCollection<GithubTrack>(_nameCollection);
            return await collection.AsQueryable()
                .Where(x => !x.Processed && (x.CreatedAt < DateTime.UtcNow.Subtract(TimeSpan.FromHours(_diffHours))))
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
