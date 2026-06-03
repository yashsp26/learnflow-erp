using LearnFlowERP.Application.Features.Designations.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Designations.Queries.GetDesignations
{
    public record GetDesignationsQuery()
        : IRequest<List<DesignationDto>>;
}