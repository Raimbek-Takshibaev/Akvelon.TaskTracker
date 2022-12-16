using System;
using System.Collections.Generic;
using Akvelon.TaskTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Akvelon.TaskTracker.Data.Helpers;

public partial class TaskTrackerDbContext : DbContext
{
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Models.Task> Tasks { get; set; }
    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
