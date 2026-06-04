using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.StudentAttendances.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.StudentAttendances.Queries.GetStudentAttendance
{
    public class GetStudentAttendanceQueryHandler
        : IRequestHandler<GetStudentAttendanceQuery,
            List<StudentAttendanceDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentAttendanceQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentAttendanceDto>> Handle(
            GetStudentAttendanceQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.StudentAttendances
                .Where(x => x.StudentId == request.StudentId)
                .Include(x => x.Course)
                .Select(x => new StudentAttendanceDto
                {
                    Date = x.AttendanceDate,
                    Status = x.Status,
                    CourseName = x.Course.CourseName
                })
                .ToListAsync(cancellationToken);
        }
    }
}