using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.StudentAttendances.Queries.GetCourseAttendance
{
    public class GetCourseAttendanceQueryHandler
        : IRequestHandler<GetCourseAttendanceQuery,
            List<StudentAttendance>>
    {
        private readonly IApplicationDbContext _context;

        public GetCourseAttendanceQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentAttendance>> Handle(
            GetCourseAttendanceQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.StudentAttendances
                .Where(x => x.CourseId == request.CourseId)
                .ToListAsync(cancellationToken);
        }
    }
}