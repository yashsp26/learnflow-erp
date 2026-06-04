using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.EmployeeAttendances.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.EmployeeAttendances.Queries.GetTodayEmployeeAttendance
{
    public class GetTodayEmployeeAttendanceQueryHandler
        : IRequestHandler<
            GetTodayEmployeeAttendanceQuery,
            List<EmployeeAttendanceDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTodayEmployeeAttendanceQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeAttendanceDto>> Handle(
            GetTodayEmployeeAttendanceQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.EmployeeAttendances
                .Include(x => x.Employee)
                .Where(x =>
                    x.AttendanceDate.Date ==
                    DateTime.Today)
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