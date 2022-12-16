using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Application.Mappers;
using Akvelon.TaskTracker.Application.Services;
using Akvelon.TaskTracker.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akvelon.TaskTracker.Web.API.Controllers
{
    [Route("api/projects")]
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
        public ProjectDto Get(int projectId)
        {
            return _projectsService.Get(projectId);
        }

        [HttpGet]
        [Route("getAll")]
        public ProjectDto[] GetAll()
        {
            return _projectsService.GetAll();
        }

        [HttpPost]
        [Route("add")]
        public async System.Threading.Tasks.Task Create(ProjectDto project)
        {
            await _projectsService.Create(project);
        }

        [HttpPost]
        [Route("edit")]
        public async System.Threading.Tasks.Task Update(ProjectDto project)
        {
            await _projectsService.Update(project);
        }

        [HttpPost]
        [Route("delete")]
        public async System.Threading.Tasks.Task Delete(int projectId)
        {
            await _projectsService.Delete(projectId);
        }

        [HttpGet]
        [Route("getStartedAt")]
        // when project was started afterwards than input date
        public ProjectDto[] GetStartedAt(DateTime startedAt)
        {
            return _projectsService.GetStartedAt(startedAt);
        }

        // when project was ended before than input date
        [HttpGet]
        [Route("getEndedAt")]
        public ProjectDto[] GetEndedAt(DateTime endedAt)
        {
            return _projectsService.GetStartedAt(endedAt);
        }

        // where project's name contains input name
        [HttpGet]
        [Route("getFilteredByName")]
        public ProjectDto[] FilterByName(string name)
        {
            return _projectsService.GetFilteredByName(name);
        }

        // where project was started before than input date and ended before than input date
        [HttpGet]
        [Route("getDateRange")]
        public ProjectDto[] GetDateRange(DateTime startedAt, DateTime endedAt)
        {
            return _projectsService.GetDateRange(startedAt, endedAt);
        }
    }
}
