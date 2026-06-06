using MediatR;

namespace LearnFlowERP.Application.Features.Scholarships.Commands.CreateScholarship
{
    public record CreateScholarshipCommand(
        long StudentId,
        long FeeId,
        string ScholarshipName,
        decimal Amount,
        DateTime EffectiveFrom,
        DateTime? EffectiveTo
    ) : IRequest<long>;
}