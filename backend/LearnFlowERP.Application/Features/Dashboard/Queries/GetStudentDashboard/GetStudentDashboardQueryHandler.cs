using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Dashboard.DTOs;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Dashboard.Queries.GetStudentDashboard
{
    public class GetStudentDashboardQueryHandler
        : IRequestHandler<
            GetStudentDashboardQuery,
            StudentDashboardDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetStudentDashboardQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<StudentDashboardDto> Handle(
            GetStudentDashboardQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            var student =
                await _context.Students
                    .FirstAsync(
                        x => x.UserId == userId,
                        cancellationToken);

            var totalAttendance =
                await _context.StudentAttendances
                    .CountAsync(
                        x =>
                            x.StudentId ==
                            student.StudentId,
                        cancellationToken);

            var presentAttendance =
                await _context.StudentAttendances
                    .CountAsync(
                        x =>
                            x.StudentId ==
                            student.StudentId &&
                            x.Status ==
                            AttendanceStatus.Present,
                        cancellationToken);

            var totalFees =
                await _context.Fees
                    .Where(x =>
                        x.StudentId ==
                        student.StudentId)
                    .SumAsync(
                        x => x.TotalAmount,
                        cancellationToken);

            var totalPaid =
                await _context.Payments
                    .Where(x =>
                        x.Fee.StudentId ==
                        student.StudentId)
                    .SumAsync(
                        x => x.AmountPaid,
                        cancellationToken);

            return new StudentDashboardDto
            {
                TotalCourses =
                    await _context.StudentCourses
                        .CountAsync(
                            x =>
                                x.StudentId ==
                                student.StudentId,
                            cancellationToken),

                AttendancePercentage =
                    totalAttendance == 0
                        ? 0
                        : Math.Round(
                            ((decimal)presentAttendance /
                             totalAttendance) * 100,
                            2),

                PendingFees =
                    totalFees - totalPaid
            };
        }
    }
}