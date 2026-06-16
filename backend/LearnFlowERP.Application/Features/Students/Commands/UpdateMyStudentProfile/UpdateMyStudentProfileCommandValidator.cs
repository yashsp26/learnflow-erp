using FluentValidation;

namespace LearnFlowERP.Application.Features.Students.Commands.UpdateMyStudentProfile;

public class UpdateMyStudentProfileCommandValidator
    : AbstractValidator<UpdateMyStudentProfileCommand>
{
    public UpdateMyStudentProfileCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Dob)
            .LessThan(DateTime.Today);
    }
}