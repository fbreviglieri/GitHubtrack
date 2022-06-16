using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swap.GithubTracker.Infra.External.Model
{
    public class IssuePostRequest
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("labels")]
        public List<string> Labels { get; set; }
    }
}
