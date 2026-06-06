using MediatR;

namespace LearnFlowERP.Application.Features.Scholarships.Commands.RemoveScholarship
{
    public record RemoveScholarshipCommand(
        long StudentScholarshipId
    ) : IRequest<Unit>;
}