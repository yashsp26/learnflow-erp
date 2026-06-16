using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Auth.Commands.Login;

namespace LearnFlowERP.Application.Tests.UnitTests.Auth.Validators;

public class LoginCommandValidatorTests
{
    private readonly LoginCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_Email_Is_Empty()
    {
        var command =
            new LoginCommand(
                "",
                "1234",
                "TENANT");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.Email);
    }

    [Fact]
    public void Should_Fail_When_Email_Invalid()
    {
        var command =
            new LoginCommand(
                "abc",
                "1234",
                "TENANT");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.Email);
    }

    [Fact]
    public void Should_Fail_When_Password_Too_Short()
    {
        var command =
            new LoginCommand(
                "admin@test.com",
                "123",
                "TENANT");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.Password);
    }

    [Fact]
    public void Should_Fail_When_TenantCode_Empty()
    {
        var command =
            new LoginCommand(
                "admin@test.com",
                "1234",
                "");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.TenantCode);
    }

    [Fact]
    public void Should_Pass_When_Request_Is_Valid()
    {
        var command =
            new LoginCommand(
                "admin@test.com",
                "1234",
                "TENANT");

        var result =
            _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}