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
        private TasksRepository _tasksRepository;
        private TaskMapper _taskMapper = new TaskMapper();
        private ProjectsRepository _projectsRepository;

        public TasksService(TasksRepository tasksRepository, ProjectsRepository projectsRepository)
        {
            _tasksRepository = tasksRepository;
            _projectsRepository = projectsRepository;
        }

        public TaskDto Get(int id)
        {
            var task = _tasksRepository.First(id);
            if (task is null)
            {
                throw new Exception("Task not found");
            }
            return _taskMapper.GetTaskDto(task);
        }

        public TaskDto[] GetProjectTasks(int projectId)
        {
            return _tasksRepository.GetAll().Where(task => task.ProjectId == projectId)
                .Select(task => _taskMapper.GetTaskDto(task)).ToArray();
        }

        public async System.Threading.Tasks.Task Create(TaskDto task)
        {
            Project project = _projectsRepository.First(task.ProjectId);
            if (project is null)
            {
                throw new Exception("Project not found");
            }
            // saving new task
            _tasksRepository.Create(_taskMapper.GetTask(task));
            await _tasksRepository.SaveChanges();
        }

        public async System.Threading.Tasks.Task Update(TaskDto task)
        {
            // id check
            if (!_tasksRepository.GetAll().Any(t => t.Id == task.Id))
            {
                throw new Exception("There is no tasks with given id");
            }
            // updating new task
            _tasksRepository.Update(_taskMapper.GetTask(task));
            await _tasksRepository.SaveChanges();
        }

        public async System.Threading.Tasks.Task Delete(int taskId)
        {
            // id check
            if (!_tasksRepository.GetAll().Any(t => t.Id == taskId))
            {
                throw new Exception("There is no tasks with given id");
            }
            // deleting task
            _tasksRepository.Delete(taskId);
            await _tasksRepository.SaveChanges();
        }
    }
}
