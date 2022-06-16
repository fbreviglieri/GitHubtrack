namespace Swap.GithubTracker.Domain.Configurations
{
    public class DbGithubTrackerSettings
    {
        public string MongoConnectionString { get; set; }
        public string NomeDataBase { get; set; }
        public int DiffIntervalHours { get; set; }

        public string WebHookUrl { get; set; }
    }
}
