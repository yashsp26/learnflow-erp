using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Scholarships.Commands.RemoveScholarship;

namespace LearnFlowERP.Application.Tests.UnitTests.Scholarships.Validators;

public class RemoveScholarshipCommandValidatorTests
{
    private readonly RemoveScholarshipCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_Id_Invalid()
    {
        var result =
            _validator.TestValidate(
                new RemoveScholarshipCommand(0));

        result.ShouldHaveValidationErrorFor(
            x => x.StudentScholarshipId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new RemoveScholarshipCommand(1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}