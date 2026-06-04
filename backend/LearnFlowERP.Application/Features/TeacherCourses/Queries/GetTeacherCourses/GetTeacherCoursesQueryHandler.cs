using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.TeacherCourses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.TeacherCourses.Queries.GetTeacherCourses
{
    public class GetTeacherCoursesQueryHandler
        : IRequestHandler<GetTeacherCoursesQuery,
            List<TeacherCourseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTeacherCoursesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherCourseDto>> Handle(
            GetTeacherCoursesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.TeacherCourses
                .Include(x => x.Employee)
                .Include(x => x.Course)
                .Where(x => x.EmployeeId == request.EmployeeId)
                .Select(x => new TeacherCourseDto
                {
                    EmployeeId = x.EmployeeId,
                    CourseId = x.CourseId,

                    EmployeeName =
                        x.Employee.FirstName + " " +
                        x.Employee.LastName,

                    EmpCode = x.Employee.EmpCode,

                    CourseCode = x.Course.CourseCode,

                    CourseName = x.Course.CourseName,

                    AssignedAt = x.AssignedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}