using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Employees.Commands.UpdateEmployeeDocument;

namespace LearnFlowERP.Application.Tests.UnitTests.Employees.Validators;

public class UpdateEmployeeDocumentCommandValidatorTests
{
    private readonly UpdateEmployeeDocumentCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_EmployeeId_Invalid()
    {
        var command =
            new UpdateEmployeeDocumentCommand(
                0,
                "https://test.com/doc.pdf");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.EmployeeId);
    }

    [Fact]
    public void Should_Fail_When_DocumentUrl_Empty()
    {
        var command =
            new UpdateEmployeeDocumentCommand(
                1,
                "");

        var result =
            _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(
            x => x.DocumentUrl);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var command =
            new UpdateEmployeeDocumentCommand(
                1,
                "https://test.com/doc.pdf");

        var result =
            _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}