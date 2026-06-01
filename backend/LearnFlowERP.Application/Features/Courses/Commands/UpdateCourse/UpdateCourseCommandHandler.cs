using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler
        : IRequestHandler<UpdateCourseCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCourseCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UpdateCourseCommand request,
            CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .FindAsync(request.CourseId);

            if (course == null)
                throw new Exception("Course not found");

            course.CourseCode = request.CourseCode;
            course.CourseName = request.CourseName;
            course.Credits = request.Credits;
            course.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}