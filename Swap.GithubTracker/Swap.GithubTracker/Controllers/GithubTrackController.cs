using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swap.GithubTracker.Application.Interfaces;
using Swap.GithubTracker.Application.ViewModels;
using Swap.GithubTracker.Services.Api.ActionFilters;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Swap.GithubTracker.Services.Api.Controllers
{
    [Route("api/v1/github-tracker/")]
    [ApiController]
    public class GithubTrackController : ControllerBase
    {
        private readonly IGithubTrackerApplicationService _githubTrackerApplicationService;

        public GithubTrackController(IGithubTrackerApplicationService githubTrackerApplicationService)
        {
            _githubTrackerApplicationService = githubTrackerApplicationService;
        }


        [Route("scheduleTrack")]
        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<ActionResult> ScheduleTrack([FromBody] RequestGithubTrackViewModel request)
        {
                await _githubTrackerApplicationService.AddGithubTrackAsync(request);
                return Ok();         
        }
    }
}
