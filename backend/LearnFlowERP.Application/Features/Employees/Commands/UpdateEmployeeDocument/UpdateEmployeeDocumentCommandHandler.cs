using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.Employees.Commands.UpdateEmployeeDocument
{
    public class UpdateEmployeeDocumentCommandHandler
    : IRequestHandler<UpdateEmployeeDocumentCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmployeeDocumentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UpdateEmployeeDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found");

            employee.DocumentUrl = request.DocumentUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value; // 🔥 REQUIRED
        }
    }
}
