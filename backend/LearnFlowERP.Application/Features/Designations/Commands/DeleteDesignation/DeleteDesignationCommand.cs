using MediatR;

namespace LearnFlowERP.Application.Features.Designations.Commands.DeleteDesignation
{
    public record DeleteDesignationCommand(long DesignationId)
        : IRequest<Unit>;
}