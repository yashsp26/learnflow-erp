using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Auth.Commands.RefreshTokenCommands;

namespace LearnFlowERP.Application.Tests.UnitTests.Auth.Validators;

public class RefreshTokenCommandValidatorTests
{
    private readonly RefreshTokenCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_RefreshToken_Empty()
    {
        var command =
            new RefreshTokenCommand("");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.RefreshToken);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var command =
            new RefreshTokenCommand(
                new string('A', 64));

        var result =
            _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}