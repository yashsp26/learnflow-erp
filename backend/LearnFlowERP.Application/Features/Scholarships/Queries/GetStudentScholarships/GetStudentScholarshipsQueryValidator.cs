using FluentValidation;

namespace LearnFlowERP.Application.Features.Scholarships.Queries.GetStudentScholarships;

public class GetStudentScholarshipsQueryValidator
    : AbstractValidator<GetStudentScholarshipsQuery>
{
    public GetStudentScholarshipsQueryValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0);
    }
}