using FluentAssertions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Permissions.Queries.GetCurrentUserPermissions;
using Moq;

namespace LearnFlowERP.Application.Tests.UnitTests.Permissions.Queries;

public class GetCurrentUserPermissionsQueryHandlerTests
{
    private readonly Mock<IPermissionCacheService> _permissionCache;
    private readonly Mock<ICurrentUserService> _currentUser;

    public GetCurrentUserPermissionsQueryHandlerTests()
    {
        _permissionCache =
            new Mock<IPermissionCacheService>();

        _currentUser =
            new Mock<ICurrentUserService>();
    }

    [Fact]
    public async Task Should_Return_Empty_List_When_User_Not_Logged_In()
    {
        _currentUser.Setup(x => x.UserId)
            .Returns((long?)null);

        var handler =
            new GetCurrentUserPermissionsQueryHandler(
                _permissionCache.Object,
                _currentUser.Object);

        var result =
            await handler.Handle(
                new GetCurrentUserPermissionsQuery(),
                CancellationToken.None);

        result.Should().BeEmpty();

        _permissionCache.Verify(
            x => x.GetPermissionsAsync(
                It.IsAny<long>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Should_Return_User_Permissions()
    {
        _currentUser.Setup(x => x.UserId)
            .Returns(10);

        var permissions =
            new List<string>
            {
                "student.view",
                "student.create"
            };

        _permissionCache.Setup(x =>
                x.GetPermissionsAsync(
                    10,
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(permissions);

        var handler =
            new GetCurrentUserPermissionsQueryHandler(
                _permissionCache.Object,
                _currentUser.Object);

        var result =
            await handler.Handle(
                new GetCurrentUserPermissionsQuery(),
                CancellationToken.None);

        result.Should().HaveCount(2);

        result.Should().Contain(
            "student.view");

        _permissionCache.Verify(
            x => x.GetPermissionsAsync(
                10,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}