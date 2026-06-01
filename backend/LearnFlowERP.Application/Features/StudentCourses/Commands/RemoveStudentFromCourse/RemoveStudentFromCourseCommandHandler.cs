using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.StudentCourses.Commands.RemoveStudentFromCourse
{
    public class RemoveStudentFromCourseCommandHandler
        : IRequestHandler<RemoveStudentFromCourseCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public RemoveStudentFromCourseCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            RemoveStudentFromCourseCommand request,
            CancellationToken cancellationToken)
        {
            var enrollment = await _context.StudentCourses
                .FirstOrDefaultAsync(
                    x => x.StudentId == request.StudentId &&
                         x.CourseId == request.CourseId,
                    cancellationToken);

            if (enrollment == null)
                throw new Exception("Enrollment not found");

            _context.StudentCourses.Remove(enrollment);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}