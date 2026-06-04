using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.StudentAttendances.Commands.MarkStudentAttendance
{
    public class MarkStudentAttendanceCommandHandler
        : IRequestHandler<MarkStudentAttendanceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public MarkStudentAttendanceCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            MarkStudentAttendanceCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            var employee = await _context.Employees
                .FirstOrDefaultAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (employee == null)
                throw new Exception("Employee not found");

            foreach (var student in request.Students)
            {
                var exists = await _context.StudentAttendances
                    .AnyAsync(x =>
                        x.StudentId == student.StudentId &&
                        x.CourseId == request.CourseId &&
                        x.AttendanceDate.Date == DateTime.Today,
                        cancellationToken);

                if (exists)
                    continue;

                _context.StudentAttendances.Add(
                    new Domain.Entities.StudentAttendance
                    {
                        StudentId = student.StudentId,
                        CourseId = request.CourseId,
                        TenantId = employee.TenantId,
                        MarkedByEmployeeId = employee.EmployeeId,
                        AttendanceDate = DateTime.Today,
                        Status = student.Status
                    });
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}