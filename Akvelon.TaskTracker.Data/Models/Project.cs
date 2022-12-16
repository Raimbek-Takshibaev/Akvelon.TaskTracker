using Akvelon.TaskTracker.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Data.Models
{
    public enum ProjectStatusEnum
    {
        NotStarted,
        Active,
        Completed
    }
    // Project entity
    public class Project : Entity
    {
        // the name of the project
        public string Name { get; set; }
        // project start date
        public DateTime StartedAt { get; set; }
        // project completion date
        public DateTime? EndedAt { get; set; }
        // priority (int)
        public int? Priority { get; set; }
        // the current status of the project (enum: NotStarted, Active, Completed)
        public ProjectStatusEnum? Status { get; set; }
    }
}
