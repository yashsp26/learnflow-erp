using FluentValidation;

namespace LearnFlowERP.Application.Features.Scholarships.Commands.RemoveScholarship;

public class RemoveScholarshipCommandValidator
    : AbstractValidator<RemoveScholarshipCommand>
{
    public RemoveScholarshipCommandValidator()
    {
        RuleFor(x => x.StudentScholarshipId)
            .GreaterThan(0);
    }
}