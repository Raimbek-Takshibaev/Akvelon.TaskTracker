using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Application.Mappers
{
    public class ProjectMapper
    {
        // class only for mapping project entities to dto or other way round
        public Project GetProject(ProjectDto projectDto)
        {
            return new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                EndedAt = projectDto.EndedAt,
                StartedAt = (DateTime)projectDto.StartedAt,
                Status = projectDto.Status,
                Priority = projectDto.Priority,
            };
        }

        public ProjectDto GetProjectDto(Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                EndedAt = project.EndedAt,
                StartedAt = project.StartedAt,
                Status = project.Status,
                Priority = project.Priority,
            };
        }
    }
}
