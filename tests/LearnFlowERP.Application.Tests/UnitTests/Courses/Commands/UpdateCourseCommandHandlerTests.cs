using FluentAssertions;
using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Courses.Commands.UpdateCourse;
using LearnFlowERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LearnFlowERP.Application.Tests.Courses.Commands;

public class UpdateCourseCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _context;

    public UpdateCourseCommandHandlerTests()
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
            new UpdateCourseCommandHandler(
                _context.Object);

        // Act
        Func<Task> act = async () =>
            await handler.Handle(
                new UpdateCourseCommand
                {
                    CourseId = 1,
                    CourseCode = "CS101",
                    CourseName = "Programming",
                    Credits = 4
                },
                CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage("Course not found");
    }

    [Fact]
    public async Task Should_Update_Course()
    {
        // Arrange
        var course = new Course
        {
            CourseId = 1,
            CourseCode = "OLD",
            CourseName = "Old Course",
            Credits = 2
        };

        var courses = new Mock<DbSet<Course>>();

        courses.Setup(x =>
                x.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync(course);

        _context.Setup(x => x.Courses)
            .Returns(courses.Object);

        var handler =
            new UpdateCourseCommandHandler(
                _context.Object);

        var command =
            new UpdateCourseCommand
            {
                CourseId = 1,
                CourseCode = "CS101",
                CourseName = "Programming",
                Credits = 4
            };

        // Act
        await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        course.CourseCode.Should()
            .Be("CS101");

        course.CourseName.Should()
            .Be("Programming");

        course.Credits.Should()
            .Be(4);
    }

    [Fact]
    public async Task Should_Set_UpdatedAt()
    {
        // Arrange
        var course = new Course
        {
            CourseId = 1
        };

        var courses = new Mock<DbSet<Course>>();

        courses.Setup(x =>
                x.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync(course);

        _context.Setup(x => x.Courses)
            .Returns(courses.Object);

        var handler =
            new UpdateCourseCommandHandler(
                _context.Object);

        // Act
        await handler.Handle(
            new UpdateCourseCommand
            {
                CourseId = 1,
                CourseCode = "CS101",
                CourseName = "Programming",
                Credits = 4
            },
            CancellationToken.None);

        // Assert
        course.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Call_SaveChanges()
    {
        // Arrange
        var course = new Course
        {
            CourseId = 1
        };

        var courses = new Mock<DbSet<Course>>();

        courses.Setup(x =>
                x.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync(course);

        _context.Setup(x => x.Courses)
            .Returns(courses.Object);

        var handler =
            new UpdateCourseCommandHandler(
                _context.Object);

        // Act
        await handler.Handle(
            new UpdateCourseCommand
            {
                CourseId = 1,
                CourseCode = "CS101",
                CourseName = "Programming",
                Credits = 4
            },
            CancellationToken.None);

        // Assert
        _context.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}