using System.ComponentModel.DataAnnotations;

namespace Swap.GithubTracker.Application.ViewModels
{
    public class RequestGithubTrackViewModel
    {        
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RepositoryName { get; set; }
    }
}
