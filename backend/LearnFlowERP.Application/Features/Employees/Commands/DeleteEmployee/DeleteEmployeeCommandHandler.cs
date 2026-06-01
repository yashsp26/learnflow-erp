using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler
        : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(
                request.EmployeeId);

            if (employee == null)
                throw new Exception("Employee not found");

            employee.IsActive = false;
            employee.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}