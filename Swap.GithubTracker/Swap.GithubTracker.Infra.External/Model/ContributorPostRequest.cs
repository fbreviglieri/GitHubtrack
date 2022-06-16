using Newtonsoft.Json;

namespace Swap.GithubTracker.Infra.External.Model
{
    public class ContributorPostRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("qtd_commits")]
        public int Qtd_Commits { get; set; }
    }
}
