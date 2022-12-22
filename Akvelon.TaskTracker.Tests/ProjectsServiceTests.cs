using Akvelon.TaskTracker.Application.Dtos;
using Akvelon.TaskTracker.Application.Services;
using Akvelon.TaskTracker.Data.Helpers;
using Akvelon.TaskTracker.Data.Models;
using Akvelon.TaskTracker.Data.Repositories;
using Akvelon.TaskTracker.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using Task = System.Threading.Tasks.Task;

namespace Akvelon.TaskTracker.Tests
{
    public class ProjectsServiceTests
    {
        ProjectsService projectsService;

        [SetUp]
        public void Setup()
        {
            var context = FillDbContext.GetDBContext();
            projectsService = new ProjectsService(new ProjectsRepository(context));
        }

        [Test]
        // method_expectedResult_condition
        public void Get_ReturnsProjectDto_IfSendingId()
        {
            // Arrange
            Random random = new Random();
            var project = FillDbContext.projectsData[random.Next(0, FillDbContext.projectsData.Length)];

            // Act
            var projectDto = this.projectsService.Get(project.Id);

            // Assert
            Assert.That(projectDto.Id, Is.EqualTo(project.Id));
        }

        [Test]
        public void GetAll_ReturnsAllProjects_OnRequest()
        {
            // Act
            var projectDtos = this.projectsService.GetAll();

            // Assert
            Assert.NotNull(projectDtos);
        }

        [Test]
        public async Task Create_CreatesNewProject_IfSendingDto()
        {
            // Arrange
            var newProjectDto = new ProjectDto()
            {
                Name = "new_project_name",
                Priority = 7,
                Status = ProjectStatusEnum.NotStarted
            };
            var lastExpectedProjectDto = this.projectsService.GetAll().Last();

            // Act
            await this.projectsService.Create(newProjectDto);

            // Assert
            var newExpectedProjectDto = this.projectsService.GetAll().Last();
            Assert.That(newExpectedProjectDto.Id, Is.Not.EqualTo(lastExpectedProjectDto.Id));
            Assert.That(newProjectDto.Name, Is.EqualTo(newExpectedProjectDto.Name));
        }

        [Test]
        public async Task Update_UpdatesProject_IfSendingDto()
        {
            // Arrange
            Random random = new Random();
            var project = FillDbContext.projectsData[random.Next(0, FillDbContext.projectsData.Length - 1)]; // getting random project
            var projectDto = new ProjectDto()
            {
                Id = project.Id,
                Name = "updated_test_name",
                Priority = 0,
                Status = project.Status,
                StartedAt = project.StartedAt,
                EndedAt = project.EndedAt,
            };

            // Act
            await this.projectsService.Update(projectDto);

            // Assert
            var updatedExpectedProject = this.projectsService.Get(projectDto.Id);
            Assert.That(updatedExpectedProject.Name, Is.EqualTo(projectDto.Name));
            Assert.That(updatedExpectedProject.Id, Is.EqualTo(projectDto.Id));
        }

        [Test]
        public async Task Delete_DeletesProject_IfSendingId()
        {
            // Arrange
            Random random = new Random();
            var projectId = FillDbContext.projectsData[random.Next(0, FillDbContext.projectsData.Length - 1)].Id;

            // Act
            await this.projectsService.Delete(projectId);

            // Assert
            Assert.Catch<KeyNotFoundException>(() => this.projectsService.Get(projectId));
        }

        [Test]
        public async Task GetStartedAt_ReturnsProjects_ThatStartedAfterInputValue()
        {
            // Arrange
            DateTime startedAt = DateTime.Parse("1.01.2000"); // only XXI century

            // Act
            var projects = projectsService.GetStartedAt(startedAt);

            // Assert
            Assert.IsFalse(projects.Any(p => p.StartedAt < startedAt));
        }

        [Test]
        public async Task GetEndedAt_ReturnsProjects_ThatEndedBeforeInputValue()
        {
            // Arrange
            DateTime endedAt = DateTime.Parse("1.01.2000"); // only XX century

            // Act
            var projects = projectsService.GetEndedAt(endedAt);

            // Assert
            Assert.IsFalse(projects.Any(p => p.EndedAt > endedAt));
        }

        [Test]
        public async Task GetDateRange_ReturnsProjects_ThatAreInDateRange()
        {
            // Arrange
            DateTime startedAt = DateTime.Parse("1.01.1960");
            DateTime endedAt = DateTime.Parse("1.01.2000"); // only XX century

            // Act
            var projects = projectsService.GetDateRange(startedAt, endedAt);

            // Assert
            Assert.IsFalse(projects.Any(p => p.EndedAt > endedAt && p.StartedAt < startedAt));
        }

        [Test]
        public async Task GetFilteredByName_ReturnsProjects_WhichNamesContainsInputValue()
        {
            // Arrange
            Random random = new Random();
            var projectName = FillDbContext.projectsData[random.Next(0, FillDbContext.projectsData.Length - 1)].Name;

            // Act
            var projects = projectsService.GetFilteredByName(projectName);

            // Assert
            Assert.IsTrue(!projects.Any(p => !p.Name.Contains(projectName)));
        }
    }
}