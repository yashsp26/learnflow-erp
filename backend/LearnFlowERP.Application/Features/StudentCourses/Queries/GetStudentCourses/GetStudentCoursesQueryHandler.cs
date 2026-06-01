using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.StudentCourses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.StudentCourses.Queries.GetStudentCourses
{
    public class GetStudentCoursesQueryHandler
        : IRequestHandler<GetStudentCoursesQuery,
            List<StudentCourseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentCoursesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentCourseDto>> Handle(
            GetStudentCoursesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.StudentCourses
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Where(x => x.StudentId == request.StudentId)
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