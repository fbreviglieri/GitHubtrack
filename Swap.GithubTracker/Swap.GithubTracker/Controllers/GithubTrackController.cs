using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swap.GithubTracker.Application.Interfaces;
using Swap.GithubTracker.Application.ViewModels;
using Swap.GithubTracker.Services.Api.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiViewModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ScheduleTrack([FromBody] RequestGithubTrackViewModel request)
        {
            try
            {
                var success = await _githubTrackerApplicationService.AddGithubTrackAsync(request);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorApiViewModel(500, ex.Message));                
            }            
        }
    }
}
