using FluentValidation;

namespace LearnFlowERP.Application.Features.Onboarding.Commands.Employees.CompleteEmployeeProfile;

public class CompleteEmployeeProfileCommandValidator
    : AbstractValidator<CompleteEmployeeProfileCommand>
{
    public CompleteEmployeeProfileCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Department)
            .NotEmpty()
            .MaximumLength(100);
    }
}