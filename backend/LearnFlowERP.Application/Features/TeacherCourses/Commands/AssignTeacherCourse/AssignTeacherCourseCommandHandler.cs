using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.TeacherCourses.Commands.AssignTeacherCourse
{
    public class AssignTeacherCourseCommandHandler
        : IRequestHandler<AssignTeacherCourseCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public AssignTeacherCourseCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            AssignTeacherCourseCommand request,
            CancellationToken cancellationToken)
        {
            var teacher = await _context.Employees
                .FirstOrDefaultAsync(
                    x => x.EmployeeId == request.EmployeeId,
                    cancellationToken);

            if (teacher == null)
                throw new Exception("Teacher not found");

            var course = await _context.Courses
                .FirstOrDefaultAsync(
                    x => x.CourseId == request.CourseId,
                    cancellationToken);

            if (course == null)
                throw new Exception("Course not found");

            var exists = await _context.TeacherCourses
                .AnyAsync(
                    x => x.EmployeeId == request.EmployeeId &&
                         x.CourseId == request.CourseId,
                    cancellationToken);

            if (exists)
                throw new Exception(
                    "Teacher already assigned to course");

            _context.TeacherCourses.Add(
                new TeacherCourse
                {
                    EmployeeId = request.EmployeeId,
                    CourseId = request.CourseId,
                    TenantId = _currentUser.TenantId ?? 0
                });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}