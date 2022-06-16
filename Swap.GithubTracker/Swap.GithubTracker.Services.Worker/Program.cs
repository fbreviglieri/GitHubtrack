using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swap.GithubTracker.Domain.Configurations;
using Swap.GithubTracker.Infra.CrossCutting.IoC;

namespace Swap.GithubTracker.Services.Worker
{
    public class Program
    {
        public static void Main()
        {
            CreateHostBuilder().Build().Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    
                    var configuration = hostContext.Configuration;
                   
                    services.Configure<WorkerSettings>(configuration.GetSection("WorkerSettings"));
                    BootStrapper.RegisterServices(services, configuration);
                    if(configuration.GetSection("WorkerSettings").Get<WorkerSettings>().Active)
                        services.AddHostedService<Worker>();
                });
    }
}
