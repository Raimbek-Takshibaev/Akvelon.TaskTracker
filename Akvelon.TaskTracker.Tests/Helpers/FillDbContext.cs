using Akvelon.TaskTracker.Data.Models;
using EntityTask = Akvelon.TaskTracker.Data.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task =  System.Threading.Tasks.Task;
using Akvelon.TaskTracker.Application.Services;
using Akvelon.TaskTracker.Data.Helpers;
using Akvelon.TaskTracker.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Akvelon.TaskTracker.Tests.Helpers
{
    public class FillDbContext
    {
        public static readonly Project[] projectsData = {
            new Project() {
                Id = 1,
                Name = "project_name_1",
                Priority = 2,
                StartedAt = DateTimeExtensions.SetKindUtc(DateTime.Parse("1.01.1970")),
                EndedAt = DateTimeExtensions.SetKindUtc(DateTime.Parse("1.01.1980")),
                Status = ProjectStatusEnum.Completed
            },
            new Project() {
                Id = 2,
                Name = "project_name_2",
                Priority = 4,
                StartedAt = DateTimeExtensions.SetKindUtc(DateTime.Parse("1.01.1990")),
                EndedAt = DateTimeExtensions.SetKindUtc(DateTime.Parse("1.01.2000")),
                Status = ProjectStatusEnum.Completed
            },
            new Project() {
                Id = 3,
                Name = "project_name_3",
                Priority = 3,
                StartedAt = DateTimeExtensions.SetKindUtc(DateTime.Parse("1.01.2022")),
                Status = ProjectStatusEnum.NotStarted
            },
            new Project() {
                Id = 4,
                Name = "project_name_4",
                Priority = 8,
                StartedAt = DateTimeExtensions.SetKindUtc(DateTime.Parse("1.01.2020")),
                Status = ProjectStatusEnum.Active
            },
        };
        public static readonly EntityTask[] tasksData = {
            new EntityTask() {
                Id = 1,
                Description = "description1",
                Name = "task_name_1",
                Priority = 2,
                ProjectId = 1,
                Status = TaskStatusEnum.ToDo
            },
            new EntityTask() {
                Id = 2,
                Description = "description2",
                Name = "task_name_2",
                Priority = 4,
                ProjectId = 2,
                Status = TaskStatusEnum.InProgress
            },
            new EntityTask() {
                Id = 3,
                Description = "description3",
                Name = "task_name_3",
                Priority = 7,
                ProjectId = 2,
                Status = TaskStatusEnum.Done
            },
            new EntityTask() {
                Id = 4,
                Description = "description4",
                Name = "task_name_4",
                Priority = 3,
                ProjectId = 1,
                Status = TaskStatusEnum.InProgress
            },
            new EntityTask() {
                Id = 5,
                Description = "description5",
                Name = "task_name_5",
                Priority = 2,
                ProjectId = 3,
                Status = TaskStatusEnum.InProgress
            },
            new EntityTask() {
                Id = 6,
                Description = "description6",
                Name = "task_name_6",
                Priority = 4,
                ProjectId = 4,
                Status = TaskStatusEnum.ToDo
            },
            new EntityTask() {
                Id = 7,
                Description = "description7",
                Name = "task_name_7",
                Priority = 7,
                ProjectId = 4,
                Status = TaskStatusEnum.Done
            },
            new EntityTask() {
                Id = 8,
                Description = "description8",
                Name = "task_name_8",
                Priority = 3,
                ProjectId = 3,
                Status = TaskStatusEnum.ToDo
            },
        };

        public static TaskTrackerTestDbContext GetDBContext()
        {
            var connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["TestDbConnection"];
            var optionsBuilder = new DbContextOptionsBuilder<TaskTrackerDbContext>();
            optionsBuilder.UseNpgsql(connection);
            TaskTrackerTestDbContext context = new TaskTrackerTestDbContext(options: optionsBuilder.Options, projectsData.Length + 1, tasksData.Length + 1);
            context.Database.EnsureCreated();
            foreach (var project in projectsData)
            {
                if (!context.Projects.Any(p => p.Id == project.Id))
                {
                    context.Entry(project).State = EntityState.Added;
                }
            }
            foreach (var task in tasksData)
            {
                if (!context.Tasks.Any(p => p.Id == task.Id))
                {
                    context.Entry(task).State = EntityState.Added;
                }
            }
            context.SaveChanges();
            return context;
        }
    }
}
