using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Application.Services;
using Akvelon.TaskTracker.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using System.Threading.Tasks;

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
        [Route("getTask")]
        public ActionResult<TaskDto> Get(int id)
        {
            try
            {
                return Ok(_tasksService.Get(id));
            }
            catch (KeyNotFoundException e)
            {
                // if task not found
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("getProjectTasks")]
        public ActionResult<TaskDto[]> GetProjectTasks(int projectId)
        {
            try
            {
                return _tasksService.GetProjectTasks(projectId);
            }
            catch (KeyNotFoundException e)
            {
                // if project not found
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public async System.Threading.Tasks.Task<IActionResult> Create(TaskDto task)
        {
            try
            {
                await _tasksService.Create(task);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // if project not found
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("edit")]
        public async System.Threading.Tasks.Task<IActionResult> Update(TaskDto task)
        {
            try
            {
                await _tasksService.Update(task);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // if task not found
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("delete")]
        public async System.Threading.Tasks.Task<IActionResult> Delete(int taskId)
        {
            try
            {
                await _tasksService.Delete(taskId);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                // if task not found
                return NotFound(e.Message);
            }
        }
    }
}
