using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swap.GithubTracker.Application.Interfaces;
using Swap.GithubTracker.Domain.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Services.Worker
{
    public class Worker : IHostedService, IDisposable
    {
        private readonly ILogger<Worker> _logger;
        private Timer _timer;
        private readonly int _intervalMinutes;
        private IGithubTrackerApplicationService _githubTrackerApplicationService;


        public Worker(ILogger<Worker> logger, IGithubTrackerApplicationService githubTrackerApplicationService, IOptions<WorkerSettings> settings)
        {
            _logger = logger;
            _githubTrackerApplicationService = githubTrackerApplicationService;
            _intervalMinutes = settings.Value.IntervalMinutes;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting Worker. Execution each {_intervalMinutes} minute(s)");
            _timer = new Timer(ExecuteWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(_intervalMinutes));
            return Task.CompletedTask;
        }

        private void ExecuteWork(object state)
        {            
            try
            {
                var nextExecution = DateTime.Now.AddMinutes(_intervalMinutes);
                _logger.LogInformation($"Execution Started. Time:{DateTime.Now}");
                _githubTrackerApplicationService.ProcessScheduledTracksAsync().Wait();
                _logger.LogInformation($"Execution Finished. Time:{DateTime.Now}. Next Execution: {nextExecution}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Execution Failed. Message: {ex.Message}");
            }
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping worker.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;          
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
