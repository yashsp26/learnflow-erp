using MediatR;

namespace LearnFlowERP.Application.Features.Designations.Commands.CreateDesignation
{
    public class CreateDesignationCommand : IRequest<long>
    {
        public string Name { get; set; } = null!;
    }
}