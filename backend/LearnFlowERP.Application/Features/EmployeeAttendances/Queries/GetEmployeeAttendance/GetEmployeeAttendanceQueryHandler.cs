using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.EmployeeAttendances.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.EmployeeAttendances.Queries.GetEmployeeAttendance
{
    public class GetEmployeeAttendanceQueryHandler
        : IRequestHandler<
            GetEmployeeAttendanceQuery,
            List<EmployeeAttendanceDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetEmployeeAttendanceQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeAttendanceDto>> Handle(
            GetEmployeeAttendanceQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.EmployeeAttendances
                .Include(x => x.Employee)
                .Where(x =>
                    x.EmployeeId == request.EmployeeId)
                .OrderByDescending(x =>
                    x.AttendanceDate)
                .Select(x =>
                    new EmployeeAttendanceDto
                    {
                        EmployeeId =
                            x.EmployeeId,

                        EmployeeName =
                            (x.Employee.FirstName ?? "")
                            + " "
                            + (x.Employee.LastName ?? ""),

                        AttendanceDate =
                            x.AttendanceDate,

                        Status =
                            x.Status
                    })
                .ToListAsync(cancellationToken);
        }
    }
}