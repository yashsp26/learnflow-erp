using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Auth.Commands.LogOut;

namespace LearnFlowERP.Application.Tests.UnitTests.Auth.Validators;

public class LogoutCommandValidatorTests
{
    private readonly LogoutCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_Token_Empty()
    {
        var result =
            _validator.TestValidate(
                new LogoutCommand(""));

        result.ShouldHaveValidationErrorFor(
            x => x.RefreshToken);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new LogoutCommand(
                    "SomeRefreshToken"));

        result.ShouldNotHaveAnyValidationErrors();
    }
}