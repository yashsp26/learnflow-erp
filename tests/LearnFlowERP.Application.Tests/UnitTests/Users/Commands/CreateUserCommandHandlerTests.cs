using FluentAssertions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Users.Commands;
using LearnFlowERP.Application.Features.Users.Commands.Events;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LearnFlowERP.Application.Tests.UnitTests.Users.Commands;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _context;
    private readonly Mock<IPasswordHasher> _hasher;
    private readonly Mock<ICurrentUserService> _currentUser;
    private readonly Mock<IEmailService> _emailService;
    private readonly Mock<IMediator> _mediator;

    public CreateUserCommandHandlerTests()
    {
        _context = new Mock<IApplicationDbContext>();
        _hasher = new Mock<IPasswordHasher>();
        _currentUser = new Mock<ICurrentUserService>();
        _emailService = new Mock<IEmailService>();
        _mediator = new Mock<IMediator>();

        var users = new Mock<DbSet<User>>();

        _context.Setup(x => x.Users)
            .Returns(users.Object);

        _context.Setup(x =>
                x.SaveChangesAsync(
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
    }

    private CreateUserCommandHandler CreateHandler()
    {
        return new CreateUserCommandHandler(
            _context.Object,
            _hasher.Object,
            _currentUser.Object,
            _emailService.Object,
            _mediator.Object);
    }

    [Fact]
    public async Task Should_Throw_When_RoleId_Invalid()
    {
        _currentUser.Setup(x => x.TenantId)
            .Returns(1);

        _currentUser.Setup(x => x.UserId)
            .Returns(99);

        var handler = CreateHandler();

        var command =
            new CreateUserCommand(
                "john",
                999,
                "john@test.com");

        await FluentActions
            .Invoking(() =>
                handler.Handle(
                    command,
                    CancellationToken.None))
            .Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Should_Throw_When_Tenant_Not_Available()
    {
        _currentUser.Setup(x => x.TenantId)
            .Returns((long?)null);

        var handler = CreateHandler();

        var command =
            new CreateUserCommand(
                "john",
                1,
                "john@test.com");

        await FluentActions
            .Invoking(() =>
                handler.Handle(
                    command,
                    CancellationToken.None))
            .Should()
            .ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task Should_Hash_Password()
    {
        _currentUser.Setup(x => x.TenantId)
            .Returns(1);

        _currentUser.Setup(x => x.UserId)
            .Returns(99);

        _hasher.Setup(x =>
                x.Hash(It.IsAny<string>()))
            .Returns("HASHED");

        var handler = CreateHandler();

        await handler.Handle(
            new CreateUserCommand(
                "john",
                1,
                "john@test.com"),
            CancellationToken.None);

        _hasher.Verify(
            x => x.Hash(It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_Send_Email()
    {
        _currentUser.Setup(x => x.TenantId)
            .Returns(1);

        _currentUser.Setup(x => x.UserId)
            .Returns(99);

        _hasher.Setup(x =>
                x.Hash(It.IsAny<string>()))
            .Returns("HASHED");

        var handler = CreateHandler();

        await handler.Handle(
            new CreateUserCommand(
                "john",
                1,
                "john@test.com"),
            CancellationToken.None);

        _emailService.Verify(
            x => x.SendAsync(
                "john@test.com",
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_Not_Create_User_When_Email_Fails()
    {
        _currentUser.Setup(x => x.TenantId)
            .Returns(1);

        _currentUser.Setup(x => x.UserId)
            .Returns(99);

        _hasher.Setup(x =>
                x.Hash(It.IsAny<string>()))
            .Returns("HASHED");

        _emailService.Setup(x =>
                x.SendAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
            .ThrowsAsync(
                new Exception("Email failed"));

        var handler = CreateHandler();

        await Assert.ThrowsAsync<Exception>(() =>
            handler.Handle(
                new CreateUserCommand(
                    "john",
                    1,
                    "john@test.com"),
                CancellationToken.None));

        _context.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);

        _mediator.Verify(
            x => x.Publish(
                It.Is<UserCreatedEvent>(
                    e =>
                        e.Email == "john@test.com"
                        &&
                        e.Username == "john"),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Should_Save_Changes()
    {
        _currentUser.Setup(x => x.TenantId)
            .Returns(1);

        _currentUser.Setup(x => x.UserId)
            .Returns(99);

        _hasher.Setup(x =>
                x.Hash(It.IsAny<string>()))
            .Returns("HASHED");

        var handler = CreateHandler();

        await handler.Handle(
            new CreateUserCommand(
                "john",
                1,
                "john@test.com"),
            CancellationToken.None);

        _context.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_Publish_UserCreated_Event()
    {
        _currentUser.Setup(x => x.TenantId)
            .Returns(1);

        _currentUser.Setup(x => x.UserId)
            .Returns(99);

        _hasher.Setup(x =>
                x.Hash(It.IsAny<string>()))
            .Returns("HASHED");

        var handler = CreateHandler();

        await handler.Handle(
            new CreateUserCommand(
                "john",
                1,
                "john@test.com"),
            CancellationToken.None);

        _mediator.Verify(
            x => x.Publish(
                It.IsAny<UserCreatedEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}