using Microsoft.AspNetCore.Mvc;
using Swap.GithubTracker.Application.Interfaces;
using Swap.GithubTracker.Application.ViewModels;
using System;
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
        public async Task<ActionResult> ScheduleTrack([FromBody] RequestGithubTrackViewModel request)
        {
            try
            {
                var success = await _githubTrackerApplicationService.AddGithubTrackAsync(request);
                return Ok(success);

            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
