using Newtonsoft.Json;
using System.Collections.Generic;

namespace Swap.GithubTracker.Infra.External.Model
{
    public class GithubTrackPostRequest
    {               
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("repository")]
        public string Repository { get; set; }
        [JsonProperty("contributors")]
        public List<ContributorPostRequest> Contributors { get; set; }
        [JsonProperty("issues")]
        public List<IssuePostRequest> Issues { get; set; }

    }
}
