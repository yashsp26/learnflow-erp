using FluentValidation.TestHelper;
using LearnFlowERP.Application.Features.Scholarships.Queries.GetStudentScholarships;

namespace LearnFlowERP.Application.Tests.UnitTests.Scholarships.Validators;

public class GetStudentScholarshipsQueryValidatorTests
{
    private readonly GetStudentScholarshipsQueryValidator _validator =
        new();

    [Fact]
    public void Should_Fail_When_StudentId_Invalid()
    {
        var result =
            _validator.TestValidate(
                new GetStudentScholarshipsQuery(0));

        result.ShouldHaveValidationErrorFor(
            x => x.StudentId);
    }

    [Fact]
    public void Should_Pass_When_Valid()
    {
        var result =
            _validator.TestValidate(
                new GetStudentScholarshipsQuery(1));

        result.ShouldNotHaveAnyValidationErrors();
    }
}