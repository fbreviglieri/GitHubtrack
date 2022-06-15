using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Swap.GithubTracker.Application.Interfaces;
using Swap.GithubTracker.Application.Services;
using Swap.GithubTracker.Domain.Configurations;
using Swap.GithubTracker.Domain.Interfaces.Repositories;
using Swap.GithubTracker.Domain.Interfaces.Services;
using Swap.GithubTracker.Infra.Data.Mapping;
using Swap.GithubTracker.Infra.Data.Repositories;
using Swap.GithubTracker.Infra.External.Services;

namespace Swap.GithubTracker.Infra.CrossCutting.IoC
{
    public static class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //IOptions Configurations
            services.Configure<DbGithubTrackerSettings>(configuration.GetSection("DbGithubTrackerSettings"));

            //Application Services
            services.AddTransient<IGithubTrackerApplicationService, GithubTrackerApplicationService>();

            //Domain 
            services.AddTransient<IGithubTrackRepository, GithubTrackRepository>();
            services.AddTransient<IGithubService, GithubService>();

            //Infra

            //Mongo Database
            services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(configuration.GetSection("DbGithubTrackerSettings").Get<DbGithubTrackerSettings>().MongoConnectionString);
            });

            //Mapping mongo entities
            MappingProfile.Map();
        }
    }
}
