using FluentValidation;

namespace LearnFlowERP.Application.Features.Scholarships.Commands.CreateScholarship;

public class CreateScholarshipCommandValidator
    : AbstractValidator<CreateScholarshipCommand>
{
    public CreateScholarshipCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0);

        RuleFor(x => x.FeeId)
            .GreaterThan(0);

        RuleFor(x => x.ScholarshipName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.EffectiveFrom)
            .NotEmpty();

        RuleFor(x => x.EffectiveTo)
            .GreaterThanOrEqualTo(x => x.EffectiveFrom)
            .When(x => x.EffectiveTo.HasValue);
    }
}