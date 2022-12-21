using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Application.Mappers;
using Akvelon.TaskTracker.Application.Services;
using Task = System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Akvelon.TaskTracker.Data.Models;
using System.Xml.Linq;

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
        public ActionResult<ProjectDto[]> GetAll()
        {
            try
            {
                return Ok(_projectsService.GetAll());
            }
            catch (Exception e)
            {
                // handling exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult> Create(ProjectDto project)
        {
            try
            {
                await _projectsService.Create(project);
                return Ok(_projectsService.GetAll());
            }
            catch (Exception e)
            {
                // handling exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult> Update(ProjectDto project)
        {
            try
            {
                await _projectsService.Update(project);
                return Ok(_projectsService.GetAll());
            }
            catch (Exception e)
            {
                // handling exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/[action]")]
        public async Task<ActionResult> Delete(int projectId)
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
        public ActionResult<ProjectDto[]> GetStartedAt(DateTime startedAt)
        {
            try
            {
                return Ok(_projectsService.GetStartedAt(startedAt));
            }
            catch (Exception e)
            {
                // handling exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // when project was ended before than input date
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ActionResult<ProjectDto[]> GetEndedAt(DateTime endedAt)
        {
            try
            {
                return Ok(_projectsService.GetStartedAt(endedAt));
            }
            catch (Exception e)
            {
                // handling exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // where project's name contains input name
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ActionResult<ProjectDto[]> FilterByName(string name)
        {
            try
            {
                return Ok(_projectsService.GetFilteredByName(name));
            }
            catch (Exception e)
            {
                // handling exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // where project was started before than input date and ended before than input date
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public ActionResult<ProjectDto[]> GetDateRange(DateTime startedAt, DateTime endedAt)
        {
            try
            {
                return Ok(_projectsService.GetDateRange(startedAt, endedAt));
            }
            catch (Exception e)
            {
                // handling exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
