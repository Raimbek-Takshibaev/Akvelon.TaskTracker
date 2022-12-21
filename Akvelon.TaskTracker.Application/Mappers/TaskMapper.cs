using Akvelon.TaskTracker.Application.Dtos;
using EntityTask = Akvelon.TaskTracker.Data.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Akvelon.TaskTracker.Application.Mappers
{
    public class TaskMapper
    {
        // class only for mapping Task entities to dto or other way round
        public EntityTask GetTask(TaskDto taskDto)
        {
            return new EntityTask
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                Description = taskDto.Description,
                ProjectId = taskDto.ProjectId,
                Priority = taskDto.Priority,
                Status = taskDto.Status
            };
        }

        public TaskDto GetTaskDto(EntityTask task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                ProjectId = task.ProjectId,
                Priority = task.Priority,
                Status = task.Status
            };
        }
    }
}
