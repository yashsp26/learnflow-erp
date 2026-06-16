using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Scholarships.Commands.CreateScholarship;

namespace LearnFlowERP.Application.Tests.UnitTests.Scholarships.Validators;

public class CreateScholarshipCommandValidatorTests
{
    private readonly CreateScholarshipCommandValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_StudentId_Invalid()
    {
        var command = new CreateScholarshipCommand(
            0,
            1,
            "Merit",
            1000,
            DateTime.Today,
            null);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.StudentId);
    }

    [Fact]
    public void Should_Fail_When_FeeId_Invalid()
    {
        var command = new CreateScholarshipCommand(
            1,
            0,
            "Merit",
            1000,
            DateTime.Today,
            null);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.FeeId);
    }

    [Fact]
    public void Should_Fail_When_Name_Empty()
    {
        var command = new CreateScholarshipCommand(
            1,
            1,
            "",
            1000,
            DateTime.Today,
            null);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.ScholarshipName);
    }

    [Fact]
    public void Should_Fail_When_Amount_Invalid()
    {
        var command = new CreateScholarshipCommand(
            1,
            1,
            "Merit",
            0,
            DateTime.Today,
            null);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Amount);
    }

    [Fact]
    public void Should_Fail_When_EffectiveTo_Before_EffectiveFrom()
    {
        var command = new CreateScholarshipCommand(
            1,
            1,
            "Merit",
            1000,
            DateTime.Today,
            DateTime.Today.AddDays(-1));

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.EffectiveTo);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var command = new CreateScholarshipCommand(
            1,
            1,
            "Merit Scholarship",
            1000,
            DateTime.Today,
            DateTime.Today.AddMonths(6));

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}