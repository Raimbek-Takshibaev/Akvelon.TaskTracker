using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Application.Mappers;
using Akvelon.TaskTracker.Data.Models;
using Akvelon.TaskTracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Application.Services
{
    public class TasksService
    {
        public const string taskNotFoundMsg = "There is no tasks with given id";
        private TasksRepository _tasksRepository;
        private TaskMapper _taskMapper = new TaskMapper();
        private ProjectsService _projectsService;

        public TasksService(TasksRepository tasksRepository, ProjectsService projectsService)
        {
            _tasksRepository = tasksRepository;
            _projectsService = projectsService;
        }

        public bool Any(int projectId)
        {
            return _tasksRepository.GetAll().Any(p => p.Id == projectId);
        }

        public TaskDto Get(int id)
        {
            var task = _tasksRepository.First(id);
            if (task is null)
            {
                throw new KeyNotFoundException(taskNotFoundMsg);
            }
            return _taskMapper.GetTaskDto(task);
        }

        public TaskDto[] GetProjectTasks(int projectId)
        {
            // check project existence
            if (_projectsService.Any(projectId))
            {
                throw new KeyNotFoundException(ProjectsService.projectNotFoundMsg);
            }
            // filtering tasks by project id
            return _tasksRepository.GetAll().Where(task => task.ProjectId == projectId)
                .Select(task => _taskMapper.GetTaskDto(task)).ToArray();
        }

        public async System.Threading.Tasks.Task Create(TaskDto task)
        {
            // check project existence
            if (!_projectsService.Any(projectId: task.ProjectId))
            {
                throw new KeyNotFoundException(ProjectsService.projectNotFoundMsg);
            }
            // saving new task
            _tasksRepository.Create(_taskMapper.GetTask(task));
            await _tasksRepository.SaveChanges();
        }

        public async System.Threading.Tasks.Task Update(TaskDto task)
        {
            // id check
            if (!Any(task.Id))
            {
                throw new KeyNotFoundException(taskNotFoundMsg);
            }
            // updating new task
            _tasksRepository.Update(_taskMapper.GetTask(task));
            await _tasksRepository.SaveChanges();
        }

        public async System.Threading.Tasks.Task Delete(int taskId)
        {
            // id check
            if (!Any(taskId))
            {
                throw new KeyNotFoundException(taskNotFoundMsg);
            }
            // deleting task
            _tasksRepository.Delete(taskId);
            await _tasksRepository.SaveChanges();
        }
    }
}
