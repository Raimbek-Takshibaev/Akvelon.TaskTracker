using System;
using System.Collections.Generic;
using Akvelon.TaskTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EntityTask = Akvelon.TaskTracker.Data.Models.Task;

namespace Akvelon.TaskTracker.Data.Helpers;

public partial class TaskTrackerDbContext : DbContext
{
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<EntityTask> Tasks { get; set; }

    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
