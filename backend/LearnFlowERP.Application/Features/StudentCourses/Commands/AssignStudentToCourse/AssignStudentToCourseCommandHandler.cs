using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.StudentCourses.Commands.AssignStudentToCourse
{
    public class AssignStudentToCourseCommandHandler
        : IRequestHandler<AssignStudentToCourseCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public AssignStudentToCourseCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            AssignStudentToCourseCommand request,
            CancellationToken cancellationToken)
        {   
            var exists = await _context.StudentCourses
                .AnyAsync(
                    x => x.StudentId == request.StudentId &&
                         x.CourseId == request.CourseId,
                    cancellationToken);

            if (exists)
                throw new Exception(
                    "Student already assigned to course");

            var enrollment = new StudentCourse
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId,

                TenantId = _currentUser.TenantId ?? 0,

                EnrollmentDate = DateTime.Now,

                Status = "ENROLLED"
            };

            _context.StudentCourses.Add(enrollment);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}