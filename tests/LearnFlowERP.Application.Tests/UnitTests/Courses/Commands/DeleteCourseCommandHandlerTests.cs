using FluentAssertions;
using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Courses.Commands.DeleteCourse;
using LearnFlowERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LearnFlowERP.Application.Tests.UnitTests.Courses.Commands;

public class DeleteCourseCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _context;

    public DeleteCourseCommandHandlerTests()
    {
        _context = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Should_Throw_When_Course_Not_Found()
    {
        // Arrange
        var courses = new Mock<DbSet<Course>>();

        courses.Setup(x =>
                x.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync((Course?)null);

        _context.Setup(x => x.Courses)
            .Returns(courses.Object);

        var handler =
            new DeleteCourseCommandHandler(
                _context.Object);

        // Act
        Func<Task> act = async () =>
            await handler.Handle(
                new DeleteCourseCommand(1),
                CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage("Course not found");
    }

    [Fact]
    public async Task Should_Mark_Course_Inactive()
    {
        // Arrange
        var course = new Course
        {
            CourseId = 1,
            CourseCode = "CS101",
            CourseName = "Programming",
            Credits = 4,
            IsActive = true
        };

        var courses = new Mock<DbSet<Course>>();

        courses.Setup(x =>
                x.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync(course);

        _context.Setup(x => x.Courses)
            .Returns(courses.Object);

        var handler =
            new DeleteCourseCommandHandler(
                _context.Object);

        // Act
        await handler.Handle(
            new DeleteCourseCommand(1),
            CancellationToken.None);

        // Assert
        course.IsActive.Should().BeFalse();
    }

    [Fact]
    public async Task Should_Call_SaveChanges()
    {
        // Arrange
        var course = new Course
        {
            CourseId = 1,
            IsActive = true
        };

        var courses = new Mock<DbSet<Course>>();

        courses.Setup(x =>
                x.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync(course);

        _context.Setup(x => x.Courses)
            .Returns(courses.Object);

        var handler =
            new DeleteCourseCommandHandler(
                _context.Object);

        // Act
        await handler.Handle(
            new DeleteCourseCommand(1),
            CancellationToken.None);

        // Assert
        _context.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}