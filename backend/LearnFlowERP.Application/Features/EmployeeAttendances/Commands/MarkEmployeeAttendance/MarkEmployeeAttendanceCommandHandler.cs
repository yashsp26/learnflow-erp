using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.EmployeeAttendances.Commands.MarkEmployeeAttendance
{
    public class MarkEmployeeAttendanceCommandHandler
        : IRequestHandler<MarkEmployeeAttendanceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public MarkEmployeeAttendanceCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            MarkEmployeeAttendanceCommand request,
            CancellationToken cancellationToken)
        {
            var tenantId =
                _currentUser.TenantId
                ?? throw new UnauthorizedAccessException();

            var employeeExists = await _context.Employees
                .AnyAsync(
                    x => x.EmployeeId == request.EmployeeId,
                    cancellationToken);

            if (!employeeExists)
                throw new Exception("Employee not found");

            var alreadyMarked =
                await _context.EmployeeAttendances
                    .AnyAsync(
                        x =>
                            x.EmployeeId == request.EmployeeId &&
                            x.AttendanceDate.Date == DateTime.Today,
                        cancellationToken);

            if (alreadyMarked)
                throw new Exception(
                    "Attendance already marked for today");

            _context.EmployeeAttendances.Add(
                new EmployeeAttendance
                {
                    EmployeeId = request.EmployeeId,
                    TenantId = tenantId,
                    AttendanceDate = DateTime.Today,
                    Status = request.Status
                });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}