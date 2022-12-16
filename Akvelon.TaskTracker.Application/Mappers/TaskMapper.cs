using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Application.Mappers
{
    public class TaskMapper
    {
        // class only for mapping Task entities to dto or other way round
        public Data.Models.Task GetTask(TaskDto taskDto)
        {
            return new Data.Models.Task
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                Description = taskDto.Description,
                ProjectId = taskDto.ProjectId,
                Priority = taskDto.Priority,
                Status = taskDto.Status
            };
        }

        public TaskDto GetTaskDto(Data.Models.Task task)
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
