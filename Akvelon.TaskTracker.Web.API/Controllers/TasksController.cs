using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Application.Services;
using Akvelon.TaskTracker.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Akvelon.TaskTracker.Web.API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // basic crud functionality
        private TasksService _tasksService;

        public TasksController(TasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public TaskDto Get(int id)
        {
            return _tasksService.Get(id);
        }

        [HttpGet]
        [Route("getProjectTasks")]
        public TaskDto[] GetProjectTasks(int projectId)
        {
            return _tasksService.GetProjectTasks(projectId);
        }

        [HttpPost]
        [Route("add")]
        public async System.Threading.Tasks.Task Create(TaskDto task)
        {
            await _tasksService.Create(task);
        }

        [HttpPost]
        [Route("edit")]
        public async System.Threading.Tasks.Task Update(TaskDto task)
        {
            await _tasksService.Update(task);
        }

        [HttpPost]
        [Route("delete")]
        public async System.Threading.Tasks.Task Delete(int taskId)
        {
            await _tasksService.Delete(taskId);
        }
    }
}
