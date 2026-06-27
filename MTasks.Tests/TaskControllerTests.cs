using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MTasks.Controllers;
using MTasks.Models;

namespace MTasks.Tests;

public class TaskControllerTests
{
    [Fact]
    public void GetAll_ShouldReturnOk()
    {
        // Arrange
        var controller = new TaskController();

        // Act
        var result = controller.GetAll();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void Create_WithValidTask_ShouldReturnCreated()
    {
        // Arrange
        var controller = new TaskController();

        var task = new TaskModel
        {
            Title = "Learn CI/CD",
            IsCompleted = false
        };

        // Act
        var result = controller.Create(task);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();

        var created = result as CreatedAtActionResult;

        created!.Value.Should().BeEquivalentTo(task);
    }

    [Fact]
    public void Create_WithEmptyTitle_ShouldReturnBadRequest()
    {
        // Arrange
        var controller = new TaskController();

        var task = new TaskModel
        {
            Title = ""
        };

        // Act
        var result = controller.Create(task);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void GetById_WhenTaskExists_ShouldReturnOk()
    {
        // Arrange
        var controller = new TaskController();

        var task = new TaskModel
        {
            Title = "Testing"
        };

        controller.Create(task);

        // Act
        var result = controller.GetById(task.Id);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void GetById_WhenTaskDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var controller = new TaskController();

        // Act
        var result = controller.GetById(999);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public void Delete_WhenTaskExists_ShouldReturnNoContent()
    {
        // Arrange
        var controller = new TaskController();

        var task = new TaskModel
        {
            Title = "Delete Me"
        };

        controller.Create(task);

        // Act
        var result = controller.Delete(task.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}