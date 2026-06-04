using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Dashboard.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Dashboard.Queries.GetTeacherDashboard
{
    public class GetTeacherDashboardQueryHandler
        : IRequestHandler<
            GetTeacherDashboardQuery,
            TeacherDashboardDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetTeacherDashboardQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<TeacherDashboardDto> Handle(
            GetTeacherDashboardQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            var employee =
                await _context.Employees
                    .FirstAsync(
                        x => x.UserId == userId,
                        cancellationToken);

            var courseIds =
                await _context.TeacherCourses
                    .Where(x =>
                        x.EmployeeId ==
                        employee.EmployeeId)
                    .Select(x => x.CourseId)
                    .ToListAsync(cancellationToken);

            return new TeacherDashboardDto
            {
                AssignedCourses = courseIds.Count,

                TotalStudents =
                    await _context.StudentCourses
                        .CountAsync(
                            x =>
                                courseIds.Contains(x.CourseId),
                            cancellationToken),

                AttendanceMarkedToday =
                    await _context.StudentAttendances
                        .CountAsync(
                            x =>
                                x.MarkedByEmployeeId ==
                                employee.EmployeeId &&
                                x.AttendanceDate.Date ==
                                DateTime.Today,
                            cancellationToken)
            };
        }
    }
}