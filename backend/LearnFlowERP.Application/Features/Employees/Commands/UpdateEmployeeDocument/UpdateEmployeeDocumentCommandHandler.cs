using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Employees.Commands.UpdateEmployeeDocument
{
    public class UpdateEmployeeDocumentCommandHandler
        : IRequestHandler<UpdateEmployeeDocumentCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmployeeDocumentCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UpdateEmployeeDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(
                    x => x.EmployeeId == request.EmployeeId,
                    cancellationToken);

            if (employee == null)
                throw new NotFoundException(
                    "Employee not found");

            employee.DocumentUrl =
                request.DocumentUrl;

            await _context.SaveChangesAsync(
                cancellationToken);

            return Unit.Value;
        }
    }
}