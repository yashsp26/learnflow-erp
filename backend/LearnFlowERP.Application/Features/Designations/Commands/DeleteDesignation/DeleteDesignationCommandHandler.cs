using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Designations.Commands.DeleteDesignation
{
    public class DeleteDesignationCommandHandler
        : IRequestHandler<DeleteDesignationCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDesignationCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteDesignationCommand request,
            CancellationToken cancellationToken)
        {
            var designation = await _context.Designations
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(
                    x => x.DesignationId == request.DesignationId,
                    cancellationToken);

            if (designation == null)
                throw new NotFoundException("Designation not found");

            if (designation.Employees.Any())
                throw new InvalidOperationException(
                    "Cannot delete designation assigned to employees");

            designation.IsActive = false;
            designation.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}