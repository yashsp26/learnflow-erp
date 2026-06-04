using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Dashboard.DTOs;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Dashboard.Queries.GetAdminDashboard
{
    public class GetAdminDashboardQueryHandler
        : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDto>
    {
        private readonly IApplicationDbContext _context;

        public GetAdminDashboardQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardDto> Handle(
            GetAdminDashboardQuery request,
            CancellationToken cancellationToken)
        {
            var totalFees =
                await _context.Fees
                    .SumAsync(
                        x => x.TotalAmount,
                        cancellationToken);

            var totalCollected =
                await _context.Payments
                    .SumAsync(
                        x => x.AmountPaid,
                        cancellationToken);

            return new AdminDashboardDto
            {
                TotalStudents =
                    await _context.Students.CountAsync(cancellationToken),

                TotalEmployees =
                    await _context.Employees.CountAsync(cancellationToken),

                TotalCourses =
                    await _context.Courses.CountAsync(cancellationToken),

                TotalTeachers =
                    await _context.Employees
                        .CountAsync(
                            x => x.Designation.Name == "Teacher",
                            cancellationToken),

                StudentsPresentToday =
                    await _context.StudentAttendances
                        .CountAsync(
                            x =>
                                x.AttendanceDate.Date ==
                                DateTime.Today &&
                                x.Status ==
                                AttendanceStatus.Present,
                            cancellationToken),

                EmployeesPresentToday =
                    await _context.EmployeeAttendances
                        .CountAsync(
                            x =>
                                x.AttendanceDate.Date ==
                                DateTime.Today &&
                                x.Status ==
                                AttendanceStatus.Present,
                            cancellationToken),

                TotalFees = totalFees,

                TotalCollected = totalCollected,

                PendingFees = totalFees - totalCollected
            };
        }
    }
}