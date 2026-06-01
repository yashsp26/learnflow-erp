using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.StudentCourses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.StudentCourses.Queries.GetCourseStudents
{
    public class GetCourseStudentsQueryHandler
        : IRequestHandler<GetCourseStudentsQuery,
            List<StudentCourseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCourseStudentsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentCourseDto>> Handle(
            GetCourseStudentsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.StudentCourses
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Where(x => x.CourseId == request.CourseId)
                .Select(x => new StudentCourseDto
                {
                    StudentId = x.StudentId,
                    CourseId = x.CourseId,

                    StudentName =
                        x.Student.FirstName + " " +
                        x.Student.LastName,

                    CourseName =
                        x.Course.CourseName,

                    EnrollmentDate =
                        x.EnrollmentDate,

                    Status =
                        x.Status
                })
                .ToListAsync(cancellationToken);
        }
    }
}