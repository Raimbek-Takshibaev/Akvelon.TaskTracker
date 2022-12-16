using Akvelon.TaskTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Application.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public int? Priority { get; set; }

        public ProjectStatusEnum? Status { get; set; }

    }
}
