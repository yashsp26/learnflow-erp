using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Users.Commands;

namespace LearnFlowERP.Application.Tests.UnitTests.Users.Validators;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_Username_Empty()
    {
        var command =
            new CreateUserCommand(
                "",
                1,
                "admin@test.com");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.Username);
    }

    [Fact]
    public void Should_Fail_When_Email_Invalid()
    {
        var command =
            new CreateUserCommand(
                "Admin",
                1,
                "abc");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.Email);
    }

    [Fact]
    public void Should_Fail_When_RoleId_Invalid()
    {
        var command =
            new CreateUserCommand(
                "Admin",
                0,
                "admin@test.com");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.RoleId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var command =
            new CreateUserCommand(
                "Admin",
                1,
                "admin@test.com");

        var result =
            _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}