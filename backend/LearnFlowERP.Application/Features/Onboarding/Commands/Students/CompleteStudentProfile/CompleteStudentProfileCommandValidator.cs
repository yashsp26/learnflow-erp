using FluentValidation;

namespace LearnFlowERP.Application.Features.Onboarding.Students.Commands.CompleteStudentProfile;

public class CompleteStudentProfileCommandValidator
    : AbstractValidator<CompleteStudentProfileCommand>
{
    public CompleteStudentProfileCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Dob)
            .LessThan(DateTime.Today);

        RuleFor(x => x.Dob)
            .GreaterThan(DateTime.Today.AddYears(-100));
    }
}