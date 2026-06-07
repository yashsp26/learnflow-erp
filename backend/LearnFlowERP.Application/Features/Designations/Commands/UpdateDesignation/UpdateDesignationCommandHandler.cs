using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.Designations.Commands.UpdateDesignation
{
    public class UpdateDesignationCommandHandler
        : IRequestHandler<UpdateDesignationCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDesignationCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UpdateDesignationCommand request,
            CancellationToken cancellationToken)
        {
            var designation = await _context.Designations
                .FindAsync(request.DesignationId);

            if (designation == null)
                throw new NotFoundException("Designation not found");

            designation.Name = request.Name;
            designation.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}