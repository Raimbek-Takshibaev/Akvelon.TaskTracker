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
        public DateTime StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public int? Priority { get; set; }

        public ProjectStatusEnum? Status { get; set; }
    }
}
