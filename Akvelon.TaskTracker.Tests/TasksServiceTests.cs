using Akvelon.TaskTracker.Application.Services;
using Akvelon.TaskTracker.Data.Helpers;
using Akvelon.TaskTracker.Data.Models;
using Akvelon.TaskTracker.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EntityTask = Akvelon.TaskTracker.Data.Models.Task;
using Task = System.Threading.Tasks.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akvelon.TaskTracker.Tests.Helpers;
using Akvelon.TaskTracker.Application.Dtos;

namespace Akvelon.TaskTracker.Tests
{
    public class TasksServiceTests
    {
        TasksService tasksService;

        [SetUp]
        public void Setup()
        {
            var context = FillDbContext.GetDBContext();
            var projectsService = new ProjectsService(new ProjectsRepository(context));
            tasksService = new TasksService(new TasksRepository(context), projectsService);
        }

        [Test]
        // method_expectedResult_condition
        public void Get_ReturnsTaskDto_IfSendingId()
        {
            // Arrange
            Random random = new Random();
            var task = FillDbContext.tasksData[random.Next(0, FillDbContext.tasksData.Length - 1)];

            // Act
            var taskDto = this.tasksService.Get(task.Id);

            // Assert
            Assert.That(taskDto.Id, Is.EqualTo(task.Id));
        }

        [Test]
        // method_expectedResult_condition
        public void GetProjectTasks_ReturnsOnlyProjectTasks_IfSendingProjectId()
        {
            // Arrange
            Random random = new Random();
            var project1 = FillDbContext.projectsData.First();
            var project2 = FillDbContext.projectsData.Last(); // getting different projects


            // Act
            var tasks1Dtos = this.tasksService.GetProjectTasks(project1.Id);
            var tasks2Dtos = this.tasksService.GetProjectTasks(project2.Id);

            // Assert
            Assert.IsTrue(!tasks1Dtos.Any(t => t.ProjectId != project1.Id));
            Assert.IsTrue(!tasks2Dtos.Any(t => t.ProjectId != project2.Id));
        }

        [Test]
        public async Task Create_CreatesNewTask_IfSendingDto()
        {
            // Arrange
            Random random = new Random();
            var newTaskDto = new TaskDto()
            {
                Name = "new_task_name",
                Description = "new_task_desc",
                ProjectId = FillDbContext.projectsData[random.Next(0, FillDbContext.projectsData.Length - 1)].Id, // random projectId
                Priority = 7,
                Status = TaskStatusEnum.ToDo
            };
            var lastExpectedTaskDtoId = this.tasksService.GetProjectTasks(newTaskDto.ProjectId).Max(t => t.Id);

            // Act
            await this.tasksService.Create(newTaskDto);

            // Assert
            var newExpectedTaskDtoId = this.tasksService.GetProjectTasks(newTaskDto.ProjectId).Max(t => t.Id);
            var newExpectedTaskDto = this.tasksService.Get(newExpectedTaskDtoId);
            Assert.That(lastExpectedTaskDtoId, Is.Not.EqualTo(newExpectedTaskDtoId));
            Assert.That(newTaskDto.Name, Is.EqualTo(newExpectedTaskDto.Name));
        }

        [Test]
        public async Task Update_UpdatesProject_IfSendingDto()
        {
            // Arrange
            Random random = new Random();
            var task = FillDbContext.tasksData[random.Next(0, FillDbContext.tasksData.Length - 1)]; // getting random project
            var taskDto = new TaskDto()
            {
                Id = task.Id,
                Description = "updated_task_desc",
                Name = "updated_test_name",
                ProjectId = task.ProjectId,
                Priority = 0,
                Status = task.Status,
            };

            // Act
            await this.tasksService.Update(taskDto);

            // Assert
            var updatedExpectedTask = this.tasksService.Get(taskDto.Id);
            Assert.That(updatedExpectedTask.Name, Is.EqualTo(taskDto.Name));
            Assert.That(updatedExpectedTask.Id, Is.EqualTo(taskDto.Id));
        }

        [Test]
        public async Task Delete_DeletesProject_IfSendingId()
        {
            // Arrange
            Random random = new Random();
            var taskId = FillDbContext.tasksData[random.Next(0, FillDbContext.tasksData.Length - 1)].Id;

            // Act
            await this.tasksService.Delete(taskId);

            // Assert
            Assert.Catch<KeyNotFoundException>(() => this.tasksService.Get(taskId));
        }
    }
}
