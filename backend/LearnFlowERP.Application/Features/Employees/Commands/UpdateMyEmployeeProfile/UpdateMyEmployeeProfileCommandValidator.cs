using FluentValidation;

namespace LearnFlowERP.Application.Features.Employees.Commands.UpdateMyEmployeeProfile;

public class UpdateMyEmployeeProfileCommandValidator
    : AbstractValidator<UpdateMyEmployeeProfileCommand>
{
    public UpdateMyEmployeeProfileCommandValidator()
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