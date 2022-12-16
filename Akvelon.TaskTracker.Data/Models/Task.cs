using Akvelon.TaskTracker.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akvelon.TaskTracker.Data.Models
{
    public enum TaskStatusEnum
    {
        ToDo,
        InProgress,
        Done
    }

    // Task entity
    public class Task : Entity
    {
        // the name of the task
        public string Name { get; set; }
        // description of the task
        public string Description { get; set; }
        // task status (enum: ToDo / InProgress / Done)
        public TaskStatusEnum? Status { get; set; }
        // priority (int)
        public int? Priority { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}
