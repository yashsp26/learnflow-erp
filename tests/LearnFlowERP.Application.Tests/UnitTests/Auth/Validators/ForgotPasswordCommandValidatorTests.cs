using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Auth.Commands.ForgotPassword;

namespace LearnFlowERP.Application.Tests.UnitTests.Auth.Validators;

public class ForgotPasswordCommandValidatorTests
{
    private readonly ForgotPasswordCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_Email_Empty()
    {
        var result =
            _validator.TestValidate(
                new ForgotPasswordCommand(""));

        result.ShouldHaveValidationErrorFor(
            x => x.Email);
    }

    [Fact]
    public void Should_Fail_When_Email_Invalid()
    {
        var result =
            _validator.TestValidate(
                new ForgotPasswordCommand("abc"));

        result.ShouldHaveValidationErrorFor(
            x => x.Email);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new ForgotPasswordCommand(
                    "admin@test.com"));

        result.ShouldNotHaveAnyValidationErrors();
    }
}