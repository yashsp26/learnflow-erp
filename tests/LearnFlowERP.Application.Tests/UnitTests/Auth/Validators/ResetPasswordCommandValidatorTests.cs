using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Auth.Commands.ResetPassword;

namespace LearnFlowERP.Application.Tests.UnitTests.Auth.Validators;

public class ResetPasswordCommandValidatorTests
{
    private readonly ResetPasswordCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_Email_Invalid()
    {
        var result =
            _validator.TestValidate(
                new ResetPasswordCommand(
                    "abc",
                    "123456",
                    "Password123"));

        result.ShouldHaveValidationErrorFor(
            x => x.Email);
    }

    [Fact]
    public void Should_Fail_When_Otp_Invalid()
    {
        var result =
            _validator.TestValidate(
                new ResetPasswordCommand(
                    "admin@test.com",
                    "123",
                    "Password123"));

        result.ShouldHaveValidationErrorFor(
            x => x.Otp);
    }

    [Fact]
    public void Should_Fail_When_Password_Too_Short()
    {
        var result =
            _validator.TestValidate(
                new ResetPasswordCommand(
                    "admin@test.com",
                    "123456",
                    "123"));

        result.ShouldHaveValidationErrorFor(
            x => x.NewPassword);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new ResetPasswordCommand(
                    "admin@test.com",
                    "123456",
                    "Password123"));

        result.ShouldNotHaveAnyValidationErrors();
    }
}