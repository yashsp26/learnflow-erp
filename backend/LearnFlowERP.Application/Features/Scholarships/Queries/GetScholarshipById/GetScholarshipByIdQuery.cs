using LearnFlowERP.Application.Features.Scholarships.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Scholarships.Queries.GetScholarshipById
{
    public record GetScholarshipByIdQuery(
        long StudentScholarshipId
    ) : IRequest<ScholarshipDto>;
}