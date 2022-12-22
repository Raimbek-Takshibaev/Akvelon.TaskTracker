using Akvelon.TaskTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EntityTask = Akvelon.TaskTracker.Data.Models.Task;

namespace Akvelon.TaskTracker.Data.Helpers
{
    public partial class TaskTrackerTestDbContext : TaskTrackerDbContext
    {
        private int _projectIdentityStartValue;
        private int _tasksIdentityStartValue;


        public TaskTrackerTestDbContext(DbContextOptions<TaskTrackerDbContext> options, int projectIdentityStartValue, int tasksIdentityStartValue) : base(options)
        {
            _projectIdentityStartValue = projectIdentityStartValue;
            _tasksIdentityStartValue = tasksIdentityStartValue;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // fixing npg sql bug https://github.com/npgsql/efcore.pg/issues/367
            modelBuilder.Entity<Project>().Property(e => e.Id).HasIdentityOptions(startValue: _projectIdentityStartValue); // id auto incrementation start value
            modelBuilder.Entity<EntityTask>().Property(e => e.Id).HasIdentityOptions(startValue: _tasksIdentityStartValue);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
