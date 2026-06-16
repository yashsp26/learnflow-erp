using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Users.Commands.Events;
using LearnFlowERP.Application.Features.Users.EventHandlers;
using Moq;

namespace LearnFlowERP.Application.Tests.UnitTests.Users.EventHandlers;

public class UserCreatedEventHandlerTests
{
    private readonly Mock<IEmailService> _emailService;

    public UserCreatedEventHandlerTests()
    {
        _emailService = new Mock<IEmailService>();
    }

    [Fact]
    public async Task Should_Send_Welcome_Email()
    {
        var handler =
            new UserCreatedEventHandler(
                _emailService.Object);

        var notification =
            new UserCreatedEvent(
                "john@test.com",
                "John");

        await handler.Handle(
            notification,
            CancellationToken.None);

        _emailService.Verify(
            x => x.SendAsync(
                "john@test.com",
                "Welcome to LearnFlow ERP",
                It.IsAny<string>()),
            Times.Once);
    }
}