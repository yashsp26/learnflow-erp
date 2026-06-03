using LearnFlowERP.Application.Features.Designations.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Designations.Queries.GetDesignationById
{
    public record GetDesignationByIdQuery(long DesignationId)
        : IRequest<DesignationDto>;
}