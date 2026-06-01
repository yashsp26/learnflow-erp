using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler
        : IRequestHandler<DeleteCourseCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCourseCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteCourseCommand request,
            CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .FindAsync(request.CourseId);

            if (course == null)
                throw new Exception("Course not found");

            course.IsActive = false;
            course.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}