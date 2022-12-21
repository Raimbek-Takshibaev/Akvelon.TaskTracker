using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Application.Mappers;
using Akvelon.TaskTracker.Application.Services;
using Akvelon.TaskTracker.Data.Models;
using Akvelon.TaskTracker.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akvelon.TaskTracker.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        // basic crud functionality
        private ProjectsService _projectsService;

        public ProjectsController(ProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ActionResult<ProjectDto> Get(int projectId)
        {
            try
            {
                return Ok(_projectsService.Get(projectId));
            }
            catch (KeyNotFoundException e)
            {
                // if project not found
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ProjectDto[] GetAll()
        {
            return _projectsService.GetAll();
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async System.Threading.Tasks.Task Create(ProjectDto project)
        {
            await _projectsService.Create(project);
        }

        [HttpPut]
        [Route("api/[controller]/[action]")]
        public async System.Threading.Tasks.Task<IActionResult> Update(ProjectDto project)
        {
            try
            {
                await _projectsService.Update(project);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // if project not found
                return NotFound(e.Message);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/[action]")]
        public async System.Threading.Tasks.Task<IActionResult> Delete(int projectId)
        {
            try
            {
                await _projectsService.Delete(projectId);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // if project not found
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        // when project was started afterwards than input date
        public ProjectDto[] GetStartedAt(DateTime startedAt)
        {
            return _projectsService.GetStartedAt(startedAt);
        }

        // when project was ended before than input date
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ProjectDto[] GetEndedAt(DateTime endedAt)
        {
            return _projectsService.GetStartedAt(endedAt);
        }

        // where project's name contains input name
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ProjectDto[] FilterByName(string name)
        {
            return _projectsService.GetFilteredByName(name);
        }

        // where project was started before than input date and ended before than input date
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ProjectDto[] GetDateRange(DateTime startedAt, DateTime endedAt)
        {
            return _projectsService.GetDateRange(startedAt, endedAt);
        }
    }
}
