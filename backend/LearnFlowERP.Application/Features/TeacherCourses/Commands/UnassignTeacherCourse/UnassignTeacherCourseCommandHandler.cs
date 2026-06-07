using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.TeacherCourses.Commands.UnassignTeacherCourse
{
    public class UnassignTeacherCourseCommandHandler
        : IRequestHandler<UnassignTeacherCourseCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UnassignTeacherCourseCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UnassignTeacherCourseCommand request,
            CancellationToken cancellationToken)
        {
            var record = await _context.TeacherCourses
                .FirstOrDefaultAsync(
                    x => x.EmployeeId == request.EmployeeId &&
                         x.CourseId == request.CourseId,
                    cancellationToken);

            if (record == null)
                throw new NotFoundException("Assignment not found");

            _context.TeacherCourses.Remove(record);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}