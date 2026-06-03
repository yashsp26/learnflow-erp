using MediatR;

namespace LearnFlowERP.Application.Features.Designations.Commands.UpdateDesignation
{
    public class UpdateDesignationCommand : IRequest<Unit>
    {
        public long DesignationId { get; set; }

        public string Name { get; set; } = null!;
    }
}